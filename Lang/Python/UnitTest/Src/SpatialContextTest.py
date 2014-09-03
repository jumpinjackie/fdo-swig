import traceback
import string
import os.path
import sys
from FDO import *
import unittest

class SpatialContextTest(unittest.TestCase):
    def _doGetSpatialContexts(self, conn):
        cmd = conn.CreateCommand(FdoCommandType_GetSpatialContexts)
        self.assertIsNotNone(cmd)
        scReader = cmd.Execute()
        count = 0
        while scReader.ReadNext():
            self.assertIsNotNone(scReader.Name)
            self.assertNotEqual(scReader.Name, "")
            count += 1
        self.assertEqual(1, count)

    def testSDFSpatialContext(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SDF")
        conn.ConnectionString = "File=../../../../TestData/SDF/World_Countries.sdf"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doGetSpatialContexts(conn)
        finally:
            conn.Close()

    def testSHPSpatialContext(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SHP")
        conn.ConnectionString = "DefaultFileLocation=../../../../TestData/SHP/World_Countries.shp"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doGetSpatialContexts(conn)
        finally:
            conn.Close()

    def testSQLiteSpatialContext(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SQLite")
        conn.ConnectionString = "File=../../../../TestData/SQLite/World_Countries.sqlite"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doGetSpatialContexts(conn)
        finally:
            conn.Close()