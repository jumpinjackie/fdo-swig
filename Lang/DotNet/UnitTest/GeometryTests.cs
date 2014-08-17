using NUnit.Framework;
using OSGeo.FDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestFixture]
    public class GeometryTests
    {
        void CheckPositionXY(FdoIDirectPosition pos, double x, double y)
        {
            Assert.AreEqual(x, pos.GetX(), "X value mismatch");
            Assert.AreEqual(y, pos.GetY(), "Y value mismatch");
        }

        void CheckPositionXYM(FdoIDirectPosition pos, double x, double y, double m)
        {
            CheckPositionXY(pos, x, y);
            Assert.AreEqual(m, pos.GetM(), "M value mismatch");
        }

        void CheckPositionXYZ(FdoIDirectPosition pos, double x, double y, double z)
        {
            CheckPositionXY(pos, x, y);
            Assert.AreEqual(z, pos.GetZ(), "Z value mismatch");
        }

        void CheckPositionXYZM(FdoIDirectPosition pos, double x, double y, double z, double m)
        {
            CheckPositionXYZ(pos, x, y, z);
            Assert.AreEqual(m, pos.GetM(), "M value mismatch");
        }

        void CheckEnvelope(FdoIEnvelope envl, double[] ordsXY)
        {
            Assert.AreEqual(ordsXY[0], envl.GetMinX(), "MinX mismatch");
            Assert.AreEqual(ordsXY[1], envl.GetMinY(), "MinY mismatch");
            Assert.AreEqual(ordsXY[2], envl.GetMaxX(), "MaxX mismatch");
            Assert.AreEqual(ordsXY[3], envl.GetMaxY(), "MaxY mismatch");
        }

        void CheckEnvelope(FdoIEnvelope envl, FdoIDirectPosition pos1, FdoIDirectPosition pos2)
        {
            Assert.AreEqual(pos1.GetX(), envl.GetMinX(), "MinX mismatch");
            Assert.AreEqual(pos1.GetY(), envl.GetMinY(), "MinY mismatch");
            Assert.True((pos1.GetDimensionality() & (int)FdoDimensionality.FdoDimensionality_Z) == 0 || envl.GetMinZ() == pos1.GetZ(), "MinZ mismatch");
            Assert.AreEqual(pos2.GetX(), envl.GetMaxX(), "MaxX mismatch");
            Assert.AreEqual(pos2.GetY(), envl.GetMaxY(), "MaxY mismatch");
            Assert.True((pos2.GetDimensionality() & (int)FdoDimensionality.FdoDimensionality_Z) == 0 || envl.GetMaxZ() == pos2.GetZ(), "MaxZ mismatch");
        }

        void CheckEnvelopeXY(FdoIEnvelope envl, double minx, double miny, double maxx, double maxy)
        {
            Assert.AreEqual(minx, envl.GetMinX(), "MinX mismatch");
            Assert.AreEqual(miny, envl.GetMinY(), "MinY mismatch");
            Assert.AreEqual(maxx, envl.GetMaxX(), "MaxX mismatch");
            Assert.AreEqual(maxy, envl.GetMaxY(), "MaxY mismatch");
        }

        void CheckEnvelopeXYZ(FdoIEnvelope envl, double minx, double miny, double minz, double maxx, double maxy, double maxz)
        {
            CheckEnvelopeXY(envl, minx, miny, maxx, maxy);
            Assert.AreEqual(minz, envl.GetMinZ(), "MinZ mismatch");
            Assert.AreEqual(maxz, envl.GetMaxZ(), "MaxZ mismatch");
        }

        void CheckEqualEnvelopes(FdoIEnvelope envl1, FdoIEnvelope envl2)
        {
            Assert.AreEqual(envl1.GetMinX(), envl2.GetMinX(), "Envelope MinX mismatch");
            Assert.AreEqual(envl1.GetMinY(), envl2.GetMinY(), "Envelope MinY mismatch");
            Assert.AreEqual(envl1.GetMinZ(), envl2.GetMinZ(), "Envelope MinZ mismatch");
            Assert.AreEqual(envl1.GetMaxX(), envl2.GetMaxX(), "Envelope MaxX mismatch");
            Assert.AreEqual(envl1.GetMaxY(), envl2.GetMaxY(), "Envelope MaxY mismatch");
            Assert.AreEqual(envl1.GetMaxZ(), envl2.GetMaxZ(), "Envelope MaxZ mismatch");
        }

        [Test]
        public void TestDirectPosition()
        {
            FdoFgfGeometryFactory factory = FdoFgfGeometryFactory.GetInstance();
            
            FdoIDirectPosition pointXY = factory.CreatePositionXY(5.0, 6.0);
	        //DumpPosition("", pointXY);
	        CheckPositionXY(pointXY, 5.0, 6.0);

	        FdoIDirectPosition pointXYM = factory.CreatePositionXYM(1.0, 3.0, 5.0);
	        //DumpPosition("", pointXYM);
	        CheckPositionXYM(pointXYM, 1.0, 3.0, 5.0);

	        FdoIDirectPosition pointXYZ = factory.CreatePositionXYZ(2.0, 7.0, 10.0);
	        //DumpPosition("", pointXYZ);
	        CheckPositionXYZ(pointXYZ, 2.0, 7.0, 10.0);

	        FdoIDirectPosition pointXYZM = factory.CreatePositionXYZM(1.23, 4.26, 3.67, 3.14);
	        //DumpPosition("", pointXYZM);
	        CheckPositionXYZM(pointXYZM, 1.23, 4.26, 3.67, 3.14);
        }

        [Test]
        public void TestEnvelope()
        {
            FdoFgfGeometryFactory gf = FdoFgfGeometryFactory.GetInstance();

            FdoIDirectPosition pos1 = gf.CreatePositionXY(5.0, 6.0);
            FdoIDirectPosition pos2 = gf.CreatePositionXY(10.0, 13.0);
            FdoIEnvelope envl2 = gf.CreateEnvelope(pos1, pos2);
            //DumpEnvelope(envl2);
            CheckEnvelope(envl2, pos1, pos2);

            FdoIEnvelope envl3 = gf.CreateEnvelopeXY(4.0, 3.0, 12.0, 120.0);
            //DumpEnvelope(envl3);
            CheckEnvelopeXY(envl3, 4.0, 3.0, 12.0, 120.0);

            FdoIEnvelope envl4 = gf.CreateEnvelopeXYZ(12.0, 45.0, 1.0, 34.0, 39.0, 2.0);
            //DumpEnvelope(envl4);
            CheckEnvelopeXYZ(envl4, 12.0, 45.0, 1.0, 34.0, 39.0, 2.0);

            /*
            FdoIEnvelope envl5 = gf.CreateEnvelope(envl4);
            //DumpEnvelope(envl5);
            CheckEqualEnvelopes(envl4, envl5);

            FdoIEnvelope envl6 = gf.CreateEnvelope(envl5);
            //DumpEnvelope(envl5);
            CheckEqualEnvelopes(envl6, envl5);
             */
        }

        [Test]
        public void TestLinearRing()
        {
            FdoDirectPositionCollection positions = FdoDirectPositionCollection.Create();
            FdoFgfGeometryFactory fact = FdoFgfGeometryFactory.GetInstance();
            FdoIDirectPosition pos1 = fact.CreatePositionXY(0.0, 0.0);
            FdoIDirectPosition pos2 = fact.CreatePositionXY(1.0, 0.0);
            FdoIDirectPosition pos3 = fact.CreatePositionXY(1.0, 1.0);
            FdoIDirectPosition pos4 = fact.CreatePositionXY(0.0, 1.0);
            FdoIDirectPosition pos5 = fact.CreatePositionXY(0.0, 0.0);
            positions.Add(pos1);
            positions.Add(pos2);
            positions.Add(pos3);
            positions.Add(pos4);
            positions.Add(pos5);

            FdoILinearRing ring = fact.CreateLinearRing(positions);
            FdoDirectPositionCollection positions2 = ring.GetPositions();
            Assert.AreEqual(positions.GetCount(), positions2.GetCount(), "Position count mismatch");
            for (int i = 0; i < positions.GetCount(); i++)
            {
                FdoIDirectPosition posExpect = positions.GetItem(i);
                FdoIDirectPosition posActual = positions2.GetItem(i);

                Assert.AreEqual(posExpect.GetDimensionality(), posActual.GetDimensionality(), "Dimensionality mismatch");
                CheckPositionXY(posExpect, posActual.GetX(), posActual.GetY());
            }
        }

        [Test]
        public void TestLineStringSegment()
        {
            FdoDirectPositionCollection positions = FdoDirectPositionCollection.Create();
            FdoFgfGeometryFactory fact = FdoFgfGeometryFactory.GetInstance();
            FdoIDirectPosition pos1 = fact.CreatePositionXY(0.0, 0.0);
            FdoIDirectPosition pos2 = fact.CreatePositionXY(1.0, 0.0);
            FdoIDirectPosition pos3 = fact.CreatePositionXY(1.0, 1.0);
            FdoIDirectPosition pos4 = fact.CreatePositionXY(0.0, 1.0);
            FdoIDirectPosition pos5 = fact.CreatePositionXY(0.0, 0.0);
            positions.Add(pos1);
            positions.Add(pos2);
            positions.Add(pos3);
            positions.Add(pos4);
            positions.Add(pos5);

            FdoILineStringSegment lineStringSegment = fact.CreateLineStringSegment(positions);
            FdoDirectPositionCollection positions2 = lineStringSegment.GetPositions();
            Assert.AreEqual(positions.GetCount(), positions2.GetCount(), "Position count mismatch");
            for (int i = 0; i < positions.GetCount(); i++)
            {
                FdoIDirectPosition posExpect = positions.GetItem(i);
                FdoIDirectPosition posActual = positions2.GetItem(i);

                Assert.AreEqual(posExpect.GetDimensionality(), posActual.GetDimensionality(), "Dimensionality mismatch");
                CheckPositionXY(posExpect, posActual.GetX(), posActual.GetY());
            }
        }

        [Test]
        public void TestCircularArcSegment()
        {
            FdoFgfGeometryFactory fact = FdoFgfGeometryFactory.GetInstance();

            var start = fact.CreatePositionXYZ(0.0, 0.0, 0.0);
            var mid = fact.CreatePositionXYZ(1.0, 1.0, 1.0);
            var end = fact.CreatePositionXYZ(2.0, 2.0, 2.0);

            var arcSeg = fact.CreateCircularArcSegment(start, mid, end);
            var start2 = arcSeg.GetStartPosition();
            var mid2 = arcSeg.GetMidPoint();
            var end2 = arcSeg.GetEndPosition();

            CheckPositionXYZ(start, start2.GetX(), start2.GetY(), start2.GetZ());
            CheckPositionXYZ(mid, mid2.GetX(), mid2.GetY(), mid2.GetZ());
            CheckPositionXYZ(end, end2.GetX(), end2.GetY(), end2.GetZ());
        }

        [Test]
        public void TestFGFT()
        {
            FdoFgfGeometryFactory gf = FdoFgfGeometryFactory.GetInstance();

            FdoIGeometry pt =   gf.CreateGeometry("POINT (5 3)");
            FdoIGeometry ls =   gf.CreateGeometry("LINESTRING (0 1, 2 3, 4 5)");
            FdoIGeometry pg =   gf.CreateGeometry("POLYGON ((0 0, 5 0, 5 5, 0 5, 0 0), (1 1, 2 1, 2 2, 1 1), (3 3, 4 3, 4 4, 3 3))");
            FdoIGeometry mpt =  gf.CreateGeometry("MULTIPOINT XYZ (1 2 3, 4 5 6, 7 8 9)");
            FdoIGeometry mls =  gf.CreateGeometry("MULTILINESTRING XYZ ((0 1 2, 3 4 5, 6 7 8), (9 10 11, 12 13 14, 15 16 17))");
            FdoIGeometry mpg =  gf.CreateGeometry("MULTIPOLYGON (((0 0, 5 0, 5 5, 0 5, 0 0), (1 1, 2 1, 2 2, 1 1), (3 3, 4 3, 4 4, 3 3)), ((0 0, 5 0, 5 5, 0 5, 0 0), (1 1, 2 1, 2 2, 1 1), (3 3, 4 3, 4 4, 3 3)))");
            FdoIGeometry mg =   gf.CreateGeometry("GEOMETRYCOLLECTION (CURVEPOLYGON ((100 100 (CIRCULARARCSEGMENT (100 101, 101 102), LINESTRINGSEGMENT (100 100))), (200 200 (CIRCULARARCSEGMENT (200 201, 201 202), LINESTRINGSEGMENT (200 200))), (300 300 (CIRCULARARCSEGMENT (300 301, 301 302), LINESTRINGSEGMENT (300 300)))), CURVESTRING (100 100 (CIRCULARARCSEGMENT (100 101, 101 102), LINESTRINGSEGMENT (103 100, 103 102))), LINESTRING (0 1, 2 3, 4 5), POINT XYZ (5 3 2), POLYGON ((0 0, 5 0, 5 5, 0 5, 0 0), (1 1, 2 1, 2 2, 1 1), (3 3, 4 3, 4 4, 3 3)))");
            FdoIGeometry cs =   gf.CreateGeometry("CURVESTRING (0 0 (CIRCULARARCSEGMENT (0 1, 1 2), LINESTRINGSEGMENT (3 0, 3 2)))");
            FdoIGeometry cpg =  gf.CreateGeometry("CURVEPOLYGON ((200 200 (CIRCULARARCSEGMENT (200 201, 201 202), LINESTRINGSEGMENT (200 200))), (300 300 (CIRCULARARCSEGMENT (300 301, 301 302), LINESTRINGSEGMENT (300 300))), (400 400 (CIRCULARARCSEGMENT (400 401, 401 402), LINESTRINGSEGMENT (400 400))))");
            FdoIGeometry mcs =  gf.CreateGeometry("MULTICURVESTRING ((100 100 (CIRCULARARCSEGMENT (100 101, 101 102), LINESTRINGSEGMENT (103 100, 103 102))), (200 200 (CIRCULARARCSEGMENT (200 201, 201 202), LINESTRINGSEGMENT (203 200, 203 202))), (300 300 (CIRCULARARCSEGMENT (300 301, 301 302), LINESTRINGSEGMENT (303 300, 303 302))))");
            FdoIGeometry mcpg = gf.CreateGeometry("MULTICURVEPOLYGON (((200 200 (CIRCULARARCSEGMENT (200 201, 201 202), LINESTRINGSEGMENT (200 200))), (300 300 (CIRCULARARCSEGMENT (300 301, 301 302), LINESTRINGSEGMENT (300 300))), (400 400 (CIRCULARARCSEGMENT (400 401, 401 402), LINESTRINGSEGMENT (400 400)))), ((201 201 (CIRCULARARCSEGMENT (201 202, 202 203), LINESTRINGSEGMENT (201 201))), (301 301 (CIRCULARARCSEGMENT (301 302, 302 303), LINESTRINGSEGMENT (301 301))), (401 401 (CIRCULARARCSEGMENT (401 402, 402 403), LINESTRINGSEGMENT (401 401)))), ((202 202 (CIRCULARARCSEGMENT (202 203, 203 204), LINESTRINGSEGMENT (202 202))), (302 302 (CIRCULARARCSEGMENT (302 303, 303 304), LINESTRINGSEGMENT (302 302))), (402 402 (CIRCULARARCSEGMENT (402 403, 403 404), LINESTRINGSEGMENT (402 402)))))");

            Assert.IsInstanceOf<FdoIPoint>(pt);
            Assert.IsInstanceOf<FdoILineString>(ls);
            Assert.IsInstanceOf<FdoIPolygon>(pg);
            Assert.IsInstanceOf<FdoIMultiPoint>(mpt);
            Assert.IsInstanceOf<FdoIMultiLineString>(mls);
            Assert.IsInstanceOf<FdoIMultiPolygon>(mpg);
            Assert.IsInstanceOf<FdoIMultiGeometry>(mg);
            Assert.IsInstanceOf<FdoICurveString>(cs);
            Assert.IsInstanceOf<FdoICurvePolygon>(cpg);
            Assert.IsInstanceOf<FdoIMultiCurveString>(mcs);
            Assert.IsInstanceOf<FdoIMultiCurvePolygon>(mcpg);
        }
    }
}
