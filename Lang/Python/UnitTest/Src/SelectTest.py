import traceback
import string
import os.path
import sys
from FDO import *
import unittest

class SelectTest(unittest.TestCase):
    def _testSelectCommand(self, selectCmd):
        selectCmd.SetFeatureClassName("World_Countries")
        reader = selectCmd.Execute()
        try:
            count = 0
            clsDef = reader.GetClassDefinition()
            self.assertIsNotNone(clsDef)
            geomFact = FdoFgfGeometryFactory.GetInstance()
            geomProp = clsDef.GetGeometryProperty()
            geomName = geomProp.Name
            self.assertIsNotNone(geomProp)
            while (reader.ReadNext()):
                name = None
                key = None
                mapkey = None
                if not reader.IsNull("NAME"):
                    name = reader.GetString("NAME")
                if not reader.IsNull("KEY"):
                    key = reader.GetString("KEY")
                if not reader.IsNull("MAPKEY"):
                    mapkey = reader.GetString("MAPKEY")
                if not reader.IsNull(geomName):
                    fgf = reader.GetGeometryBytes(geomName)
                    self.assertIsNotNone(fgf)
                    geom = geomFact.CreateGeometryFromFgf(fgf)
                    self.assertIsNotNone(geom)
                    wkt = geom.Text
                    self.assertIsNotNone(wkt)
                    self.assertNotEqual(wkt, "")
                count += 1
            self.assertEqual(419, count)
        finally:
            reader.Close()

    def _testFilteredSelectCommand(self, selectCmd):
        selectCmd.SetFeatureClassName("World_Countries")
        selectCmd.SetFilter("NAME = 'Canada'")
        reader = selectCmd.Execute()
        try:
            count = 0
            clsDef = reader.GetClassDefinition()
            self.assertIsNotNone(clsDef)
            geomFact = FdoFgfGeometryFactory.GetInstance()
            geomProp = clsDef.GetGeometryProperty()
            geomName = geomProp.Name
            self.assertIsNotNone(geomProp)
            while (reader.ReadNext()):
                name = None
                key = None
                mapkey = None
                if not reader.IsNull("NAME"):
                    name = reader.GetString("NAME")
                if not reader.IsNull("KEY"):
                    key = reader.GetString("KEY")
                if not reader.IsNull("MAPKEY"):
                    mapkey = reader.GetString("MAPKEY")
                if not reader.IsNull(geomName):
                    fgf = reader.GetGeometryBytes(geomName)
                    self.assertIsNotNone(fgf)
                    geom = geomFact.CreateGeometryFromFgf(fgf)
                    self.assertIsNotNone(geom)
                    wkt = geom.Text
                    self.assertIsNotNone(wkt)
                    self.assertNotEqual(wkt, "")
                count += 1
            self.assertEqual(66, count)
        finally:
            reader.Close()

    def testSDFSelect(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SDF")
        conn.ConnectionString = "File=../../../../TestData/SDF/World_Countries.sdf"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            selectCmd = conn.CreateCommand(FdoCommandType_Select)
            self.assertIsNotNone(selectCmd)
            self._testSelectCommand(selectCmd)
        finally:
            conn.Close()

    def testSDFSelectFiltered(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SDF")
        conn.ConnectionString = "File=../../../../TestData/SDF/World_Countries.sdf"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            selectCmd = conn.CreateCommand(FdoCommandType_Select)
            self.assertIsNotNone(selectCmd)
            self._testFilteredSelectCommand(selectCmd)
        finally:
            conn.Close()

    def testSHPSelect(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SHP")
        conn.ConnectionString = "DefaultFileLocation=../../../../TestData/SHP/World_Countries.shp"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            selectCmd = conn.CreateCommand(FdoCommandType_Select)
            self.assertIsNotNone(selectCmd)
            self._testSelectCommand(selectCmd)
        finally:
            conn.Close()

    def testSHPSelectFiltered(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SHP")
        conn.ConnectionString = "DefaultFileLocation=../../../../TestData/SHP/World_Countries.shp"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            selectCmd = conn.CreateCommand(FdoCommandType_Select)
            self.assertIsNotNone(selectCmd)
            self._testFilteredSelectCommand(selectCmd)
        finally:
            conn.Close()

    def testSQLiteSelect(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SQLite")
        conn.ConnectionString = "File=../../../../TestData/SQLite/World_Countries.sqlite"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            selectCmd = conn.CreateCommand(FdoCommandType_Select)
            self.assertIsNotNone(selectCmd)
            self._testSelectCommand(selectCmd)
        finally:
            conn.Close()

    def testSQLiteSelectFiltered(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SQLite")
        conn.ConnectionString = "File=../../../../TestData/SQLite/World_Countries.sqlite"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            selectCmd = conn.CreateCommand(FdoCommandType_Select)
            self.assertIsNotNone(selectCmd)
            self._testFilteredSelectCommand(selectCmd)
        finally:
            conn.Close()