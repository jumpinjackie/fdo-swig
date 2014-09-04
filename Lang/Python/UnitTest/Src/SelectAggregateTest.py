import traceback
import string
import os.path
import sys
from sets import Set
from FDO import *
import unittest

class SelectAggregateTest(unittest.TestCase):
    def _doCount(self, conn):
        selectCmd = conn.CreateCommand(FdoCommandType_SelectAggregates)
        self.assertIsNotNone(selectCmd)
        selectCmd.SetFeatureClassName("World_Countries")
        propNames = selectCmd.GetPropertyNames()
        countExpr = FdoExpression.Parse("COUNT(NAME)")
        countIdent = FdoComputedIdentifier.Create("TOTAL_COUNT", countExpr)
        propNames.Add(countIdent)
        rdr = selectCmd.Execute()
        total = 0
        while rdr.ReadNext():
            total += rdr.GetInt64("TOTAL_COUNT")
        rdr.Close()
        self.assertEqual(419, total)
        #re-test with sugar
        propNames.Clear()
        self.assertEqual(0, propNames.Count)
        propNames.AddComputedIdentifier("TOTAL_COUNT", "COUNT(NAME)")
        self.assertEqual(1, propNames.Count)
        rdr = selectCmd.Execute()
        total = 0
        while rdr.ReadNext():
            total += rdr.GetInt64("TOTAL_COUNT")
        rdr.Close()
        self.assertEqual(419, total)

    def _doFilteredCount(self, conn):
        selectCmd = conn.CreateCommand(FdoCommandType_SelectAggregates)
        self.assertIsNotNone(selectCmd)
        selectCmd.SetFeatureClassName("World_Countries")
        selectCmd.SetFilter("NAME = 'Canada'")
        propNames = selectCmd.GetPropertyNames()
        countExpr = FdoExpression.Parse("COUNT(NAME)")
        countIdent = FdoComputedIdentifier.Create("TOTAL_COUNT", countExpr)
        propNames.Add(countIdent)
        rdr = selectCmd.Execute()
        total = 0
        while rdr.ReadNext():
            total += rdr.GetInt64("TOTAL_COUNT")
        rdr.Close()
        self.assertEqual(66, total)
        #re-test with sugar
        propNames.Clear()
        self.assertEqual(0, propNames.Count)
        propNames.AddComputedIdentifier("TOTAL_COUNT", "COUNT(NAME)")
        self.assertEqual(1, propNames.Count)
        rdr = selectCmd.Execute()
        total = 0
        while rdr.ReadNext():
            total += rdr.GetInt64("TOTAL_COUNT")
        rdr.Close()
        self.assertEqual(66, total)

    def _doDistinct(self, conn):
        selectCmd = conn.CreateCommand(FdoCommandType_SelectAggregates)
        self.assertIsNotNone(selectCmd)
        selectCmd.SetFeatureClassName("World_Countries")
        selectCmd.Distinct = True
        propNames = selectCmd.GetPropertyNames()
        ident = FdoIdentifier.Create("NAME")
        propNames.Add(ident)
        rdr = selectCmd.Execute()
        values = Set()
        total = 0
        while rdr.ReadNext():
            values.add(rdr.GetString("NAME"))
            total += 1
        rdr.Close()
        self.assertEqual(len(values), total)

    def _doFilteredDistinct(self, conn):
        selectCmd = conn.CreateCommand(FdoCommandType_SelectAggregates)
        self.assertIsNotNone(selectCmd)
        selectCmd.SetFeatureClassName("World_Countries")
        selectCmd.SetFilter("NAME = 'Canada'")
        selectCmd.Distinct = True
        propNames = selectCmd.GetPropertyNames()
        ident = FdoIdentifier.Create("NAME")
        propNames.Add(ident)
        rdr = selectCmd.Execute()
        values = Set()
        total = 0
        while rdr.ReadNext():
            values.add(rdr.GetString("NAME"))
            total += 1
        rdr.Close()
        self.assertEqual(len(values), total)

    def _doSpatialExtents(self, conn):
        desc = conn.CreateCommand(FdoCommandType_DescribeSchema)
        self.assertIsNotNone(desc)
        schemas = desc.Execute()
        self.assertIsNotNone(schemas)
        self.assertEqual(1, schemas.Count)
        schema = schemas.GetItem(0)
        classes = schema.GetClasses()
        geomName = None
        for i in range(classes.Count):
            cls = classes.GetItem(i)
            if (cls.Name == "World_Countries"):
                self.assertEqual(cls.ClassType, FdoClassType_FeatureClass)
                geomProp = cls.GetGeometryProperty()
                geomName = geomProp.Name
        self.assertIsNotNone(geomName)
        selectCmd = conn.CreateCommand(FdoCommandType_SelectAggregates)
        self.assertIsNotNone(selectCmd)
        selectCmd.SetFeatureClassName("World_Countries")
        propNames = selectCmd.GetPropertyNames()
        sceExpr = FdoExpression.Parse("SpatialExtents(" + geomName + ")")
        compIdent = FdoComputedIdentifier.Create("EXTENTS", sceExpr)
        propNames.Add(compIdent)
        rdr = selectCmd.Execute()
        geomFactory = FdoFgfGeometryFactory.GetInstance()
        iterations = 0
        while (rdr.ReadNext()):
            self.assertFalse(rdr.IsNull("EXTENTS"))
            self.assertEqual(rdr.GetPropertyType("EXTENTS"), FdoPropertyType_GeometricProperty)
            bytes = rdr.GetGeometryBytes("EXTENTS")
            self.assertIsNotNone(bytes)
            geom = geomFactory.CreateGeometryFromFgf(bytes)
            self.assertIsNotNone(geom)
            wkt = geom.Text
            self.assertIsNotNone(wkt)
            print "\nSpatialExtents() - " + wkt
            iterations += 1
        rdr.Close()
        self.assertEqual(1, iterations)
        #re-test with sugar
        propNames.Clear()
        self.assertEqual(0, propNames.Count)
        propNames.AddComputedIdentifier("EXTENTS", "SpatialExtents(" + geomName + ")")
        self.assertEqual(1, propNames.Count)
        rdr = selectCmd.Execute()
        iterations = 0
        while (rdr.ReadNext()):
            self.assertFalse(rdr.IsNull("EXTENTS"))
            self.assertEqual(rdr.GetPropertyType("EXTENTS"), FdoPropertyType_GeometricProperty)
            bytes = rdr.GetGeometryBytes("EXTENTS")
            self.assertIsNotNone(bytes)
            geom = geomFactory.CreateGeometryFromFgf(bytes)
            self.assertIsNotNone(geom)
            wkt = geom.Text
            self.assertIsNotNone(wkt)
            print "\nSpatialExtents() - " + wkt
            iterations += 1
        rdr.Close()
        self.assertEqual(1, iterations)

    def _doFilteredSpatialExtents(self, conn):
        desc = conn.CreateCommand(FdoCommandType_DescribeSchema)
        self.assertIsNotNone(desc)
        schemas = desc.Execute()
        self.assertIsNotNone(schemas)
        self.assertEqual(1, schemas.Count)
        schema = schemas.GetItem(0)
        classes = schema.GetClasses()
        geomName = None
        for i in range(classes.Count):
            cls = classes.GetItem(i)
            if (cls.Name == "World_Countries"):
                self.assertEqual(cls.ClassType, FdoClassType_FeatureClass)
                geomProp = cls.GetGeometryProperty()
                geomName = geomProp.Name
        self.assertIsNotNone(geomName)
        selectCmd = conn.CreateCommand(FdoCommandType_SelectAggregates)
        self.assertIsNotNone(selectCmd)
        selectCmd.SetFeatureClassName("World_Countries")
        selectCmd.SetFilter("NAME = 'Canada'")
        propNames = selectCmd.GetPropertyNames()
        sceExpr = FdoExpression.Parse("SpatialExtents(" + geomName + ")")
        compIdent = FdoComputedIdentifier.Create("EXTENTS", sceExpr)
        propNames.Add(compIdent)
        rdr = selectCmd.Execute()
        geomFactory = FdoFgfGeometryFactory.GetInstance()
        iterations = 0
        while (rdr.ReadNext()):
            self.assertFalse(rdr.IsNull("EXTENTS"))
            self.assertEqual(rdr.GetPropertyType("EXTENTS"), FdoPropertyType_GeometricProperty)
            bytes = rdr.GetGeometryBytes("EXTENTS")
            self.assertIsNotNone(bytes)
            geom = geomFactory.CreateGeometryFromFgf(bytes)
            self.assertIsNotNone(geom)
            wkt = geom.Text
            self.assertIsNotNone(wkt)
            print "\nSpatialExtents() - " + wkt
            iterations += 1
        rdr.Close()
        self.assertEqual(1, iterations)
        #re-test with sugar
        propNames.Clear()
        self.assertEqual(0, propNames.Count)
        propNames.AddComputedIdentifier("EXTENTS", "SpatialExtents(" + geomName + ")")
        self.assertEqual(1, propNames.Count)
        rdr = selectCmd.Execute()
        iterations = 0
        while (rdr.ReadNext()):
            self.assertFalse(rdr.IsNull("EXTENTS"))
            self.assertEqual(rdr.GetPropertyType("EXTENTS"), FdoPropertyType_GeometricProperty)
            bytes = rdr.GetGeometryBytes("EXTENTS")
            self.assertIsNotNone(bytes)
            geom = geomFactory.CreateGeometryFromFgf(bytes)
            self.assertIsNotNone(geom)
            wkt = geom.Text
            self.assertIsNotNone(wkt)
            print "\nSpatialExtents() - " + wkt
            iterations += 1
        rdr.Close()
        self.assertEqual(1, iterations)

    def testSDFCount(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SDF")
        conn.ConnectionString = "File=../../../../TestData/SDF/World_Countries.sdf"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doCount(conn)
            self._doFilteredCount(conn)
        finally:
            conn.Close()

    def testSHPCount(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SHP")
        conn.ConnectionString = "DefaultFileLocation=../../../../TestData/SHP/World_Countries.shp"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doCount(conn)
            self._doFilteredCount(conn)
        finally:
            conn.Close()

    def testSQLiteCount(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SQLite")
        conn.ConnectionString = "File=../../../../TestData/SQLite/World_Countries.sqlite"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doCount(conn)
            self._doFilteredCount(conn)
        finally:
            conn.Close()

    def testSDFDistinct(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SDF")
        conn.ConnectionString = "File=../../../../TestData/SDF/World_Countries.sdf"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doDistinct(conn)
            self._doFilteredDistinct(conn)
        finally:
            conn.Close()

    def testSHPDistinct(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SHP")
        conn.ConnectionString = "DefaultFileLocation=../../../../TestData/SHP/World_Countries.shp"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doDistinct(conn)
            self._doFilteredDistinct(conn)
        finally:
            conn.Close()

    def testSQLiteDistinct(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SQLite")
        conn.ConnectionString = "File=../../../../TestData/SQLite/World_Countries.sqlite"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doDistinct(conn)
            self._doFilteredDistinct(conn)
        finally:
            conn.Close()

    def testSDFSpatialExtents(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SDF")
        conn.ConnectionString = "File=../../../../TestData/SDF/World_Countries.sdf"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doSpatialExtents(conn)
            self._doFilteredSpatialExtents(conn)
        finally:
            conn.Close()

    def testSHPSpatialExtents(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SHP")
        conn.ConnectionString = "DefaultFileLocation=../../../../TestData/SHP/World_Countries.shp"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doSpatialExtents(conn)
            self._doFilteredSpatialExtents(conn)
        finally:
            conn.Close()

    def testSQLiteSpatialExtents(self):
        connMgr = FdoFeatureAccessManager.GetConnectionManager()
        conn = connMgr.CreateConnection("OSGeo.SQLite")
        conn.ConnectionString = "File=../../../../TestData/SQLite/World_Countries.sqlite"
        self.assert_(conn.Open(), FdoConnectionState_Open)
        try:
            self._doSpatialExtents(conn)
            self._doFilteredSpatialExtents(conn)
        finally:
            conn.Close()