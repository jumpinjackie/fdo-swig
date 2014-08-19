using NUnit.Framework;
using OSGeo.FDO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestFixture]
    public class DeleteTests
    {
        const string SDF = "DeleteTest/SDF/DeleteTest.sdf";
        const string SHP = "DeleteTest/SHP";
        const string SQLITE = "DeleteTest/SQLite/DeleteTest.sqlite";

        [TestFixtureSetUp]
        public void Setup()
        {
            Directory.CreateDirectory("DeleteTest");
            Directory.CreateDirectory("DeleteTest/SDF");
            Directory.CreateDirectory("DeleteTest/SHP");
            Directory.CreateDirectory("DeleteTest/SQLite");
            TestDataStore.CopySDF(SDF);
            TestDataStore.CopySQLite(SQLITE);
            TestDataStore.CopySHP(SHP);
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
            Directory.Delete("DeleteTest", true);
        }

        private void DoDelete(FdoIConnection conn)
        {
            string geomName = null;
            FdoIDescribeSchema desc = conn.CreateCommand((int)FdoCommandType.FdoCommandType_DescribeSchema) as FdoIDescribeSchema;
            Assert.NotNull(desc);
            FdoFeatureSchemaCollection schemas = desc.Execute();
            Assert.NotNull(schemas);
            FdoFeatureClass clsDef = schemas.GetClassDefinition(null, "World_Countries") as FdoFeatureClass;
            Assert.NotNull(clsDef);
            FdoGeometricPropertyDefinition geomProp = clsDef.GetGeometryProperty();
            Assert.NotNull(geomProp);
            geomName = geomProp.GetName();

            FdoIDelete deleteCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Delete) as FdoIDelete;
            Assert.NotNull(deleteCmd);

            deleteCmd.SetFeatureClassName("World_Countries");
            deleteCmd.SetFilter("NAME = 'Canada'");

            int updated = deleteCmd.Execute();
            Assert.AreEqual(66, updated, "Expect 66 features deleted");

            FdoISelect selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Select) as FdoISelect;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");
            selectCmd.SetFilter("NAME = 'Canada'");

            FdoIFeatureReader fr = selectCmd.Execute();
            Assert.False(fr.ReadNext());
            fr.Close();
        }

        [Test]
        public void TestSDFDelete()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.SetConnectionString("File=" + SDF);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            try
            {
                DoDelete(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Test]
        public void TestSHPDelete()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.SetConnectionString("DefaultFileLocation=" + SHP);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            try
            {
                DoDelete(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Test]
        public void TestSQLiteDelete()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.SetConnectionString("File=" + SQLITE);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            try
            {
                DoDelete(conn);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
