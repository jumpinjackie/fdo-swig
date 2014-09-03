import traceback
import string
import os
import sys
import shutil
from FDO import *
import unittest

class DeleteTest(unittest.TestCase):
    def setUp(self):
        if not os.path.isdir("DeleteTest/SDF"):
            os.makedirs("DeleteTest/SDF")
        if not os.path.isdir("DeleteTest/SQLite"):
            os.makedirs("DeleteTest/SQLite")
        if os.path.isdir("DeleteTest/SHP"):
            os.rmdir("DeleteTest/SHP")
        shutil.copyfile("../../../../TestData/SDF/World_Countries.sdf", "DeleteTest/SDF/DeleteTest.sdf")
        shutil.copyfile("../../../../TestData/SQLite/World_Countries.sqlite", "DeleteTest/SQLite/DeleteTest.sqlite")
        shutil.copytree("../../../../TestData/SHP", "DeleteTest/SHP")

    def tearDown(self):
        shutil.rmtree("DeleteTest")

    def _doDelete(self, conn):
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
        deleteCmd = conn.CreateCommand(FdoCommandType_Delete)
        self.assertIsNotNone(deleteCmd)
        deleteCmd.SetFeatureClassName("World_Countries")
        deleteCmd.SetFilter("NAME = 'Canada'")
        deleted = deleteCmd.Execute()
        self.assertEqual(66, deleted)
        selectCmd = conn.CreateCommand(FdoCommandType_Select)
        self.assertIsNotNone(selectCmd)
        selectCmd.SetFeatureClassName("World_Countries")
        selectCmd.SetFilter("NAME = 'Canada'")
        fr = selectCmd.Execute()
        try:
            self.assertFalse(fr.ReadNext())
        finally:
            fr.Close()

    def testSDFDelete(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SDF")
        conn.ConnectionString = "File=DeleteTest/SDF/DeleteTest.sdf"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doDelete(conn)
        finally:
            conn.Close()

    def testSHPDelete(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SHP")
        conn.ConnectionString = "DefaultFileLocation=DeleteTest/SHP"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doDelete(conn)
        finally:
            conn.Close()

    def testSQLiteDelete(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SQLite")
        conn.ConnectionString = "File=DeleteTest/SQLite/DeleteTest.sqlite"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doDelete(conn)
        finally:
            conn.Close()