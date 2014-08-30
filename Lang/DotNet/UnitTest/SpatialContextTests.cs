using OSGeo.FDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class SpatialContextTests
    {
        private void DoGetSpatialContexts(FdoIConnection conn)
        {
            FdoIGetSpatialContexts cmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_GetSpatialContexts) as FdoIGetSpatialContexts;
            Assert.NotNull(cmd);
            FdoISpatialContextReader scReader = cmd.Execute();
            int count = 0;
            while (scReader.ReadNext())
            {
                Assert.NotNull(scReader.Name);
                Assert.NotEmpty(scReader.Name);
                count++;
            }
            Assert.Equal(1, count);
        }

        [Fact]
        public void TestSDFSpatialContext()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.ConnectionString = "File=" + TestDataStore.SDF;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            try
            {
                DoGetSpatialContexts(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void TestSHPSpatialContext()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.ConnectionString = "DefaultFileLocation=" + TestDataStore.SHP;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            try
            {
                DoGetSpatialContexts(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void TestSQLiteSpatialContext()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.ConnectionString = "File=" + TestDataStore.SQLITE;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            try
            {
                DoGetSpatialContexts(conn);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
