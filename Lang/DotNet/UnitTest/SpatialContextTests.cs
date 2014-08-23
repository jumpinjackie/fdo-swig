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
                Assert.IsNotNullOrEmpty(scReader.Name);
                count++;
            }
            Assert.AreEqual(1, count, "Expect one spatial context in data store");
        }

        [Test]
        public void TestSDFSpatialContext()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.SetConnectionString("File=" + TestDataStore.SDF);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            try
            {
                DoGetSpatialContexts(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Test]
        public void TestSHPSpatialContext()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.SetConnectionString("DefaultFileLocation=" + TestDataStore.SHP);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            try
            {
                DoGetSpatialContexts(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Test]
        public void TestSQLiteSpatialContext()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.SetConnectionString("File=" + TestDataStore.SQLITE);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
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
