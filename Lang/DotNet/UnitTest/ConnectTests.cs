using OSGeo.FDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class ConnectTests
    {
        [Fact]
        public void TestSDF()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.ConnectionString = "File=" + TestDataStore.SDF;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            conn.Close();

            conn.ConnectionString = "File=../IDontExist.sdf";
            Assert.Throws<ManagedFdoException>(() =>
            {
                FdoConnectionState state = conn.Open();
            });
        }

        [Fact]
        public void TestSHP()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.ConnectionString = "DefaultFileLocation=" + TestDataStore.SHP;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            conn.Close();

            conn.ConnectionString = "DefaultFileLocation=" + TestDataStore.SHP_DIR;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            conn.Close();

            conn.ConnectionString = "File=../IDontExist.shp";
            Assert.Throws<ManagedFdoException>(() =>
            {
                FdoConnectionState state = conn.Open();
            });
        }

        [Fact]
        public void TestSQLite()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.ConnectionString = "File=" + TestDataStore.SQLITE;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            conn.Close();

            conn.ConnectionString = "File=../IDontExist.sqlite";
            Assert.Throws<ManagedFdoException>(() =>
            {
                FdoConnectionState state = conn.Open();
            });
        }
    }
}
