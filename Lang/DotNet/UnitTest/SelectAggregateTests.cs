using OSGeo.FDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class SelectAggregateTests
    {
        private void DoCount(FdoIConnection conn)
        {
            FdoISelectAggregates selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_SelectAggregates) as FdoISelectAggregates;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");

            FdoIdentifierCollection propNames = selectCmd.GetPropertyNames();
            FdoExpression countExpr = FdoExpression.Parse("COUNT(NAME)");
            FdoComputedIdentifier countIdent = FdoComputedIdentifier.Create("TOTAL_COUNT", countExpr);
            propNames.Add(countIdent);

            FdoIDataReader rdr = selectCmd.Execute();
            long total = 0;
            while (rdr.ReadNext())
            {
                total += rdr.GetInt64("TOTAL_COUNT");
            }
            rdr.Close();
            Assert.Equal(419L, total);

            //Re-test with sugar methods
            propNames.Clear();
            Assert.Equal(0, propNames.Count);
            propNames.AddComputedIdentifier("TOTAL_COUNT", "COUNT(NAME)");
            Assert.Equal(1, propNames.Count);

            rdr = selectCmd.Execute();
            total = 0;
            while (rdr.ReadNext())
            {
                total += rdr.GetInt64("TOTAL_COUNT");
            }
            rdr.Close();
            Assert.Equal(419L, total);
        }

        private void DoFilteredCount(FdoIConnection conn)
        {
            FdoISelectAggregates selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_SelectAggregates) as FdoISelectAggregates;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");
            selectCmd.SetFilter("NAME = 'Canada'");

            FdoIdentifierCollection propNames = selectCmd.GetPropertyNames();
            FdoExpression countExpr = FdoExpression.Parse("COUNT(NAME)");
            FdoComputedIdentifier countIdent = FdoComputedIdentifier.Create("TOTAL_COUNT", countExpr);
            propNames.Add(countIdent);

            FdoIDataReader rdr = selectCmd.Execute();
            long total = 0;
            while (rdr.ReadNext())
            {
                total += rdr.GetInt64("TOTAL_COUNT");
            }
            rdr.Close();
            Assert.Equal(66L, total);

            //Re-test with sugar methods
            propNames.Clear();
            Assert.Equal(0, propNames.Count);
            propNames.AddComputedIdentifier("TOTAL_COUNT", "COUNT(NAME)");
            Assert.Equal(1, propNames.Count);

            rdr = selectCmd.Execute();
            total = 0;
            while (rdr.ReadNext())
            {
                total += rdr.GetInt64("TOTAL_COUNT");
            }
            rdr.Close();
            Assert.Equal(66L, total);
        }

        private void DoDistinct(FdoIConnection conn)
        {
            FdoISelectAggregates selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_SelectAggregates) as FdoISelectAggregates;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");
            selectCmd.Distinct = true;

            FdoIdentifierCollection propNames = selectCmd.GetPropertyNames();
            FdoIdentifier ident = FdoIdentifier.Create("NAME");
            propNames.Add(ident);

            FdoIDataReader rdr = selectCmd.Execute();
            HashSet<string> values = new HashSet<string>();

            int count = 0;
            while (rdr.ReadNext())
            {
                values.Add(rdr.GetString("NAME"));
                count++;
            }
            rdr.Close();
            Assert.Equal(values.Count, count);
        }

        private void DoFilteredDistinct(FdoIConnection conn)
        {
            FdoISelectAggregates selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_SelectAggregates) as FdoISelectAggregates;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");
            selectCmd.SetFilter("NAME = 'Canada'");
            selectCmd.Distinct = true;

            FdoIdentifierCollection propNames = selectCmd.GetPropertyNames();
            FdoIdentifier ident = FdoIdentifier.Create("KEY");
            propNames.Add(ident);

            FdoIDataReader rdr = selectCmd.Execute();
            HashSet<string> values = new HashSet<string>();

            int count = 0;
            while (rdr.ReadNext())
            {
                values.Add(rdr.GetString("KEY"));
                count++;
            }
            rdr.Close();
            Assert.Equal(values.Count, count);
        }

        private void DoSpatialExtents(FdoIConnection conn)
        {
            FdoIDescribeSchema desc = conn.CreateCommand((int)FdoCommandType.FdoCommandType_DescribeSchema) as FdoIDescribeSchema;
            Assert.NotNull(desc);
            FdoFeatureSchemaCollection schemas = desc.Execute();
            Assert.NotNull(schemas);
            Assert.Equal(1, schemas.Count);
            FdoFeatureSchema schema = schemas.GetItem(0);
            FdoClassCollection classes = schema.GetClasses();
            string geomName = null;
            for (int i = 0; i < classes.Count; i++)
            {
                FdoClassDefinition cls = classes.GetItem(i);
                if (cls.Name == "World_Countries")
                {
                    Assert.IsAssignableFrom<FdoFeatureClass>(cls);
                    FdoGeometricPropertyDefinition geomProp = ((FdoFeatureClass)cls).GetGeometryProperty();
                    Assert.NotNull(geomProp);
                    geomName = geomProp.Name;
                }
            }
            Assert.NotNull(geomName);

            FdoISelectAggregates selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_SelectAggregates) as FdoISelectAggregates;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");

            FdoIdentifierCollection propNames = selectCmd.GetPropertyNames();
            FdoExpression countExpr = FdoExpression.Parse("SpatialExtents(" + geomName + ")");
            FdoComputedIdentifier countIdent = FdoComputedIdentifier.Create("EXTENTS", countExpr);
            propNames.Add(countIdent);

            FdoIDataReader rdr = selectCmd.Execute();
            FdoFgfGeometryFactory geomFactory = FdoFgfGeometryFactory.GetInstance();
            int iterations = 0;
            while (rdr.ReadNext())
            {
                Assert.False(rdr.IsNull("EXTENTS"));
                Assert.Equal(FdoPropertyType.FdoPropertyType_GeometricProperty, rdr.GetPropertyType("EXTENTS"));

                FdoByteArrayHandle bytes = rdr.GetGeometryBytes("EXTENTS");
                Assert.NotNull(bytes);
                FdoIGeometry geom = geomFactory.CreateGeometryFromFgf(bytes);
                Assert.NotNull(geom);
                string wkt = geom.Text;
                Assert.NotNull(wkt);
                System.Diagnostics.Debug.WriteLine(string.Format("SpatialExtents() - {0}", wkt));
                iterations++;
            }
            rdr.Close();
            Assert.Equal(1, iterations);
            
            //Re-test with sugar methods
            propNames.Clear();
            Assert.Equal(0, propNames.Count);
            propNames.AddComputedIdentifier("EXTENTS", "SpatialExtents(" + geomName + ")");
            Assert.Equal(1, propNames.Count);

            rdr = selectCmd.Execute();
            iterations = 0;
            while (rdr.ReadNext())
            {
                Assert.False(rdr.IsNull("EXTENTS"));
                Assert.Equal(FdoPropertyType.FdoPropertyType_GeometricProperty, rdr.GetPropertyType("EXTENTS"));

                FdoByteArrayHandle bytes = rdr.GetGeometryBytes("EXTENTS");
                Assert.NotNull(bytes);
                FdoIGeometry geom = geomFactory.CreateGeometryFromFgf(bytes);
                Assert.NotNull(geom);
                string wkt = geom.Text;
                Assert.NotNull(wkt);
                System.Diagnostics.Debug.WriteLine(string.Format("SpatialExtents() - {0}", wkt));
                iterations++;
            }
            rdr.Close();
            Assert.Equal(1, iterations);
        }

        private void DoFilteredSpatialExtents(FdoIConnection conn)
        {
            FdoIDescribeSchema desc = conn.CreateCommand((int)FdoCommandType.FdoCommandType_DescribeSchema) as FdoIDescribeSchema;
            Assert.NotNull(desc);
            FdoFeatureSchemaCollection schemas = desc.Execute();
            Assert.NotNull(schemas);
            Assert.Equal(1, schemas.Count);
            FdoFeatureSchema schema = schemas.GetItem(0);
            FdoClassCollection classes = schema.GetClasses();
            string geomName = null;
            for (int i = 0; i < classes.Count; i++)
            {
                FdoClassDefinition cls = classes.GetItem(i);
                if (cls.Name == "World_Countries")
                {
                    Assert.IsAssignableFrom<FdoFeatureClass>(cls);
                    FdoGeometricPropertyDefinition geomProp = ((FdoFeatureClass)cls).GetGeometryProperty();
                    Assert.NotNull(geomProp);
                    geomName = geomProp.Name;
                }
            }
            Assert.NotNull(geomName);

            FdoISelectAggregates selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_SelectAggregates) as FdoISelectAggregates;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");
            selectCmd.SetFilter("NAME = 'Canada'");

            FdoIdentifierCollection propNames = selectCmd.GetPropertyNames();
            FdoExpression countExpr = FdoExpression.Parse("SpatialExtents(" + geomName + ")");
            FdoComputedIdentifier countIdent = FdoComputedIdentifier.Create("EXTENTS", countExpr);
            propNames.Add(countIdent);

            FdoIDataReader rdr = selectCmd.Execute();
            FdoFgfGeometryFactory geomFactory = FdoFgfGeometryFactory.GetInstance();
            int iterations = 0;
            while (rdr.ReadNext())
            {
                Assert.False(rdr.IsNull("EXTENTS"));
                Assert.Equal(FdoPropertyType.FdoPropertyType_GeometricProperty, rdr.GetPropertyType("EXTENTS"));

                FdoByteArrayHandle bytes = rdr.GetGeometryBytes("EXTENTS");
                Assert.NotNull(bytes);
                FdoIGeometry geom = geomFactory.CreateGeometryFromFgf(bytes);
                Assert.NotNull(geom);
                string wkt = geom.Text;
                Assert.NotNull(wkt);
                System.Diagnostics.Debug.WriteLine(string.Format("SpatialExtents() - {0}", wkt));
                iterations++;
            }
            rdr.Close();
            Assert.Equal(1, iterations);

            //Re-test with sugar methods
            propNames.Clear();
            Assert.Equal(0, propNames.Count);
            propNames.AddComputedIdentifier("EXTENTS", "SpatialExtents(" + geomName + ")");
            Assert.Equal(1, propNames.Count);

            rdr = selectCmd.Execute();
            iterations = 0;
            while (rdr.ReadNext())
            {
                Assert.False(rdr.IsNull("EXTENTS"));
                Assert.Equal(FdoPropertyType.FdoPropertyType_GeometricProperty, rdr.GetPropertyType("EXTENTS"));

                FdoByteArrayHandle bytes = rdr.GetGeometryBytes("EXTENTS");
                Assert.NotNull(bytes);
                FdoIGeometry geom = geomFactory.CreateGeometryFromFgf(bytes);
                Assert.NotNull(geom);
                string wkt = geom.Text;
                Assert.NotNull(wkt);
                System.Diagnostics.Debug.WriteLine(string.Format("SpatialExtents() - {0}", wkt));
                iterations++;
            }
            rdr.Close();
            Assert.Equal(1, iterations);
        }

        [Fact]
        public void TestSDFCount()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.ConnectionString = "File=" + TestDataStore.SDF;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            try
            {
                DoCount(conn);
                DoFilteredCount(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void TestSDFDistinct()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.ConnectionString = "File=" + TestDataStore.SDF;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            try
            {
                DoDistinct(conn);
                DoFilteredDistinct(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void TestSDFSpatialExtents()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.ConnectionString = "File=" + TestDataStore.SDF;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            try
            {
                DoSpatialExtents(conn);
                DoFilteredSpatialExtents(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void TestSHPCount()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.ConnectionString = "DefaultFileLocation=" + TestDataStore.SHP;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            try
            {
                DoCount(conn);
                DoFilteredCount(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void TestSHPDistinct()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.ConnectionString = "DefaultFileLocation=" + TestDataStore.SHP;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            try
            {
                DoDistinct(conn);
                DoFilteredDistinct(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void TestSHPSpatialExtents()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.ConnectionString = "DefaultFileLocation=" + TestDataStore.SHP;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            try
            {
                DoSpatialExtents(conn);
                DoFilteredSpatialExtents(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void TestSQLiteCount()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.ConnectionString = "File=" + TestDataStore.SQLITE;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            try
            {
                DoCount(conn);
                DoFilteredCount(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void TestSQLiteDistinct()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.ConnectionString = "File=" + TestDataStore.SQLITE;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            try
            {
                DoDistinct(conn);
                DoFilteredDistinct(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void TestSQLiteSpatialExtents()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.ConnectionString = "File=" + TestDataStore.SQLITE;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            try
            {
                DoSpatialExtents(conn);
                DoFilteredSpatialExtents(conn);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
