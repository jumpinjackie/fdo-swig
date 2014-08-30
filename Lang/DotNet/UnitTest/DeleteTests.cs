using OSGeo.FDO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class DeleteTests : IDisposable
    {
        const string SDF = "DeleteTest/SDF/DeleteTest.sdf";
        const string SHP = "DeleteTest/SHP";
        const string SQLITE = "DeleteTest/SQLite/DeleteTest.sqlite";

        public DeleteTests()
        {
            Directory.CreateDirectory("DeleteTest");
            Directory.CreateDirectory("DeleteTest/SDF");
            Directory.CreateDirectory("DeleteTest/SHP");
            Directory.CreateDirectory("DeleteTest/SQLite");
            TestDataStore.CopySDF(SDF);
            TestDataStore.CopySQLite(SQLITE);
            TestDataStore.CopySHP(SHP);
        }

        public void Dispose()
        {
            try
            {
                Directory.Delete("DeleteTest", true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}{0}WARNING: Exception during teardown:{0}{1}{0}", Environment.NewLine, ex.ToString());
            }
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
            geomName = geomProp.Name;

            FdoIDelete deleteCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Delete) as FdoIDelete;
            Assert.NotNull(deleteCmd);

            deleteCmd.SetFeatureClassName("World_Countries");
            deleteCmd.SetFilter("NAME = 'Canada'");

            int updated = deleteCmd.Execute();
            Assert.Equal(66, updated);

            FdoISelect selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Select) as FdoISelect;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");
            selectCmd.SetFilter("NAME = 'Canada'");

            FdoIFeatureReader fr = selectCmd.Execute();
            Assert.False(fr.ReadNext());
            fr.Close();
        }

        [Fact]
        public void TestSDFDelete()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.ConnectionString = "File=" + SDF;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            try
            {
                DoDelete(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void TestSHPDelete()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.ConnectionString = "DefaultFileLocation=" + SHP;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            try
            {
                DoDelete(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void TestSQLiteDelete()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.ConnectionString = "File=" + SQLITE;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());

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
