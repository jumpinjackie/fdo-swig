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
            Assert.AreEqual(419L, total, "Expected 419 features");
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
            Assert.AreEqual(66L, total, "Expected 66 features");
        }

        private void DoDistinct(FdoIConnection conn)
        {
            FdoISelectAggregates selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_SelectAggregates) as FdoISelectAggregates;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");
            selectCmd.SetDistinct(true);

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
            Assert.AreEqual(values.Count, count, string.Format("Expected {0} distinct results", values.Count));
        }

        private void DoFilteredDistinct(FdoIConnection conn)
        {
            FdoISelectAggregates selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_SelectAggregates) as FdoISelectAggregates;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");
            selectCmd.SetFilter("NAME = 'Canada'");
            selectCmd.SetDistinct(true);

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
            Assert.AreEqual(values.Count, count, string.Format("Expected {0} distinct results", values.Count));
        }

        private void DoSpatialExtents(FdoIConnection conn)
        {
            FdoIDescribeSchema desc = conn.CreateCommand((int)FdoCommandType.FdoCommandType_DescribeSchema) as FdoIDescribeSchema;
            Assert.NotNull(desc);
            FdoFeatureSchemaCollection schemas = desc.Execute();
            Assert.NotNull(schemas);
            Assert.AreEqual(1, schemas.GetCount());
            FdoFeatureSchema schema = schemas.GetItem(0);
            FdoClassCollection classes = schema.GetClasses();
            string geomName = null;
            for (int i = 0; i < classes.GetCount(); i++)
            {
                FdoClassDefinition cls = classes.GetItem(i);
                if (cls.GetName() == "World_Countries")
                {
                    Assert.IsInstanceOf<FdoFeatureClass>(cls);
                    FdoGeometricPropertyDefinition geomProp = ((FdoFeatureClass)cls).GetGeometryProperty();
                    Assert.NotNull(geomProp);
                    geomName = geomProp.GetName();
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
                Assert.AreEqual(FdoPropertyType.FdoPropertyType_GeometricProperty, rdr.GetPropertyType("EXTENTS"));

                FdoByteArrayHandle bytes = rdr.GetGeometryBytes("EXTENTS");
                Assert.NotNull(bytes);
                FdoIGeometry geom = geomFactory.CreateGeometryFromFgf(bytes);
                Assert.NotNull(geom);
                string wkt = geom.GetText();
                Assert.NotNull(wkt);
                System.Diagnostics.Debug.WriteLine(string.Format("SpatialExtents() - {0}", wkt));
                iterations++;
            }
            rdr.Close();
            Assert.AreEqual(1, iterations, "Expected 1 iteration of the data reader");
        }

        private void DoFilteredSpatialExtents(FdoIConnection conn)
        {
            FdoIDescribeSchema desc = conn.CreateCommand((int)FdoCommandType.FdoCommandType_DescribeSchema) as FdoIDescribeSchema;
            Assert.NotNull(desc);
            FdoFeatureSchemaCollection schemas = desc.Execute();
            Assert.NotNull(schemas);
            Assert.AreEqual(1, schemas.GetCount());
            FdoFeatureSchema schema = schemas.GetItem(0);
            FdoClassCollection classes = schema.GetClasses();
            string geomName = null;
            for (int i = 0; i < classes.GetCount(); i++)
            {
                FdoClassDefinition cls = classes.GetItem(i);
                if (cls.GetName() == "World_Countries")
                {
                    Assert.IsInstanceOf<FdoFeatureClass>(cls);
                    FdoGeometricPropertyDefinition geomProp = ((FdoFeatureClass)cls).GetGeometryProperty();
                    Assert.NotNull(geomProp);
                    geomName = geomProp.GetName();
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
                Assert.AreEqual(FdoPropertyType.FdoPropertyType_GeometricProperty, rdr.GetPropertyType("EXTENTS"));

                FdoByteArrayHandle bytes = rdr.GetGeometryBytes("EXTENTS");
                Assert.NotNull(bytes);
                FdoIGeometry geom = geomFactory.CreateGeometryFromFgf(bytes);
                Assert.NotNull(geom);
                string wkt = geom.GetText();
                Assert.NotNull(wkt);
                System.Diagnostics.Debug.WriteLine(string.Format("SpatialExtents() - {0}", wkt));
                iterations++;
            }
            rdr.Close();
            Assert.AreEqual(1, iterations, "Expected 1 iteration of the data reader");
        }

        [Test]
        public void TestSDFCount()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.SetConnectionString("File=" + TestDataStore.SDF);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
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

        [Test]
        public void TestSDFDistinct()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.SetConnectionString("File=" + TestDataStore.SDF);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
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

        [Test]
        public void TestSDFSpatialExtents()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.SetConnectionString("File=" + TestDataStore.SDF);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
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

        [Test]
        public void TestSHPCount()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.SetConnectionString("DefaultFileLocation=" + TestDataStore.SHP);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
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

        [Test]
        public void TestSHPDistinct()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.SetConnectionString("DefaultFileLocation=" + TestDataStore.SHP);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
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

        [Test]
        public void TestSHPSpatialExtents()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.SetConnectionString("DefaultFileLocation=" + TestDataStore.SHP);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
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

        [Test]
        public void TestSQLiteCount()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.SetConnectionString("File=" + TestDataStore.SQLITE);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
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

        [Test]
        public void TestSQLiteDistinct()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.SetConnectionString("File=" + TestDataStore.SQLITE);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
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

        [Test]
        public void TestSQLiteSpatialExtents()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.SetConnectionString("File=" + TestDataStore.SQLITE);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
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
