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
from FDO import *
import unittest

class CapabilitiesTest(unittest.TestCase):
	"""
	Unit test for handling FDO capabilities in Python
	"""
	def testCapabilities(self):
		# ArgumentDefinition
		argue1 = FdoArgumentDefinition.Create("Arg1", "Argument 1 (Boolean)", FdoDataType_Boolean)
		argue2 = FdoArgumentDefinition.Create("Arg2", "Argument 2 (Byte)", FdoDataType_Byte);
		argue3 = FdoArgumentDefinition.Create("Arg3", "Argument 3 (BLOB)", FdoDataType_BLOB);
		argue4 = FdoArgumentDefinition.Create("Arg4", "Argument 4 (Int64)", FdoDataType_Int64);

		self.assert_(argue1.Name, "Arg1")
		self.assert_(argue1.Description, "Argument 1 (Boolean)")
		self.assertEquals(argue1.DataType, FdoDataType_Boolean)
		self.assert_(argue2.Name, "Arg2")
		self.assert_(argue2.Description, "Argument 2 (Byte)")
		self.assertEquals(argue2.DataType, FdoDataType_Byte)
		self.assert_(argue3.Name, "Arg3")
		self.assert_(argue3.Description, "Argument 3 (BLOB)")
		self.assertEquals(argue3.DataType, FdoDataType_BLOB)
		self.assert_(argue4.Name, "Arg4")
		self.assert_(argue4.Description, "Argument 4 (Int64)")
		self.assertEquals(argue4.DataType, FdoDataType_Int64)

		# ArgumentDefinitionCollection
		collect = FdoArgumentDefinitionCollection.Create()
		collect.Add(argue1)
		collect.Add(argue2)
		collect.Add(argue3)
		collect.Add(argue4)

		# FunctionDefinition
		fun = FdoFunctionDefinition.Create("Function", "Test of FdoFunctionDefinition", FdoDataType_Double, collect)

		# ReadOnlyArgumentDefinitionCollection
		readonlyargues = fun.GetArguments()
		count = readonlyargues.GetCount()
		self.assertEquals(count, 4)

		arg = readonlyargues.GetItem(0)
		self.assert_(arg.Name, argue1)

		self.assert_(readonlyargues.Contains(argue1))

		arguement = readonlyargues.GetItem("Arg2");
		self.assert_(arguement.Name, "Arg2");

		self.assert_(fun.ReturnType, FdoDataType_Double)
		self.assert_(fun.Name, "Function")
		self.assert_(fun.Description, "Test of FdoFunctionDefinition")

	def testSDFCapabilities(self):
		connMgr = FdoFeatureAccessManager.GetConnectionManager()
		conn = connMgr.CreateConnection("OSGeo.SDF")
		conn.ConnectionString = "File=../../../../TestData/SDF/World_Countries.sdf"
		self.assert_(conn.Open(), FdoConnectionState_Open)
		try:
			self._processCommandCapabilities(conn)
			self._processConnectionCapabilities(conn)
			self._processExpressionCapabilities(conn)
			self._processFilterCapabilities(conn)
			self._processGeometryCapabilities(conn)
			self._processRasterCapabilities(conn)
			self._processSchemaCapabilities(conn)
			self._processTopologyCapabilities(conn)
		finally:
			conn.Close()

	def testSHPCapabilities(self):
		connMgr = FdoFeatureAccessManager.GetConnectionManager()
		conn = connMgr.CreateConnection("OSGeo.SHP")
		conn.ConnectionString = "DefaultFileLocation=../../../../TestData/SHP/World_Countries.shp"
		self.assert_(conn.Open(), FdoConnectionState_Open)
		try:
			self._processCommandCapabilities(conn)
			self._processConnectionCapabilities(conn)
			self._processExpressionCapabilities(conn)
			self._processFilterCapabilities(conn)
			self._processGeometryCapabilities(conn)
			self._processRasterCapabilities(conn)
			self._processSchemaCapabilities(conn)
			self._processTopologyCapabilities(conn)
		finally:
			conn.Close()

	def testSQLiteCapabilities(self):
		connMgr = FdoFeatureAccessManager.GetConnectionManager()
		conn = connMgr.CreateConnection("OSGeo.SQLite")
		conn.ConnectionString = "File=../../../../TestData/SQLite/World_Countries.sqlite"
		self.assert_(conn.Open(), FdoConnectionState_Open)
		try:
			self._processCommandCapabilities(conn)
			self._processConnectionCapabilities(conn)
			self._processExpressionCapabilities(conn)
			self._processFilterCapabilities(conn)
			self._processGeometryCapabilities(conn)
			self._processRasterCapabilities(conn)
			self._processSchemaCapabilities(conn)
			self._processTopologyCapabilities(conn)
		finally:
			conn.Close()

	def _processCommandCapabilities(self, conn):
		caps = conn.GetCommandCapabilities()
		cmds = caps.SupportedCommands()
		for i in range(cmds.Count):
			cmdType = cmds.GetItem(i);
			# Skip provider-specific commands
			if cmdType > FdoCommandType_FirstProviderCommand:
				continue
			try:
				cmd = conn.CreateCommand(cmdType)
			except:
				raise AssertionError("Could not create command of type " + str(cmdType))
			bSupportsParameters = caps.SupportsParameters()
			bSupportsSelectDistinct = caps.SupportsSelectDistinct()
			bSupportsSelectExpressions = caps.SupportsSelectExpressions()
			bSupportsSelectFunctions = caps.SupportsSelectFunctions()
			bSupportsSelectGrouping = caps.SupportsSelectGrouping()
			bSupportsSelectOrdering = caps.SupportsSelectOrdering()
			bSupportsTimeout = caps.SupportsTimeout()

	def _processConnectionCapabilities(self, conn):
		caps = conn.GetConnectionCapabilities()
		jtypes = caps.GetJoinTypes()
		ltypes = caps.SupportedLockTypes()
		for i in range(ltypes.Count):
			ltype = ltypes.GetItem(i)
		sceTypes = caps.SupportedSpatialContextExtentTypes()
		for i in range(sceTypes.Count):
			sceType = sceTypes.GetItem(i)
		bSupportsConfiguration = caps.SupportsConfiguration()
		bSupportsCSysWKTFromCSysName = caps.SupportsCSysWKTFromCSysName()
		bSupportsFlush = caps.SupportsFlush()
		bSupportsJoins = caps.SupportsJoins()
		bSupportsLocking = caps.SupportsLocking()
		bSupportsLongTransactions = caps.SupportsLongTransactions()
		bSupportsMultipleSpatialContexts = caps.SupportsMultipleSpatialContexts()
		bSupportsMultiUserWrite = caps.SupportsMultiUserWrite()
		bSupportsSavePoint = caps.SupportsSavePoint()
		bSupportsSQL = caps.SupportsSQL()
		bSupportsSubSelects = caps.SupportsSubSelects()
		bSupportsTimeout = caps.SupportsTimeout()
		bSupportsTransactions = caps.SupportsTransactions()
		bSupportsWrite = caps.SupportsWrite()

	def _processExpressionCapabilities(self, conn):
		caps = conn.GetExpressionCapabilities()
		funcs = caps.GetFunctions()
		for i in range(funcs.Count):
			func = funcs.GetItem(i);
			funcName = func.Name
			funcDesc = func.Description
			funcCat = func.FunctionCategoryType
			funcRetType = func.ReturnPropertyType
			bVarArgs = func.SupportsVariableArgumentsList()
			bAggregate = func.IsAggregate()
			args = func.GetArguments()
			for j in range(args.Count):
				arg = args.GetItem(j)
				argName = arg.Name
				argDesc = arg.Description
				argDataType = arg.DataType
				argPropType = arg.PropertyType
				argValList = arg.GetArgumentValueList()
				if not argValList is None:
					argValConstType = argValList.ConstraintType
					argConstList = argValList.GetConstraintList()
					for k in range(argConstList.Count):
						constValue = argConstList.GetItem(k)

	def _processFilterCapabilities(self, conn):
		caps = conn.GetFilterCapabilities()
		condTypes = caps.SupportedConditionTypes()
		for i in range(condTypes.Count):
			condType = condTypes.GetItem(i)
		distOps = caps.SupportedDistanceOperations()
		for i in range(distOps.Count):
			distOp = distOps.GetItem(i)
		spOps = caps.SupportedSpatialOperations()
		for i in range(spOps.Count):
			spOp = spOps.GetItem(i)
		bSupportsGeodesicDistance = caps.SupportsGeodesicDistance()
		bSupportsNonLiteralGeometricOperations = caps.SupportsNonLiteralGeometricOperations()

	def _processGeometryCapabilities(self, conn):
		caps = conn.GetGeometryCapabilities()
		dims = caps.GetDimensionalities()
		geomCompTypes = caps.SupportedGeometryComponentTypes()
		for i in range(geomCompTypes.Count):
			geomCompType = geomCompTypes.GetItem(i)
		geomTypes = caps.SupportedGeometryTypes()
		for i in range(geomTypes.Count):
			geomType = geomTypes.GetItem(i)

	def _processRasterCapabilities(self, conn):
		caps = conn.GetRasterCapabilities()
		bSupportsRaster = caps.SupportsRaster()
		bSupportsStitching = caps.SupportsStitching()
		bSupportsSubsampling = caps.SupportsSubsampling()

	def _processSchemaCapabilities(self, conn):
		caps = conn.GetSchemaCapabilities()
		dataTypes = [FdoDataType_Boolean, FdoDataType_Byte, FdoDataType_DateTime, FdoDataType_Decimal, FdoDataType_Double, FdoDataType_Int16, FdoDataType_Int32, FdoDataType_Int64, FdoDataType_Single, FdoDataType_String, FdoDataType_BLOB, FdoDataType_CLOB]
		for i in range(len(dataTypes)):
			maxLen = caps.GetMaximumDataValueLength(dataTypes[i])
		snTypes = [FdoSchemaElementNameType_Datastore, FdoSchemaElementNameType_Schema, FdoSchemaElementNameType_Class, FdoSchemaElementNameType_Property, FdoSchemaElementNameType_Description]
		for i in range(len(snTypes)):
			snLimit = caps.GetNameSizeLimit(snTypes[i])
		resChars = caps.GetReservedCharactersForName()
		autoGenTypes = caps.SupportedAutogeneratedDataTypes()
		for i in range(autoGenTypes.Count):
			dt = autoGenTypes.GetItem(i)
		clsTypes = caps.SupportedClassTypes()
		for i in range(clsTypes.Count):
			clsType = clsTypes.GetItem(i)
		sDataTypes = caps.SupportedDataTypes()
		for i in range(sDataTypes.Count):
			dt = sDataTypes.GetItem(i)
		sIdTypes = caps.SupportedIdentityPropertyTypes()
		for i in range(sIdTypes.Count):
			dt = sIdTypes.GetItem(i)
		bSupportsAssociationProperties = caps.SupportsAssociationProperties()
		bSupportsAutoIdGeneration = caps.SupportsAutoIdGeneration()
		bSupportsCompositeId = caps.SupportsCompositeId()
		bSupportsCompositeUniqueValueConstraints = caps.SupportsCompositeUniqueValueConstraints()
		bSupportsDataStoreScopeUniqueIdGeneration = caps.SupportsDataStoreScopeUniqueIdGeneration()
		bSupportsDefaultValue = caps.SupportsDefaultValue()
		bSupportsExclusiveValueRangeConstraints = caps.SupportsExclusiveValueRangeConstraints()
		bSupportsInclusiveValueRangeConstraints = caps.SupportsInclusiveValueRangeConstraints()
		bSupportsInheritance = caps.SupportsInheritance()
		bSupportsMultipleSchemas = caps.SupportsMultipleSchemas()
		bSupportsNetworkModel = caps.SupportsNetworkModel()
		bSupportsNullValueConstraints = caps.SupportsNullValueConstraints()
		bSupportsObjectProperties = caps.SupportsObjectProperties()
		bSupportsSchemaModification = caps.SupportsSchemaModification()
		bSupportsSchemaOverrides = caps.SupportsSchemaOverrides()
		bSupportsUniqueValueConstraints = caps.SupportsUniqueValueConstraints()
		bSupportsValueConstraintsList = caps.SupportsValueConstraintsList()

	def _processTopologyCapabilities(self, conn):
		caps = conn.GetTopologyCapabilities()
		bActivatesTopologyByArea = caps.ActivatesTopologyByArea()
		bBreaksCurveCrossingsAutomatically = caps.BreaksCurveCrossingsAutomatically()
		bConstrainsFeatureMovements = caps.ConstrainsFeatureMovements()
		bSupportsTopologicalHierarchy = caps.SupportsTopologicalHierarchy()
		bSupportsTopology = caps.SupportsTopology()