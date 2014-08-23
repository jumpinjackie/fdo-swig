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
    public class SelectTests
    {
        private static void TestSelectCommand(FdoISelect selectCmd)
        {
            selectCmd.SetFeatureClassName("World_Countries");

            FdoIFeatureReader reader = selectCmd.Execute();
            try
            {
                int count = 0;
                FdoFeatureClass clsDef = reader.GetClassDefinition() as FdoFeatureClass;
                Assert.NotNull(clsDef);
                FdoFgfGeometryFactory geomFact = FdoFgfGeometryFactory.GetInstance();
                FdoGeometricPropertyDefinition geomProp = clsDef.GetGeometryProperty();
                string geomName = geomProp.Name;
                Assert.NotNull(geomProp);
                while (reader.ReadNext())
                {
                    string name = null;
                    string key = null;
                    string mapkey = null;
                    if (!reader.IsNull("NAME"))
                        name = reader.GetString("NAME");
                    if (!reader.IsNull("KEY"))
                        key = reader.GetString("KEY");
                    if (!reader.IsNull("MAPKEY"))
                        mapkey = reader.GetString("MAPKEY");
                    if (!reader.IsNull(geomName))
                    {
                        FdoByteArrayHandle fgf = reader.GetGeometryBytes(geomName);
                        Assert.NotNull(fgf);
                        FdoIGeometry geom = geomFact.CreateGeometryFromFgf(fgf);
                        Assert.NotNull(geom);
                        string wkt = geom.Text;
                        Assert.IsNotNullOrEmpty(wkt);
                    }
                    count++;
                }
                Assert.AreEqual(419, count, "Expected 419 features");
            }
            finally
            {
                reader.Close();
            }
        }

        private static void TestFilteredSelectCommand(FdoISelect selectCmd)
        {
            selectCmd.SetFeatureClassName("World_Countries");
            selectCmd.SetFilter("NAME = 'Canada'");

            FdoIFeatureReader reader = selectCmd.Execute();
            try
            {
                int count = 0;
                FdoFeatureClass clsDef = reader.GetClassDefinition() as FdoFeatureClass;
                Assert.NotNull(clsDef);
                FdoFgfGeometryFactory geomFact = FdoFgfGeometryFactory.GetInstance();
                FdoGeometricPropertyDefinition geomProp = clsDef.GetGeometryProperty();
                string geomName = geomProp.Name;
                Assert.NotNull(geomProp);
                while (reader.ReadNext())
                {
                    string name = null;
                    string key = null;
                    string mapkey = null;
                    if (!reader.IsNull("NAME"))
                        name = reader.GetString("NAME");
                    if (!reader.IsNull("KEY"))
                        key = reader.GetString("KEY");
                    if (!reader.IsNull("MAPKEY"))
                        mapkey = reader.GetString("MAPKEY");
                    if (!reader.IsNull(geomName))
                    {
                        FdoByteArrayHandle fgf = reader.GetGeometryBytes(geomName);
                        Assert.NotNull(fgf);
                        FdoIGeometry geom = geomFact.CreateGeometryFromFgf(fgf);
                        Assert.NotNull(geom);
                        string wkt = geom.Text;
                        Assert.IsNotNullOrEmpty(wkt);
                    }
                    count++;
                }
                Assert.AreEqual(66, count, "Expected 66 features");
            }
            finally
            {
                reader.Close();
            }
        }

        [Test]
        public void TestSDFSelect()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.SetConnectionString("File=" + TestDataStore.SDF);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            try
            {
                FdoISelect selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Select) as FdoISelect;
                Assert.NotNull(selectCmd);
                TestSelectCommand(selectCmd);
            }
            finally
            {
                conn.Close();
            }
        }

        [Test]
        public void TestSDFSelectFiltered()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.SetConnectionString("File=" + TestDataStore.SDF);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            try
            {
                FdoISelect selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Select) as FdoISelect;
                Assert.NotNull(selectCmd);
                TestFilteredSelectCommand(selectCmd);
            }
            finally
            {
                conn.Close();
            }
        }

        [Test]
        public void TestSHPSelect()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.SetConnectionString("DefaultFileLocation=" + TestDataStore.SHP);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            try
            {
                FdoISelect selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Select) as FdoISelect;
                Assert.NotNull(selectCmd);
                TestSelectCommand(selectCmd);
            }
            finally
            {
                conn.Close();
            }
        }

        [Test]
        public void TestSHPSelectFiltered()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.SetConnectionString("DefaultFileLocation=" + TestDataStore.SHP);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            try
            {
                FdoISelect selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Select) as FdoISelect;
                Assert.NotNull(selectCmd);
                TestFilteredSelectCommand(selectCmd);
            }
            finally
            {
                conn.Close();
            }
        }

        [Test]
        public void TestSQLiteSelect()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.SetConnectionString("File=" + TestDataStore.SQLITE);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            try
            {
                FdoISelect selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Select) as FdoISelect;
                Assert.NotNull(selectCmd);
                TestSelectCommand(selectCmd);
            }
            finally
            {
                conn.Close();
            }
        }

        [Test]
        public void TestSQLiteSelectFiltered()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.SetConnectionString("File=" + TestDataStore.SQLITE);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            try
            {
                FdoISelect selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Select) as FdoISelect;
                Assert.NotNull(selectCmd);
                TestFilteredSelectCommand(selectCmd);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
