using OSGeo.FDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class GeometryTests
    {
        void CheckPositionXY(FdoIDirectPosition pos, double x, double y)
        {
            Assert.Equal(x, pos.X);
            Assert.Equal(y, pos.Y);
        }

        void CheckPositionXYM(FdoIDirectPosition pos, double x, double y, double m)
        {
            CheckPositionXY(pos, x, y);
            Assert.Equal(m, pos.M);
        }

        void CheckPositionXYZ(FdoIDirectPosition pos, double x, double y, double z)
        {
            CheckPositionXY(pos, x, y);
            Assert.Equal(z, pos.Z);
        }

        void CheckPositionXYZM(FdoIDirectPosition pos, double x, double y, double z, double m)
        {
            CheckPositionXYZ(pos, x, y, z);
            Assert.Equal(m, pos.M);
        }

        void CheckEnvelope(FdoIEnvelope envl, double[] ordsXY)
        {
            Assert.Equal(ordsXY[0], envl.MinX);
            Assert.Equal(ordsXY[1], envl.MinY);
            Assert.Equal(ordsXY[2], envl.MaxX);
            Assert.Equal(ordsXY[3], envl.MaxY);
        }

        void CheckEnvelope(FdoIEnvelope envl, FdoIDirectPosition pos1, FdoIDirectPosition pos2)
        {
            Assert.Equal(pos1.X, envl.MinX);
            Assert.Equal(pos1.Y, envl.MinY);
            Assert.True((pos1.Dimensionality & (int)FdoDimensionality.FdoDimensionality_Z) == 0 || envl.MinZ == pos1.Z, "MinZ mismatch");
            Assert.Equal(pos2.X, envl.MaxX);
            Assert.Equal(pos2.Y, envl.MaxY);
            Assert.True((pos2.Dimensionality & (int)FdoDimensionality.FdoDimensionality_Z) == 0 || envl.MaxZ == pos2.Z, "MaxZ mismatch");
        }

        void CheckEnvelopeXY(FdoIEnvelope envl, double minx, double miny, double maxx, double maxy)
        {
            Assert.Equal(minx, envl.MinX);
            Assert.Equal(miny, envl.MinY);
            Assert.Equal(maxx, envl.MaxX);
            Assert.Equal(maxy, envl.MaxY);
        }

        void CheckEnvelopeXYZ(FdoIEnvelope envl, double minx, double miny, double minz, double maxx, double maxy, double maxz)
        {
            CheckEnvelopeXY(envl, minx, miny, maxx, maxy);
            Assert.Equal(minz, envl.MinZ);
            Assert.Equal(maxz, envl.MaxZ);
        }

        void CheckEqualEnvelopes(FdoIEnvelope envl1, FdoIEnvelope envl2)
        {
            Assert.Equal(envl1.MinX, envl2.MinX);
            Assert.Equal(envl1.MinY, envl2.MinY);
            Assert.Equal(envl1.MinZ, envl2.MinZ);
            Assert.Equal(envl1.MaxX, envl2.MaxX);
            Assert.Equal(envl1.MaxY, envl2.MaxY);
            Assert.Equal(envl1.MaxZ, envl2.MaxZ);
        }

        [Fact]
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

        [Fact]
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

        [Fact]
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
            Assert.Equal(positions.Count, positions2.Count);
            for (int i = 0; i < positions.Count; i++)
            {
                FdoIDirectPosition posExpect = positions.GetItem(i);
                FdoIDirectPosition posActual = positions2.GetItem(i);

                Assert.Equal(posExpect.Dimensionality, posActual.Dimensionality);
                CheckPositionXY(posExpect, posActual.X, posActual.Y);
            }
        }

        [Fact]
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
            Assert.Equal(positions.Count, positions2.Count);
            for (int i = 0; i < positions.Count; i++)
            {
                FdoIDirectPosition posExpect = positions.GetItem(i);
                FdoIDirectPosition posActual = positions2.GetItem(i);

                Assert.Equal(posExpect.Dimensionality, posActual.Dimensionality);
                CheckPositionXY(posExpect, posActual.X, posActual.Y);
            }
        }

        [Fact]
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

            CheckPositionXYZ(start, start2.X, start2.Y, start2.Z);
            CheckPositionXYZ(mid, mid2.X, mid2.Y, mid2.Z);
            CheckPositionXYZ(end, end2.X, end2.Y, end2.Z);
        }

        [Fact]
        public void TestFGFTAndRoundtrip()
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

            Assert.IsAssignableFrom<FdoIPoint>(pt);
            Assert.IsAssignableFrom<FdoILineString>(ls);
            Assert.IsAssignableFrom<FdoIPolygon>(pg);
            Assert.IsAssignableFrom<FdoIMultiPoint>(mpt);
            Assert.IsAssignableFrom<FdoIMultiLineString>(mls);
            Assert.IsAssignableFrom<FdoIMultiPolygon>(mpg);
            Assert.IsAssignableFrom<FdoIMultiGeometry>(mg);
            Assert.IsAssignableFrom<FdoICurveString>(cs);
            Assert.IsAssignableFrom<FdoICurvePolygon>(cpg);
            Assert.IsAssignableFrom<FdoIMultiCurveString>(mcs);
            Assert.IsAssignableFrom<FdoIMultiCurvePolygon>(mcpg);

            FdoByteArrayHandle fgf_pt = gf.GetFgfBytes(pt);
            FdoByteArrayHandle fgf_ls = gf.GetFgfBytes(ls);
            FdoByteArrayHandle fgf_pg = gf.GetFgfBytes(pg);
            FdoByteArrayHandle fgf_mpt = gf.GetFgfBytes(mpt);
            FdoByteArrayHandle fgf_mls = gf.GetFgfBytes(mls);
            FdoByteArrayHandle fgf_mpg = gf.GetFgfBytes(mpg);
            FdoByteArrayHandle fgf_mg = gf.GetFgfBytes(mg);
            FdoByteArrayHandle fgf_cs = gf.GetFgfBytes(cs);
            FdoByteArrayHandle fgf_cpg = gf.GetFgfBytes(cpg);
            FdoByteArrayHandle fgf_mcs = gf.GetFgfBytes(mcs);
            FdoByteArrayHandle fgf_mcpg = gf.GetFgfBytes(mcpg);

            Assert.NotNull(fgf_pt);
            Assert.NotNull(fgf_ls);
            Assert.NotNull(fgf_pg);
            Assert.NotNull(fgf_mpt);
            Assert.NotNull(fgf_mls);
            Assert.NotNull(fgf_mpg);
            Assert.NotNull(fgf_mg);
            Assert.NotNull(fgf_cs);
            Assert.NotNull(fgf_cpg);
            Assert.NotNull(fgf_mcs);
            Assert.NotNull(fgf_mcpg);

            //Only test geometries we know are transferrable to WKB
            FdoByteArrayHandle wkb_pt = gf.GetWkbBytes(pt);
            FdoByteArrayHandle wkb_ls = gf.GetWkbBytes(ls);
            FdoByteArrayHandle wkb_pg = gf.GetWkbBytes(pg);
            //FdoByteArrayHandle wkb_mpt = gf.GetWkbBytes(mpt);
            //FdoByteArrayHandle wkb_mls = gf.GetWkbBytes(mls);
            FdoByteArrayHandle wkb_mpg = gf.GetWkbBytes(mpg);
            //FdoByteArrayHandle wkb_mg = gf.GetWkbBytes(mg);
            //FdoByteArrayHandle wkb_cs = gf.GetWkbBytes(cs);
            //FdoByteArrayHandle wkb_cpg = gf.GetWkbBytes(cpg);
            //FdoByteArrayHandle wkb_mcs = gf.GetWkbBytes(mcs);
            //FdoByteArrayHandle wkb_mcpg = gf.GetWkbBytes(mcpg);

            Assert.NotNull(wkb_pt);
            Assert.NotNull(wkb_ls);
            Assert.NotNull(wkb_pg);
            //Assert.NotNull(wkb_mpt);
            //Assert.NotNull(wkb_mls);
            Assert.NotNull(wkb_mpg);
            //Assert.NotNull(wkb_mg);
            //Assert.NotNull(wkb_cs);
            //Assert.NotNull(wkb_cpg);
            //Assert.NotNull(wkb_mcs);
            //Assert.NotNull(wkb_mcpg);

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

            Assert.IsAssignableFrom<FdoIPoint>(pt);
            Assert.IsAssignableFrom<FdoILineString>(ls);
            Assert.IsAssignableFrom<FdoIPolygon>(pg);
            Assert.IsAssignableFrom<FdoIMultiPoint>(mpt);
            Assert.IsAssignableFrom<FdoIMultiLineString>(mls);
            Assert.IsAssignableFrom<FdoIMultiPolygon>(mpg);
            Assert.IsAssignableFrom<FdoIMultiGeometry>(mg);
            Assert.IsAssignableFrom<FdoICurveString>(cs);
            Assert.IsAssignableFrom<FdoICurvePolygon>(cpg);
            Assert.IsAssignableFrom<FdoIMultiCurveString>(mcs);
            Assert.IsAssignableFrom<FdoIMultiCurvePolygon>(mcpg);

            pt = gf.CreateGeometryFromWkb(wkb_pt);
            ls = gf.CreateGeometryFromWkb(wkb_ls);
            pg = gf.CreateGeometryFromWkb(wkb_pg);
            //mpt = gf.CreateGeometryFromWkb(wkb_mpt);
            //mls = gf.CreateGeometryFromWkb(wkb_mls);
            mpg = gf.CreateGeometryFromWkb(wkb_mpg);
            //mg = gf.CreateGeometryFromWkb(wkb_mg);
            //cs = gf.CreateGeometryFromWkb(wkb_cs);
            //cpg = gf.CreateGeometryFromWkb(wkb_cpg);
            //mcs = gf.CreateGeometryFromWkb(wkb_mcs);
            //mcpg = gf.CreateGeometryFromWkb(wkb_mcpg);

            Assert.IsAssignableFrom<FdoIPoint>(pt);
            Assert.IsAssignableFrom<FdoILineString>(ls);
            Assert.IsAssignableFrom<FdoIPolygon>(pg);
            //Assert.IsAssignableFrom<FdoIMultiPoint>(mpt);
            //Assert.IsAssignableFrom<FdoIMultiLineString>(mls);
            Assert.IsAssignableFrom<FdoIMultiPolygon>(mpg);
            //Assert.IsAssignableFrom<FdoIMultiGeometry>(mg);
            //Assert.IsAssignableFrom<FdoICurveString>(cs);
            //Assert.IsAssignableFrom<FdoICurvePolygon>(cpg);
            //Assert.IsAssignableFrom<FdoIMultiCurveString>(mcs);
            //Assert.IsAssignableFrom<FdoIMultiCurvePolygon>(mcpg);
        }
    }
}
