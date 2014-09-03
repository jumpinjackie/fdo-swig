import traceback
import string
import os.path
import sys
from FDO import *
import unittest

class GeometryTest(unittest.TestCase):

    def _checkPositionXY(self, pos, x, y):
        self.assertEqual(x, pos.X)
        self.assertEqual(y, pos.Y)

    def _checkPositionXYM(self, pos, x, y, m):
        self._checkPositionXY(pos, x, y)
        self.assertEqual(m, pos.M)

    def _checkPositionXYZ(self, pos, x, y, z):
        self._checkPositionXY(pos, x, y)
        self.assertEqual(z, pos.Z)

    def _checkPositionXYZM(self, pos, x, y, z, m):
        self._checkPositionXYZ(pos, x, y, z)
        self.assertEqual(m, pos.M)

    def _checkEnvelope(self, envl, ordsXY):
        self.assertEqual(ordsXY[0], envl.MinX)
        self.assertEqual(ordsXY[1], envl.MinY)
        self.assertEqual(ordsXY[2], envl.MaxX)
        self.assertEqual(ordsXY[3], envl.MaxY)

    def _checkEnvelope(self, envl, pos1, pos2):
        self.assertEqual(pos1.X, envl.MinX)
        self.assertEqual(pos1.Y, envl.MinY)
        self.assert_((pos1.Dimensionality & FdoDimensionality_Z) == 0 or envl.MinZ == pos1.Z)
        self.assertEqual(pos2.X, envl.MaxX)
        self.assertEqual(pos2.Y, envl.MaxY)
        self.assert_((pos2.Dimensionality & FdoDimensionality_Z) == 0 or envl.MaxZ == pos2.Z)

    def _checkEnvelopeXY(self, envl, minx, miny, maxx, maxy):
        self.assertEqual(minx, envl.MinX)
        self.assertEqual(miny, envl.MinY)
        self.assertEqual(maxx, envl.MaxX)
        self.assertEqual(maxy, envl.MaxY)

    def _checkEnvelopeXYZ(self, envl, minx, miny, minz, maxx, maxy, maxz):
        self._checkEnvelopeXY(envl, minx, miny, maxx, maxy)
        self.assertEqual(minz, envl.MinZ)
        self.assertEqual(maxz, envl.MaxZ)

    def _checkEqualEnvelopes(self, envl1, envl2):
        self.assertEqual(envl1.MinX, envl2.MinX)
        self.assertEqual(envl1.MinY, envl2.MinY)
        self.assertEqual(envl1.MinZ, envl2.MinZ)
        self.assertEqual(envl1.MaxX, envl2.MaxX)
        self.assertEqual(envl1.MaxY, envl2.MaxY)
        self.assertEqual(envl1.MaxZ, envl2.MaxZ)

    def testDirectPosition(self):
        factory = FdoFgfGeometryFactory.GetInstance()
        pointXY = factory.CreatePositionXY(5.0, 6.0)
        self._checkPositionXY(pointXY, 5.0, 6.0)
        pointXYM = factory.CreatePositionXYM(1.0, 3.0, 5.0)
        self._checkPositionXYM(pointXYM, 1.0, 3.0, 5.0)
        pointXYZ = factory.CreatePositionXYZ(2.0, 7.0, 10.0)
        self._checkPositionXYZ(pointXYZ, 2.0, 7.0, 10.0)
        pointXYZM = factory.CreatePositionXYZM(1.23, 4.26, 3.67, 3.14)
        self._checkPositionXYZM(pointXYZM, 1.23, 4.26, 3.67, 3.14)

    def testEnvelope(self):
        gf = FdoFgfGeometryFactory.GetInstance()
        pos1 = gf.CreatePositionXY(5.0, 6.0)
        pos2 = gf.CreatePositionXY(10.0, 13.0)
        envl2 = gf.CreateEnvelope(pos1, pos2)
        self._checkEnvelope(envl2, pos1, pos2)
        envl3 = gf.CreateEnvelopeXY(4.0, 3.0, 12.0, 120.0)
        self._checkEnvelopeXY(envl3, 4.0, 3.0, 12.0, 120.0)
        envl4 = gf.CreateEnvelopeXYZ(12.0, 45.0, 1.0, 34.0, 39.0, 2.0)
        self._checkEnvelopeXYZ(envl4, 12.0, 45.0, 1.0, 34.0, 39.0, 2.0)

    def testLinearRing(self):
        positions = FdoDirectPositionCollection.Create()
        fact = FdoFgfGeometryFactory.GetInstance()
        pos1 = fact.CreatePositionXY(0.0, 0.0)
        pos2 = fact.CreatePositionXY(1.0, 0.0)
        pos3 = fact.CreatePositionXY(1.0, 1.0)
        pos4 = fact.CreatePositionXY(0.0, 1.0)
        pos5 = fact.CreatePositionXY(0.0, 0.0)
        positions.Add(pos1)
        positions.Add(pos2)
        positions.Add(pos3)
        positions.Add(pos4)
        positions.Add(pos5)
        ring = fact.CreateLinearRing(positions)
        positions2 = ring.GetPositions()
        self.assertEqual(positions.Count, positions2.Count)
        for i in range(positions.Count):
            posExpect = positions.GetItem(i)
            posActual = positions2.GetItem(i)
            self.assertEqual(posExpect.Dimensionality, posActual.Dimensionality)
            self._checkPositionXY(posExpect, posActual.X, posActual.Y)

    def testLineStringSegment(self):
        positions = FdoDirectPositionCollection.Create()
        fact = FdoFgfGeometryFactory.GetInstance()
        pos1 = fact.CreatePositionXY(0.0, 0.0)
        pos2 = fact.CreatePositionXY(1.0, 0.0)
        pos3 = fact.CreatePositionXY(1.0, 1.0)
        pos4 = fact.CreatePositionXY(0.0, 1.0)
        pos5 = fact.CreatePositionXY(0.0, 0.0)
        positions.Add(pos1)
        positions.Add(pos2)
        positions.Add(pos3)
        positions.Add(pos4)
        positions.Add(pos5)
        lineStringSegment = fact.CreateLineStringSegment(positions)
        positions2 = lineStringSegment.GetPositions()
        self.assertEqual(positions.Count, positions2.Count)
        for i in range(positions.Count):
            posExpect = positions.GetItem(i)
            posActual = positions2.GetItem(i)
            self.assertEqual(posExpect.Dimensionality, posActual.Dimensionality)
            self._checkPositionXY(posExpect, posActual.X, posActual.Y)

    def testCircularArcSegment(self):
        geomFact = FdoFgfGeometryFactory.GetInstance()
        start = geomFact.CreatePositionXYZ(0.0, 0.0, 0.0)
        mid = geomFact.CreatePositionXYZ(1.0, 1.0, 1.0)
        end = geomFact.CreatePositionXYZ(2.0, 2.0, 2.0)
        arcSeg = geomFact.CreateCircularArcSegment(start, mid, end)
        start2 = arcSeg.GetStartPosition()
        mid2 = arcSeg.GetMidPoint()
        end2 = arcSeg.GetEndPosition()

        self._checkPositionXYZ(start, start2.X, start2.Y, start2.Z)
        self._checkPositionXYZ(mid, mid2.X, mid2.Y, mid2.Z)
        self._checkPositionXYZ(end, end2.X, end2.Y, end2.Z)

    def testFGFTAndRoundtrip(self):
        gf = FdoFgfGeometryFactory.GetInstance();
        pt =   gf.CreateGeometry("POINT (5 3)");
        ls =   gf.CreateGeometry("LINESTRING (0 1, 2 3, 4 5)");
        pg =   gf.CreateGeometry("POLYGON ((0 0, 5 0, 5 5, 0 5, 0 0), (1 1, 2 1, 2 2, 1 1), (3 3, 4 3, 4 4, 3 3))");
        mpt =  gf.CreateGeometry("MULTIPOINT XYZ (1 2 3, 4 5 6, 7 8 9)");
        mls =  gf.CreateGeometry("MULTILINESTRING XYZ ((0 1 2, 3 4 5, 6 7 8), (9 10 11, 12 13 14, 15 16 17))");
        mpg =  gf.CreateGeometry("MULTIPOLYGON (((0 0, 5 0, 5 5, 0 5, 0 0), (1 1, 2 1, 2 2, 1 1), (3 3, 4 3, 4 4, 3 3)), ((0 0, 5 0, 5 5, 0 5, 0 0), (1 1, 2 1, 2 2, 1 1), (3 3, 4 3, 4 4, 3 3)))");
        mg =   gf.CreateGeometry("GEOMETRYCOLLECTION (CURVEPOLYGON ((100 100 (CIRCULARARCSEGMENT (100 101, 101 102), LINESTRINGSEGMENT (100 100))), (200 200 (CIRCULARARCSEGMENT (200 201, 201 202), LINESTRINGSEGMENT (200 200))), (300 300 (CIRCULARARCSEGMENT (300 301, 301 302), LINESTRINGSEGMENT (300 300)))), CURVESTRING (100 100 (CIRCULARARCSEGMENT (100 101, 101 102), LINESTRINGSEGMENT (103 100, 103 102))), LINESTRING (0 1, 2 3, 4 5), POINT XYZ (5 3 2), POLYGON ((0 0, 5 0, 5 5, 0 5, 0 0), (1 1, 2 1, 2 2, 1 1), (3 3, 4 3, 4 4, 3 3)))");
        cs =   gf.CreateGeometry("CURVESTRING (0 0 (CIRCULARARCSEGMENT (0 1, 1 2), LINESTRINGSEGMENT (3 0, 3 2)))");
        cpg =  gf.CreateGeometry("CURVEPOLYGON ((200 200 (CIRCULARARCSEGMENT (200 201, 201 202), LINESTRINGSEGMENT (200 200))), (300 300 (CIRCULARARCSEGMENT (300 301, 301 302), LINESTRINGSEGMENT (300 300))), (400 400 (CIRCULARARCSEGMENT (400 401, 401 402), LINESTRINGSEGMENT (400 400))))");
        mcs =  gf.CreateGeometry("MULTICURVESTRING ((100 100 (CIRCULARARCSEGMENT (100 101, 101 102), LINESTRINGSEGMENT (103 100, 103 102))), (200 200 (CIRCULARARCSEGMENT (200 201, 201 202), LINESTRINGSEGMENT (203 200, 203 202))), (300 300 (CIRCULARARCSEGMENT (300 301, 301 302), LINESTRINGSEGMENT (303 300, 303 302))))");
        mcpg = gf.CreateGeometry("MULTICURVEPOLYGON (((200 200 (CIRCULARARCSEGMENT (200 201, 201 202), LINESTRINGSEGMENT (200 200))), (300 300 (CIRCULARARCSEGMENT (300 301, 301 302), LINESTRINGSEGMENT (300 300))), (400 400 (CIRCULARARCSEGMENT (400 401, 401 402), LINESTRINGSEGMENT (400 400)))), ((201 201 (CIRCULARARCSEGMENT (201 202, 202 203), LINESTRINGSEGMENT (201 201))), (301 301 (CIRCULARARCSEGMENT (301 302, 302 303), LINESTRINGSEGMENT (301 301))), (401 401 (CIRCULARARCSEGMENT (401 402, 402 403), LINESTRINGSEGMENT (401 401)))), ((202 202 (CIRCULARARCSEGMENT (202 203, 203 204), LINESTRINGSEGMENT (202 202))), (302 302 (CIRCULARARCSEGMENT (302 303, 303 304), LINESTRINGSEGMENT (302 302))), (402 402 (CIRCULARARCSEGMENT (402 403, 403 404), LINESTRINGSEGMENT (402 402)))))");

        self.assertEqual(pt.DerivedType, FdoGeometryType_Point)
        self.assertEqual(ls.DerivedType, FdoGeometryType_LineString)
        self.assertEqual(pg.DerivedType, FdoGeometryType_Polygon)
        self.assertEqual(mpt.DerivedType, FdoGeometryType_MultiPoint)
        self.assertEqual(mls.DerivedType, FdoGeometryType_MultiLineString)
        self.assertEqual(mpg.DerivedType, FdoGeometryType_MultiPolygon)
        self.assertEqual(mg.DerivedType, FdoGeometryType_MultiGeometry)
        self.assertEqual(cs.DerivedType, FdoGeometryType_CurveString)
        self.assertEqual(cpg.DerivedType, FdoGeometryType_CurvePolygon)
        self.assertEqual(mcs.DerivedType, FdoGeometryType_MultiCurveString)
        self.assertEqual(mcpg.DerivedType, FdoGeometryType_MultiCurvePolygon)

        fgf_pt = gf.GetFgfBytes(pt);
        fgf_ls = gf.GetFgfBytes(ls);
        fgf_pg = gf.GetFgfBytes(pg);
        fgf_mpt = gf.GetFgfBytes(mpt);
        fgf_mls = gf.GetFgfBytes(mls);
        fgf_mpg = gf.GetFgfBytes(mpg);
        fgf_mg = gf.GetFgfBytes(mg);
        fgf_cs = gf.GetFgfBytes(cs);
        fgf_cpg = gf.GetFgfBytes(cpg);
        fgf_mcs = gf.GetFgfBytes(mcs);
        fgf_mcpg = gf.GetFgfBytes(mcpg);

        self.assertIsNotNone(fgf_pt);
        self.assertIsNotNone(fgf_ls);
        self.assertIsNotNone(fgf_pg);
        self.assertIsNotNone(fgf_mpt);
        self.assertIsNotNone(fgf_mls);
        self.assertIsNotNone(fgf_mpg);
        self.assertIsNotNone(fgf_mg);
        self.assertIsNotNone(fgf_cs);
        self.assertIsNotNone(fgf_cpg);
        self.assertIsNotNone(fgf_mcs);
        self.assertIsNotNone(fgf_mcpg);

        #Only test geometries we know are transferrable to WKB
        wkb_pt = gf.GetWkbBytes(pt);
        wkb_ls = gf.GetWkbBytes(ls);
        wkb_pg = gf.GetWkbBytes(pg);
        #wkb_mpt = gf.GetWkbBytes(mpt);
        #wkb_mls = gf.GetWkbBytes(mls);
        wkb_mpg = gf.GetWkbBytes(mpg);
        #wkb_mg = gf.GetWkbBytes(mg);
        #wkb_cs = gf.GetWkbBytes(cs);
        #wkb_cpg = gf.GetWkbBytes(cpg);
        #wkb_mcs = gf.GetWkbBytes(mcs);
        #wkb_mcpg = gf.GetWkbBytes(mcpg);

        self.assertIsNotNone(wkb_pt);
        self.assertIsNotNone(wkb_ls);
        self.assertIsNotNone(wkb_pg);
        #self.assertIsNotNone(wkb_mpt);
        #self.assertIsNotNone(wkb_mls);
        self.assertIsNotNone(wkb_mpg);
        #self.assertIsNotNone(wkb_mg);
        #self.assertIsNotNone(wkb_cs);
        #self.assertIsNotNone(wkb_cpg);
        #self.assertIsNotNone(wkb_mcs);
        #self.assertIsNotNone(wkb_mcpg);

        pt = gf.CreateGeometryFromFgf(fgf_pt);
        ls = gf.CreateGeometryFromFgf(fgf_ls);
        pg = gf.CreateGeometryFromFgf(fgf_pg);
        mpt = gf.CreateGeometryFromFgf(fgf_mpt);
        mls = gf.CreateGeometryFromFgf(fgf_mls);
        mpg = gf.CreateGeometryFromFgf(fgf_mpg);
        mg = gf.CreateGeometryFromFgf(fgf_mg);
        cs = gf.CreateGeometryFromFgf(fgf_cs);
        cpg = gf.CreateGeometryFromFgf(fgf_cpg);
        mcs = gf.CreateGeometryFromFgf(fgf_mcs);
        mcpg = gf.CreateGeometryFromFgf(fgf_mcpg);

        self.assertEqual(pt.DerivedType, FdoGeometryType_Point)
        self.assertEqual(ls.DerivedType, FdoGeometryType_LineString)
        self.assertEqual(pg.DerivedType, FdoGeometryType_Polygon)
        self.assertEqual(mpt.DerivedType, FdoGeometryType_MultiPoint)
        self.assertEqual(mls.DerivedType, FdoGeometryType_MultiLineString)
        self.assertEqual(mpg.DerivedType, FdoGeometryType_MultiPolygon)
        self.assertEqual(mg.DerivedType, FdoGeometryType_MultiGeometry)
        self.assertEqual(cs.DerivedType, FdoGeometryType_CurveString)
        self.assertEqual(cpg.DerivedType, FdoGeometryType_CurvePolygon)
        self.assertEqual(mcs.DerivedType, FdoGeometryType_MultiCurveString)
        self.assertEqual(mcpg.DerivedType, FdoGeometryType_MultiCurvePolygon)

        pt = gf.CreateGeometryFromWkb(wkb_pt);
        ls = gf.CreateGeometryFromWkb(wkb_ls);
        pg = gf.CreateGeometryFromWkb(wkb_pg);
        #mpt = gf.CreateGeometryFromWkb(wkb_mpt);
        #mls = gf.CreateGeometryFromWkb(wkb_mls);
        mpg = gf.CreateGeometryFromWkb(wkb_mpg);
        #mg = gf.CreateGeometryFromWkb(wkb_mg);
        #cs = gf.CreateGeometryFromWkb(wkb_cs);
        #cpg = gf.CreateGeometryFromWkb(wkb_cpg);
        #mcs = gf.CreateGeometryFromWkb(wkb_mcs);
        #mcpg = gf.CreateGeometryFromWkb(wkb_mcpg);

        self.assertEqual(pt.DerivedType, FdoGeometryType_Point)
        self.assertEqual(ls.DerivedType, FdoGeometryType_LineString)
        self.assertEqual(pg.DerivedType, FdoGeometryType_Polygon)
        #self.assertEqual(mpt.DerivedType, FdoGeometryType_MultiPoint)
        #self.assertEqual(mls.DerivedType, FdoGeometryType_MultiLineString)
        self.assertEqual(mpg.DerivedType, FdoGeometryType_MultiPolygon)
        #self.assertEqual(mg.DerivedType, FdoGeometryType_MultiGeometry)
        #self.assertEqual(cs.DerivedType, FdoGeometryType_CurveString)
        #self.assertEqual(cpg.DerivedType, FdoGeometryType_CurvePolygon)
        #self.assertEqual(mcs.DerivedType, FdoGeometryType_MultiCurveString)
        #self.assertEqual(mcpg.DerivedType, FdoGeometryType_MultiCurvePolygon)