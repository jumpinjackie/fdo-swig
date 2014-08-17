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
        [Test]
        public void TestSDFSelect()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.SetConnectionString("File=" + TestDataStore.SDF);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            FdoISelect selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Select) as FdoISelect;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");

            FdoIFeatureReader reader = selectCmd.Execute();
            int count = 0;
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
                count++;
            }
            reader.Close();
            Assert.AreEqual(419, count, "Expected 419 features");
        }

        [Test]
        public void TestSDFSelectFiltered()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.SetConnectionString("File=" + TestDataStore.SDF);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            FdoISelect selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Select) as FdoISelect;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");
            selectCmd.SetFilter("NAME = 'Canada'");

            FdoIFeatureReader reader = selectCmd.Execute();
            int count = 0;
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
                count++;
            }
            reader.Close();
            Assert.AreEqual(66, count, "Expected 66 features");
        }

        [Test]
        public void TestSHPSelect()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.SetConnectionString("DefaultFileLocation=" + TestDataStore.SHP);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            FdoISelect selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Select) as FdoISelect;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");

            FdoIFeatureReader reader = selectCmd.Execute();
            int count = 0;
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
                count++;
            }
            reader.Close();
            Assert.AreEqual(419, count, "Expected 419 features");
        }

        [Test]
        public void TestSHPSelectFiltered()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.SetConnectionString("DefaultFileLocation=" + TestDataStore.SHP);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            FdoISelect selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Select) as FdoISelect;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");
            selectCmd.SetFilter("NAME = 'Canada'");

            FdoIFeatureReader reader = selectCmd.Execute();
            int count = 0;
            while (reader.ReadNext())
            {
                count++;
            }
            reader.Close();
            Assert.AreEqual(66, count, "Expected 66 features");
        }

        [Test]
        public void TestSQLiteSelect()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.SetConnectionString("File=" + TestDataStore.SQLITE);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            FdoISelect selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Select) as FdoISelect;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");

            FdoIFeatureReader reader = selectCmd.Execute();
            int count = 0;
            while (reader.ReadNext())
            {
                count++;
            }
            reader.Close();
            Assert.AreEqual(419, count, "Expected 419 features");
        }

        [Test]
        public void TestSQLiteSelectFiltered()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.SetConnectionString("File=" + TestDataStore.SQLITE);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            FdoISelect selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Select) as FdoISelect;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");
            selectCmd.SetFilter("NAME = 'Canada'");

            FdoIFeatureReader reader = selectCmd.Execute();
            int count = 0;
            while (reader.ReadNext())
            {
                count++;
            }
            reader.Close();
            Assert.AreEqual(66, count, "Expected 66 features");
        }
    }
}
