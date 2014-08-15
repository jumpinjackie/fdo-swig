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
    public class ConnectTests
    {
        [Test]
        public void TestSDF()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.SetConnectionString("File=../../../../../../TestData/SDF/World_Countries.sdf");
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            conn.Close();

            conn.SetConnectionString("File=../IDontExist.sdf");
            Assert.Throws<ManagedFdoException>(() =>
            {
                FdoConnectionState state = conn.Open();
            });
        }

        [Test]
        public void TestSHP()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.SetConnectionString("DefaultFileLocation=../../../../../../TestData/SHP/World_Countries.shp");
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            conn.Close();

            conn.SetConnectionString("DefaultFileLocation=../../../../../../TestData/SHP/");
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            conn.Close();

            conn.SetConnectionString("File=../IDontExist.shp");
            Assert.Throws<ManagedFdoException>(() =>
            {
                FdoConnectionState state = conn.Open();
            });
        }

        [Test]
        public void TestSQLite()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.SetConnectionString("File=../../../../../../TestData/SQLite/World_Countries.sqlite");
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            conn.Close();

            conn.SetConnectionString("File=../IDontExist.sqlite");
            Assert.Throws<ManagedFdoException>(() =>
            {
                FdoConnectionState state = conn.Open();
            });
        }
    }
}
