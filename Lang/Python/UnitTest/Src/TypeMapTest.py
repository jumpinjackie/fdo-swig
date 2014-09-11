#
# Copyright (C) 2004-2007  Autodesk, Inc.
# 
# This library is free software; you can redistribute it and/or
# modify it under the terms of version 2.1 of the GNU Lesser
# General Public License as published by the Free Software Foundation.
# 
# This library is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
# Lesser General Public License for more details.
# 
# You should have received a copy of the GNU Lesser General Public
# License along with this library; if not, write to the Free Software
# Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
#


import traceback
import string
import os.path
import sys
from FDO import *
import unittest

class TypeMapTest(unittest.TestCase):
	"""
	Unit test for SWIG typemaps.  The typemaps handle the marshalling
	of C++ datatypes into the Python environment.  The marshalling
	includes:
		- converting wchar_t* into a Python string
		- converting enumerations into Python numbers
		- converting int* to Python lists, whose first item is a tuple of
		immutable string values, and the second item is the size of the tuple
		- converting wchar_t** to Python lists, whose first item is a tuple
		of strings, and the second item is the size of the tuple
	"""
	def testPrimitives(self):
		"""
		Test the marshalling of primitives back and forth between
		Python and C++
		"""
		
		# Check doubles
		doubleVal = FdoDoubleValue.Create(1234.123212)
		self.assert_(doubleVal.Double == 1234.123212)

		# Check int 64s
		int64Val = FdoInt64Value.Create()
		int64Val.Int64 = 123
		self.assert_(int64Val.Int64 == 123)
		
		# Check int 32s
		int32Val = FdoInt32Value.Create()
		int32Val.Int32 = 12345
		self.assert_(int32Val.Int32 == 12345)
	
		# Check int 16s	
		int16Val = FdoInt16Value.Create()
		int16Val.Int16 = 12345
		self.assert_(int16Val.Int16 == 12345)
		
		# Check utf-8 strings
		stringVal = FdoStringValue.Create()
		stringVal.String = "Hello World"
		self.assert_(stringVal.ToString() == "'Hello World'")
		
		# Check unicode strings
		stringVal = FdoStringValue.Create()
		stringVal.String = u"Andr\202 Encyclop\221dia"
		uString = stringVal.ToString().encode('ascii', 'xmlcharrefreplace')		
		self.assert_(uString == "'Andr&#130; Encyclop&#145;dia'")
		
	def testExceptions(self):
		"""
		Verify the marshalling of FdoException objects
		"""
		try:
			connMgr = FdoFeatureAccessManager.GetConnectionManager()
			conn = connMgr.CreateConnection("This doesn't exist")
		except:
			excType, excValue, excTraceback = sys.exc_info()
			self.assert_(str(excType) == "<class 'FDOw.FdoException'>")			
			return None
			
		# An exception was expected here.  Raise an exception
		raise AssertionError("Exception expected")

	def testEnumerations(self):
		"""
		Test the marshalling of FDO enumerated types into
		Python integers.  These are a spotcheck for
		most of the FDO enumerated types.
		"""	
		self.assert_(FdoDataType_Boolean == 0)
		self.assert_(FdoDataType_Int32 == 6	)
		self.assert_(FdoConnectionState_Closed == 1)
		self.assert_(FdoClassType_NetworkLinkClass == 5)
		self.assert_(FdoGeometryType_MultiCurveString == 12)

	def testCollectionMagic(self):
		"""
		Test the magic methods injected into each FdoCollection-derived proxy class. For this test case,
		we'll use a schema collection
		"""
		schemas = FdoFeatureSchemaCollection.Create(None)
		schema1 = FdoFeatureSchema.Create("Test1", "")
		schema2 = FdoFeatureSchema.Create("Test2", "")
		schemas.Add(schema1)
		schemas.Add(schema2)
		self.assertEqual(2, len(schemas))
		self.assertEqual(schemas.Count, len(schemas))
		self.assertTrue(schema1 in schemas)
		self.assertTrue(schema2 in schemas)
		schemas.Clear()
		#now try __setitem__ with __contains__ checks
		schemas[0] = schema1
		schemas[1] = schema2
		self.assertEqual(2, len(schemas))
		self.assertEqual(schemas.Count, len(schemas))
		self.assertTrue(schema1 in schemas)
		self.assertTrue(schema2 in schemas)
		schemas.Clear()
		#ensure invalid bounds are handled
		try:
			schemas[5] = schema1
			raise AssertionError("Expected exception on invalid bounds")
		except:
			pass
		schemas[0] = schema1
		self.assertEqual(1, len(schemas))
		self.assertEqual(schemas.Count, len(schemas))
		self.assertTrue(schema1 in schemas)
		self.assertFalse(schema2 in schemas)
		try:
			schemas[5] = schema2
			raise AssertionError("Expected exception on invalid bounds")
		except:
			pass
		schemas[1] = schema2
		self.assertEqual(2, len(schemas))
		self.assertEqual(schemas.Count, len(schemas))
		self.assertTrue(schema1 in schemas)
		self.assertTrue(schema2 in schemas)
		#now try __delitem__
		del schemas[1]
		self.assertTrue(schema1 in schemas)
		self.assertFalse(schema2 in schemas)
		self.assertEqual(1, len(schemas))
		del schemas[0]
		self.assertFalse(schema1 in schemas)
		self.assertFalse(schema2 in schemas)
		self.assertEqual(0, len(schemas))

	def testValueExpression(self):
		"""
		Test the out typemap for FdoValueExpression
		"""
		boolVal = FdoBooleanValue.Create(True)
		pBool = FdoPropertyValue.Create("BOOL", boolVal)
		boolVal2 = pBool.GetValue()
		self.assertEqual("FdoBooleanValue", boolVal2.__class__.__name__)
		self.assertEqual(boolVal.Boolean, boolVal2.Boolean)

		bVal = FdoByteValue.Create(1)
		pByte = FdoPropertyValue.Create("BYTE", bVal)
		bVal2 = pByte.GetValue()
		self.assertEqual("FdoByteValue", bVal2.__class__.__name__)
		self.assertEqual(bVal.Byte, bVal2.Byte)

		#FIXME: SWIG reports FdoDateTime is memory leaking because there's no dtor and SWIG is
		#instructed not to create any
		dt = FdoDateTime()
		dtVal = FdoDateTimeValue.Create(dt)
		pDateTime = FdoPropertyValue.Create("DATETIME", dtVal)
		dtVal2 = pDateTime.GetValue()
		self.assertEqual("FdoDateTimeValue", dtVal.__class__.__name__)
		dt1 = dtVal.GetDateTime()
		dt2 = dtVal2.GetDateTime();
		self.assertEqual(dt1.year, dt2.year)
		self.assertEqual(dt1.month, dt2.month)
		self.assertEqual(dt1.day, dt2.day)
		self.assertEqual(dt1.hour, dt2.hour)
		self.assertEqual(dt1.minute, dt2.minute)
		self.assertEqual(dt1.seconds, dt2.seconds)
		
		dblVal = FdoDoubleValue.Create(1.0)
		pDouble = FdoPropertyValue.Create("DOUBLE", dblVal)
		dblVal2 = pDouble.GetValue()
		self.assertEqual("FdoDoubleValue", dblVal2.__class__.__name__)
		self.assertEqual(dblVal.Double, dblVal2.Double)

		i16Val = FdoInt16Value.Create(1)
		pInt16 = FdoPropertyValue.Create("INT16", i16Val)
		i16Val2 = pInt16.GetValue()
		self.assertEqual("FdoInt16Value", i16Val2.__class__.__name__)
		self.assertEqual(i16Val.Int16, i16Val2.Int16)

		i32Val = FdoInt32Value.Create(1)
		pInt32 = FdoPropertyValue.Create("INT32", i32Val)
		i32Val2 = pInt32.GetValue()
		self.assertEqual("FdoInt32Value", i32Val2.__class__.__name__)
		self.assertEqual(i32Val.Int32, i32Val2.Int32)

		i64Val = FdoInt64Value.Create(1)
		pInt64 = FdoPropertyValue.Create("INT64", i64Val)
		i64Val2 = pInt64.GetValue()
		self.assertEqual("FdoInt64Value", i64Val2.__class__.__name__)
		self.assertEqual(i64Val.Int64, i64Val2.Int64)

		singVal = FdoSingleValue.Create(1.0)
		pSingle = FdoPropertyValue.Create("SINGLE", singVal)
		singVal2 = pSingle.GetValue()
		self.assertEqual("FdoSingleValue", singVal2.__class__.__name__)
		self.assertEqual(singVal.Single, singVal2.Single)

		strVal = FdoStringValue.Create("Hello World")
		pString = FdoPropertyValue.Create("STRING", strVal)
		strVal2 = pString.GetValue()
		self.assertEqual("FdoStringValue", strVal2.__class__.__name__)
		self.assertEqual(strVal.String, strVal2.String)

		gf = FdoFgfGeometryFactory.GetInstance()
		geom = gf.CreateGeometry("POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))")
		fgf = gf.GetFgfBytes(geom)
		geomVal = FdoGeometryValue.Create(fgf)
		pGeom = FdoPropertyValue.Create("GEOMETRY", geomVal)
		geomVal2 = pGeom.GetValue()
		self.assertEqual("FdoGeometryValue", geomVal2.__class__.__name__)

	def testLiteralValue(self):
		"""
		Test the out typemap for FdoLiteralValue
		"""
		boolVal = FdoBooleanValue.Create(True)
		pBool = FdoParameterValue.Create("BOOL", boolVal)
		boolVal2 = pBool.GetValue()
		self.assertEqual("FdoBooleanValue", boolVal2.__class__.__name__)
		self.assertEqual(boolVal.Boolean, boolVal2.Boolean)

		bVal = FdoByteValue.Create(1)
		pByte = FdoParameterValue.Create("BYTE", bVal)
		bVal2 = pByte.GetValue()
		self.assertEqual("FdoByteValue", bVal2.__class__.__name__)
		self.assertEqual(bVal.Byte, bVal2.Byte)

		#FIXME: SWIG reports FdoDateTime is memory leaking because there's no dtor and SWIG is
		#instructed not to create any
		dt = FdoDateTime()
		dtVal = FdoDateTimeValue.Create(dt)
		pDateTime = FdoParameterValue.Create("DATETIME", dtVal)
		dtVal2 = pDateTime.GetValue()
		self.assertEqual("FdoDateTimeValue", dtVal.__class__.__name__)
		dt1 = dtVal.GetDateTime()
		dt2 = dtVal2.GetDateTime();
		self.assertEqual(dt1.year, dt2.year)
		self.assertEqual(dt1.month, dt2.month)
		self.assertEqual(dt1.day, dt2.day)
		self.assertEqual(dt1.hour, dt2.hour)
		self.assertEqual(dt1.minute, dt2.minute)
		self.assertEqual(dt1.seconds, dt2.seconds)
		
		dblVal = FdoDoubleValue.Create(1.0)
		pDouble = FdoParameterValue.Create("DOUBLE", dblVal)
		dblVal2 = pDouble.GetValue()
		self.assertEqual("FdoDoubleValue", dblVal2.__class__.__name__)
		self.assertEqual(dblVal.Double, dblVal2.Double)

		i16Val = FdoInt16Value.Create(1)
		pInt16 = FdoParameterValue.Create("INT16", i16Val)
		i16Val2 = pInt16.GetValue()
		self.assertEqual("FdoInt16Value", i16Val2.__class__.__name__)
		self.assertEqual(i16Val.Int16, i16Val2.Int16)

		i32Val = FdoInt32Value.Create(1)
		pInt32 = FdoParameterValue.Create("INT32", i32Val)
		i32Val2 = pInt32.GetValue()
		self.assertEqual("FdoInt32Value", i32Val2.__class__.__name__)
		self.assertEqual(i32Val.Int32, i32Val2.Int32)

		i64Val = FdoInt64Value.Create(1)
		pInt64 = FdoParameterValue.Create("INT64", i64Val)
		i64Val2 = pInt64.GetValue()
		self.assertEqual("FdoInt64Value", i64Val2.__class__.__name__)
		self.assertEqual(i64Val.Int64, i64Val2.Int64)

		singVal = FdoSingleValue.Create(1.0)
		pSingle = FdoParameterValue.Create("SINGLE", singVal)
		singVal2 = pSingle.GetValue()
		self.assertEqual("FdoSingleValue", singVal2.__class__.__name__)
		self.assertEqual(singVal.Single, singVal2.Single)

		strVal = FdoStringValue.Create("Hello World")
		pString = FdoParameterValue.Create("STRING", strVal)
		strVal2 = pString.GetValue()
		self.assertEqual("FdoStringValue", strVal2.__class__.__name__)
		self.assertEqual(strVal.String, strVal2.String)

		gf = FdoFgfGeometryFactory.GetInstance()
		geom = gf.CreateGeometry("POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))")
		fgf = gf.GetFgfBytes(geom)
		geomVal = FdoGeometryValue.Create(fgf)
		pGeom = FdoParameterValue.Create("GEOMETRY", geomVal)
		geomVal2 = pGeom.GetValue()
		self.assertEqual("FdoGeometryValue", geomVal2.__class__.__name__)

	def _validateIntList(self, capabilityList):
		"""
		Validate the output from the FDO capabilities API.  There is a 
		'family' of methods with a signature like this:

		GisInt* GetSomething(GisInt32& length)

		This output consists of two things:
			- a C++ 'array' of integers or enumerations
			- the length of the array

		The typemapping in Python will translate the output into
		a two-element List with two things:
			- a Python Tuple containing the contents of the C++ array
			- the length of the array as a number

		This function validates that the first item in the list is a
		Tuple, and the second value is an integer.

		Arguments:
			capabilityList - the list created by an FDO Get... function

		Returns:
			None

		Raises an exception is there's a problem
		"""
		self.assert_(isinstance(capabilityList, list))
		self.assert_(isinstance(capabilityList[0], tuple))
		self.assert_(isinstance(capabilityList[1], int))
		self.assert_(len(capabilityList[0]) == capabilityList[1])
		

	def _validateUnicodeList(self, capabilityList):
		"""
		Validate the output from the FDO capabilities API.
		There is a  'family' of methods with a signature like this:

		wchar_t** GetSomething(GisInt32& length)

		This output consists of two things:
			- a C++ 'array' of wide char strings
			- the length of the array

		The typemapping in Python will translate the output into
		a two-element List with two things:
			- a Python Tuple containing the contents of the C++ array
			- the length of the array as a number

		This function validates that the first item in the list is a
		Tuple, and the second value is an integer.

		Arguments:
			capabilityList - the list created by an FDO Get... function

		Returns:
			None

		Raises an exception is there's a problem
		"""		
		self.assert_(isinstance(capabilityList, list))
		self.assert_(isinstance(capabilityList[0], tuple))				
		self.assert_(isinstance(capabilityList[1], int))
		self.assert_(len(capabilityList[0]) == capabilityList[1])
		
		# If the capability list has at least one item, check the type
		if capabilityList[1] > 0:
			self.assert_(isinstance(capabilityList[0][0], unicode))


	def _getSdfProvider(self):
		"""
		Returns an instance of the SDF provider.
		"""

		retVal = None

		registry = FdoFeatureAccessManager.GetProviderRegistry()
		providers = registry.GetProviders()

		# Iterate through the provider registry, and find
		# providers with 'Sdf' in their name
		for index in range(providers.Count):
			provider = providers.GetItem(index)

			if string.find(provider.Name, "OSGeo.SDF.3.9") > -1:
				retVal = provider
				break
	
		if provider is None:
			raise RuntimeError("Cannot find the SDF provider")

		return provider
