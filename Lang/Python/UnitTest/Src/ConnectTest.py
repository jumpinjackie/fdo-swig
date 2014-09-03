import traceback
import string
import os.path
import sys
from FDO import *
import unittest

class ConnectTest(unittest.TestCase):
    """
    Unit test for connecting to sample test data stores
    """
    def testSDF(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SDF")
        conn.ConnectionString = "File=../../../../TestData/SDF/World_Countries.sdf"
        self.assert_(conn.ConnectionString, "File=../../../../TestData/SDF/World_Countries.sdf")
        self.assert_(conn.Open(), FdoConnectionState_Open)
        conn.Close()
        conn.ConnectionString = "File=../IDontExist.sdf"
        try:
            conn.Open()
        except:
            return None

        raise AssertionError("Exception expected from failed connect")

    def testSHP(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SHP")
        conn.ConnectionString = "DefaultFileLocation=../../../../TestData/SHP/World_Countries.shp"
        self.assert_(conn.ConnectionString, "File=../../../../TestData/SHP/World_Countries.shp")
        self.assert_(conn.Open(), FdoConnectionState_Open)
        conn.Close()
        conn.ConnectionString = "DefaultFileLocation=../IDontExist.shp"
        try:
            conn.Open()
        except:
            return None

        raise AssertionError("Exception expected from failed connect")

    def testSQLite(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SQLite")
        conn.ConnectionString = "File=../../../../TestData/SQLite/World_Countries.sqlite"
        self.assert_(conn.ConnectionString, "File=../../../../TestData/SQLite/World_Countries.sqlite")
        self.assert_(conn.Open(), FdoConnectionState_Open)
        conn.Close()
        conn.ConnectionString = "File=../IDontExist.sqlite"
        try:
            conn.Open()
        except:
            return None

        raise AssertionError("Exception expected from failed connect")