import traceback
import string
import os
import sys
import shutil
from FDO import *
import unittest

class UpdateTest(unittest.TestCase):
    def setUp(self):
        if not os.path.isdir("UpdateTest/SDF"):
            os.makedirs("UpdateTest/SDF")
        if not os.path.isdir("UpdateTest/SQLite"):
            os.makedirs("UpdateTest/SQLite")
        if os.path.isdir("UpdateTest/SHP"):
            os.rmdir("UpdateTest/SHP")
        shutil.copyfile("../../../../TestData/SDF/World_Countries.sdf", "UpdateTest/SDF/UpdateTest.sdf")
        shutil.copyfile("../../../../TestData/SQLite/World_Countries.sqlite", "UpdateTest/SQLite/UpdateTest.sqlite")
        shutil.copytree("../../../../TestData/SHP", "UpdateTest/SHP")

    def tearDown(self):
        shutil.rmtree("UpdateTest")

    def _doUpdate(self, conn):
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
        updateCmd = conn.CreateCommand(FdoCommandType_Update)
        self.assertIsNotNone(updateCmd)
        updateCmd.SetFeatureClassName("World_Countries")
        updateCmd.SetFilter("NAME = 'Canada'")
        geomFactory = FdoFgfGeometryFactory.GetInstance()
        propVals = updateCmd.GetPropertyValues()
        nameVal = FdoStringValue.Create()
        self.assertTrue(nameVal.IsNull())
        keyVal = FdoStringValue.Create()
        self.assertTrue(keyVal.IsNull())
        mapkeyVal = FdoStringValue.Create()
        self.assertTrue(mapkeyVal.IsNull())
        geomVal = FdoGeometryValue.Create()
        self.assertTrue(geomVal.IsNull())
        pKey = FdoPropertyValue.Create("KEY", keyVal)
        pMapKey = FdoPropertyValue.Create("MAPKEY", mapkeyVal)
        propVals.Add(pKey)
        propVals.Add(pMapKey)
        #set the actual values
        keyVal.String = "MOC"
        mapkeyVal.String = "MOC123"
        updated = updateCmd.Execute()
        self.assertEqual(66, updated)
        selectCmd = conn.CreateCommand(FdoCommandType_Select)
        self.assertIsNotNone(selectCmd)
        selectCmd.SetFeatureClassName("World_Countries")
        selectCmd.SetFilter("NAME = 'Canada'")
        fr = selectCmd.Execute()
        try:
            while (fr.ReadNext()):
                self.assertFalse(fr.IsNull("KEY"))
                self.assertFalse(fr.IsNull("MAPKEY"))
                self.assertEqual(fr.GetString("KEY"), "MOC")
                self.assertEqual(fr.GetString("MAPKEY"), "MOC123")
        finally:
            fr.Close()

    def testSDFUpdate(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SDF")
        conn.ConnectionString = "File=UpdateTest/SDF/UpdateTest.sdf"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doUpdate(conn)
        finally:
            conn.Close()

    def testSHPUpdate(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SHP")
        conn.ConnectionString = "DefaultFileLocation=UpdateTest/SHP"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doUpdate(conn)
        finally:
            conn.Close()

    def testSQLiteUpdate(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SQLite")
        conn.ConnectionString = "File=UpdateTest/SQLite/UpdateTest.sqlite"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doUpdate(conn)
        finally:
            conn.Close()