import traceback
import string
import os.path
import sys
from FDO import *
import unittest

class SchemaTest(unittest.TestCase):
    def _verifyClass(self, clsDef):
        self.assertEqual(FdoClassType_FeatureClass, clsDef.ClassType)
        clsProps = clsDef.GetProperties()
        self.assertEqual(5, clsProps.Count)
        clsIdProps = clsDef.GetIdentityProperties()
        self.assertEqual(1, clsIdProps.Count)
        geomProp = clsDef.GetGeometryProperty()
        self.assertIsNotNone(geomProp)
        self.assertTrue(clsProps.IndexOf("NAME") >= 0)
        self.assertTrue(clsProps.IndexOf("KEY") >= 0)
        self.assertTrue(clsProps.IndexOf("MAPKEY") >= 0)
        self.assertTrue(clsProps.IndexOf(geomProp) >= 0)
        name = clsProps.GetItem("NAME")
        key = clsProps.GetItem("KEY")
        mapkey = clsProps.GetItem("MAPKEY")
        self.assertEqual(name.PropertyType, FdoPropertyType_DataProperty)
        self.assertEqual(key.PropertyType, FdoPropertyType_DataProperty)
        self.assertEqual(mapkey.PropertyType, FdoPropertyType_DataProperty)

    def testSDFSchema(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SDF")
        conn.ConnectionString = "File=../../../../TestData/SDF/World_Countries.sdf"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            desc = conn.CreateCommand(FdoCommandType_DescribeSchema)
            self.assertIsNotNone(desc)
            schemas = desc.Execute()
            self.assertEqual(1, schemas.Count)
            schema = schemas.GetItem(0)
            classes = schema.GetClasses()
            self.assertEqual(1, classes.Count)
            clsDef = classes.GetItem(0)
            self._verifyClass(clsDef)
            #re-test with sugar
            clsDef1 = schemas.GetClassDefinition(schema.Name, "World_Countries")
            self.assertIsNotNone(clsDef1)
            self._verifyClass(clsDef1)
            clsDef2 = schemas.GetClassDefinition(None, "World_Countries")
            self.assertIsNotNone(clsDef2)
            self._verifyClass(clsDef2)
            try:
                clsDef3 = schemas.GetClassDefinition(None, "WorldCountries")
                raise AssertionError("Expected exception thrown when class is not found")
            except:
                pass
            try:
                clsDef4 = schemas.GetClassDefinition("BogusSchema", "World_Countries")
                raise AssertionError("Expected exception thrown when class is not found")
            except:
                pass
        finally:
            conn.Close()

    def testSHPSchema(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SHP")
        conn.ConnectionString = "DefaultFileLocation=../../../../TestData/SHP/World_Countries.shp"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            desc = conn.CreateCommand(FdoCommandType_DescribeSchema)
            self.assertIsNotNone(desc)
            schemas = desc.Execute()
            self.assertEqual(1, schemas.Count)
            schema = schemas.GetItem(0)
            classes = schema.GetClasses()
            self.assertEqual(1, classes.Count)
            clsDef = classes.GetItem(0)
            self._verifyClass(clsDef)
            #re-test with sugar
            clsDef1 = schemas.GetClassDefinition(schema.Name, "World_Countries")
            self.assertIsNotNone(clsDef1)
            self._verifyClass(clsDef1)
            clsDef2 = schemas.GetClassDefinition(None, "World_Countries")
            self.assertIsNotNone(clsDef2)
            self._verifyClass(clsDef2)
            try:
                clsDef3 = schemas.GetClassDefinition(None, "WorldCountries")
                raise AssertionError("Expected exception thrown when class is not found")
            except:
                pass
            try:
                clsDef4 = schemas.GetClassDefinition("BogusSchema", "World_Countries")
                raise AssertionError("Expected exception thrown when class is not found")
            except:
                pass
        finally:
            conn.Close()

    def testSQLiteSchema(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SQLite")
        conn.ConnectionString = "File=../../../../TestData/SQLite/World_Countries.sqlite"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            desc = conn.CreateCommand(FdoCommandType_DescribeSchema)
            self.assertIsNotNone(desc)
            schemas = desc.Execute()
            self.assertEqual(1, schemas.Count)
            schema = schemas.GetItem(0)
            classes = schema.GetClasses()
            self.assertEqual(1, classes.Count)
            clsDef = classes.GetItem(0)
            self._verifyClass(clsDef)
            #re-test with sugar
            clsDef1 = schemas.GetClassDefinition(schema.Name, "World_Countries")
            self.assertIsNotNone(clsDef1)
            self._verifyClass(clsDef1)
            clsDef2 = schemas.GetClassDefinition(None, "World_Countries")
            self.assertIsNotNone(clsDef2)
            self._verifyClass(clsDef2)
            try:
                clsDef3 = schemas.GetClassDefinition(None, "WorldCountries")
                raise AssertionError("Expected exception thrown when class is not found")
            except:
                pass
            try:
                clsDef4 = schemas.GetClassDefinition("BogusSchema", "World_Countries")
                raise AssertionError("Expected exception thrown when class is not found")
            except:
                pass
        finally:
            conn.Close()