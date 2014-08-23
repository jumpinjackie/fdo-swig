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
    public class InsertTests
    {
        const string SDF = "InsertTest/SDF/InsertTest.sdf";
        const string SHP = "InsertTest/SHP";
        const string SQLITE = "InsertTest/SQLite/InsertTest.sqlite";

        [TestFixtureSetUp]
        public void Setup()
        {
            Directory.CreateDirectory("InsertTest");
            Directory.CreateDirectory("InsertTest/SDF");
            Directory.CreateDirectory("InsertTest/SHP");
            Directory.CreateDirectory("InsertTest/SQLite");
            TestDataStore.CopySDF(SDF);
            TestDataStore.CopySQLite(SQLITE);
            TestDataStore.CopySHP(SHP);
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
            Directory.Delete("InsertTest", true);
        }

        private void DoInsert(FdoIConnection conn)
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

            FdoIInsert insertCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Insert) as FdoIInsert;
            Assert.NotNull(insertCmd);

            insertCmd.SetFeatureClassName("World_Countries");

            FdoFgfGeometryFactory geomFactory = FdoFgfGeometryFactory.GetInstance();
            FdoPropertyValueCollection propVals = insertCmd.GetPropertyValues();

            FdoStringValue nameVal = FdoStringValue.Create();
            Assert.True(nameVal.IsNull());
            FdoStringValue keyVal = FdoStringValue.Create();
            Assert.True(keyVal.IsNull());
            FdoStringValue mapkeyVal = FdoStringValue.Create();
            Assert.True(mapkeyVal.IsNull());
            FdoGeometryValue geomVal = FdoGeometryValue.Create();
            Assert.True(geomVal.IsNull());

            FdoPropertyValue pName = FdoPropertyValue.Create("NAME", nameVal);
            FdoPropertyValue pKey = FdoPropertyValue.Create("KEY", keyVal);
            FdoPropertyValue pMapKey = FdoPropertyValue.Create("MAPKEY", mapkeyVal);
            FdoPropertyValue pGeom = FdoPropertyValue.Create(geomName, geomVal);

            propVals.Add(pName);
            propVals.Add(pKey);
            propVals.Add(pMapKey);
            propVals.Add(pGeom);

            //Set the actual values
            nameVal.String = "My Own Country";
            keyVal.String = "MOC";
            mapkeyVal.String = "MOC123";
            FdoIGeometry geom = geomFactory.CreateGeometry("POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))");
            FdoByteArrayHandle fgf = geomFactory.GetFgfBytes(geom);
            geomVal.SetGeometryBytes(fgf);

            int inserted = GetInsertedFeatures(insertCmd);
            Assert.AreEqual(1, inserted, "Expected 1 feature inserted");

            int count = GetFeatureCountForName(conn, "My Own Country");
            Assert.AreEqual(1, count, "Expected 1 feature");

            mapkeyVal.String = "MOC234";
            Assert.AreEqual(1, GetInsertedFeatures(insertCmd), "Expected 1 feature inserted");
            Assert.AreEqual(2, GetFeatureCountForName(conn, "My Own Country"));
            Assert.AreEqual(1, GetFeatureCountForMapKey(conn, "MOC123"));
            Assert.AreEqual(1, GetFeatureCountForMapKey(conn, "MOC234"));
        }

        private static int GetInsertedFeatures(FdoIInsert insertCmd)
        {
            FdoIFeatureReader reader = insertCmd.Execute();
            int inserted = 0;
            try
            {
                while (reader.ReadNext())
                {
                    inserted++;
                }
            }
            finally
            {
                reader.Close();
            }
            return inserted;
        }

        private static int GetFeatureCountForName(FdoIConnection conn, string name)
        {
            FdoISelect selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Select) as FdoISelect;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");
            selectCmd.SetFilter("NAME = '" + name + "'");
            FdoIFeatureReader selReader = selectCmd.Execute();
            int count = 0;
            try
            {
                while (selReader.ReadNext())
                {
                    count++;
                }
            }
            finally
            {
                selReader.Close();
            }
            return count;
        }

        private static int GetFeatureCountForMapKey(FdoIConnection conn, string key)
        {
            FdoISelect selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Select) as FdoISelect;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");
            selectCmd.SetFilter("MAPKEY = '" + key + "'");
            FdoIFeatureReader selReader = selectCmd.Execute();
            int count = 0;
            try
            {
                while (selReader.ReadNext())
                {
                    count++;
                }
            }
            finally
            {
                selReader.Close();
            }
            return count;
        }

        [Test]
        public void TestSDFInsert()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.SetConnectionString("File=" + SDF);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            try
            {
                DoInsert(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Test]
        public void TestSHPInsert()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.SetConnectionString("DefaultFileLocation=" + SHP);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            try
            {
                DoInsert(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Test]
        public void TestSQLiteInsert()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.SetConnectionString("File=" + SQLITE);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            try
            {
                DoInsert(conn);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
