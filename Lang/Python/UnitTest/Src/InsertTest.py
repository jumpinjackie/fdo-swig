import traceback
import string
import os
import sys
import shutil
from FDO import *
import unittest

class InsertTest(unittest.TestCase):
    def setUp(self):
        if not os.path.isdir("InsertTest/SDF"):
            os.makedirs("InsertTest/SDF")
        if not os.path.isdir("InsertTest/SQLite"):
            os.makedirs("InsertTest/SQLite")
        if os.path.isdir("InsertTest/SHP"):
            os.rmdir("InsertTest/SHP")
        shutil.copyfile("../../../../TestData/SDF/World_Countries.sdf", "InsertTest/SDF/InsertTest.sdf")
        shutil.copyfile("../../../../TestData/SQLite/World_Countries.sqlite", "InsertTest/SQLite/InsertTest.sqlite")
        shutil.copytree("../../../../TestData/SHP", "InsertTest/SHP")

    def tearDown(self):
        shutil.rmtree("InsertTest")

    def _getInsertedFeatures(self, insertCmd):
        reader = insertCmd.Execute()
        inserted = 0
        try:
            while (reader.ReadNext()):
                inserted += 1
        finally:
            reader.Close()
        return inserted

    def _getFeatureCountForName(self, conn, name):
        selectCmd = conn.CreateCommand(FdoCommandType_Select)
        self.assertIsNotNone(selectCmd)
        selectCmd.SetFeatureClassName("World_Countries")
        selectCmd.SetFilter("NAME = '" + name + "'")
        selReader = selectCmd.Execute()
        count = 0
        try:
            while (selReader.ReadNext()):
                count += 1
        finally:
            selReader.Close()
        return count

    def _getFeatureCountForMapKey(self, conn, key):
        selectCmd = conn.CreateCommand(FdoCommandType_Select)
        self.assertIsNotNone(selectCmd)
        selectCmd.SetFeatureClassName("World_Countries")
        selectCmd.SetFilter("MAPKEY = '" + key + "'")
        selReader = selectCmd.Execute()
        count = 0
        try:
            while (selReader.ReadNext()):
                count += 1
        finally:
            selReader.Close()
        return count

    def _doInsert(self, conn):
        geomName = None
        desc = conn.CreateCommand(FdoCommandType_DescribeSchema)
        self.assertIsNotNone(desc)
        schemas = desc.Execute()
        self.assertIsNotNone(schemas)
        clsDef = schemas.GetClassDefinition(None, "World_Countries")
        self.assertIsNotNone(clsDef)
        geomProp = clsDef.GetGeometryProperty()
        self.assertIsNotNone(geomProp)
        geomName = geomProp.Name
        insertCmd = conn.CreateCommand(FdoCommandType_Insert)
        insertCmd.SetFeatureClassName("World_Countries")
        geomFactory = FdoFgfGeometryFactory.GetInstance()
        propVals = insertCmd.GetPropertyValues()
        nameVal = FdoStringValue.Create()
        self.assertTrue(nameVal.IsNull())
        keyVal = FdoStringValue.Create()
        self.assertTrue(keyVal.IsNull())
        mapkeyVal = FdoStringValue.Create()
        self.assertTrue(mapkeyVal.IsNull())
        geomVal = FdoGeometryValue.Create()
        self.assertTrue(geomVal.IsNull())
        pName = FdoPropertyValue.Create("NAME", nameVal)
        pKey = FdoPropertyValue.Create("KEY", keyVal)
        pMapKey = FdoPropertyValue.Create("MAPKEY", mapkeyVal)
        pGeom = FdoPropertyValue.Create(geomName, geomVal)
        propVals.Add(pName)
        propVals.Add(pKey)
        propVals.Add(pMapKey)
        propVals.Add(pGeom)
        #set the actual values
        nameVal.String = "My Own Country"
        keyVal.String = "MOC"
        mapkeyVal.String = "MOC123"
        geom = geomFactory.CreateGeometry("POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))")
        fgf = geomFactory.GetFgfBytes(geom)
        geomVal.SetGeometryBytes(fgf)
        inserted = self._getInsertedFeatures(insertCmd)
        self.assertEqual(1, inserted)
        count = self._getFeatureCountForName(conn, "My Own Country")
        self.assertEqual(1, count)
        mapkeyVal.String = "MOC234"
        self.assertEqual(1, self._getInsertedFeatures(insertCmd))
        self.assertEqual(2, self._getFeatureCountForName(conn, "My Own Country"))
        self.assertEqual(1, self._getFeatureCountForMapKey(conn, "MOC123"))
        self.assertEqual(1, self._getFeatureCountForMapKey(conn, "MOC234"))

    def testSDFInsert(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SDF")
        conn.ConnectionString = "File=InsertTest/SDF/InsertTest.sdf"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doInsert(conn)
        finally:
            conn.Close()

    def testSHPInsert(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SHP")
        conn.ConnectionString = "DefaultFileLocation=InsertTest/SHP"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doInsert(conn)
        finally:
            conn.Close()

    def testSQLiteInsert(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SQLite")
        conn.ConnectionString = "File=InsertTest/SQLite/InsertTest.sqlite"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doInsert(conn)
        finally:
            conn.Close()