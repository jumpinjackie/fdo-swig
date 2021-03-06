﻿using OSGeo.FDO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class UpdateTests : IDisposable
    {
        const string SDF = "UpdateTest/SDF/UpdateTest.sdf";
        const string SHP = "UpdateTest/SHP";
        const string SQLITE = "UpdateTest/SQLite/UpdateTest.sqlite";

        public UpdateTests()
        {
            Directory.CreateDirectory("UpdateTest");
            Directory.CreateDirectory("UpdateTest/SDF");
            Directory.CreateDirectory("UpdateTest/SHP");
            Directory.CreateDirectory("UpdateTest/SQLite");
            TestDataStore.CopySDF(SDF);
            TestDataStore.CopySQLite(SQLITE);
            TestDataStore.CopySHP(SHP);
        }

        public void Dispose()
        {
            try
            {
                Directory.Delete("UpdateTest", true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}{0}WARNING: Exception during teardown:{0}{1}{0}", Environment.NewLine, ex.ToString());
            }
        }

        private void DoUpdate(FdoIConnection conn)
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

            FdoIUpdate updateCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Update) as FdoIUpdate;
            Assert.NotNull(updateCmd);

            updateCmd.SetFeatureClassName("World_Countries");
            updateCmd.SetFilter("NAME = 'Canada'");

            FdoFgfGeometryFactory geomFactory = FdoFgfGeometryFactory.GetInstance();
            FdoPropertyValueCollection propVals = updateCmd.GetPropertyValues();

            FdoStringValue nameVal = FdoStringValue.Create();
            Assert.True(nameVal.IsNull());
            FdoStringValue keyVal = FdoStringValue.Create();
            Assert.True(keyVal.IsNull());
            FdoStringValue mapkeyVal = FdoStringValue.Create();
            Assert.True(mapkeyVal.IsNull());
            FdoGeometryValue geomVal = FdoGeometryValue.Create();
            Assert.True(geomVal.IsNull());

            FdoPropertyValue pKey = FdoPropertyValue.Create("KEY", keyVal);
            FdoPropertyValue pMapKey = FdoPropertyValue.Create("MAPKEY", mapkeyVal);

            propVals.Add(pKey);
            propVals.Add(pMapKey);

            //Set the actual values
            keyVal.String = "MOC";
            mapkeyVal.String = "MOC123";

            int updated = updateCmd.Execute();
            Assert.Equal(66, updated);

            FdoISelect selectCmd = conn.CreateCommand((int)FdoCommandType.FdoCommandType_Select) as FdoISelect;
            Assert.NotNull(selectCmd);
            selectCmd.SetFeatureClassName("World_Countries");
            selectCmd.SetFilter("NAME = 'Canada'");

            FdoIFeatureReader fr = selectCmd.Execute();
            while (fr.ReadNext())
            {
                Assert.False(fr.IsNull("KEY"));
                Assert.False(fr.IsNull("MAPKEY"));
                Assert.Equal(fr.GetString("KEY"), "MOC");
                Assert.Equal(fr.GetString("MAPKEY"), "MOC123");
            }
            fr.Close();
        }

        [Fact]
        public void TestSDFUpdate()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.ConnectionString = "File=" + SDF;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            try
            {
                DoUpdate(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void TestSHPUpdate()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.ConnectionString = "DefaultFileLocation=" + SHP;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            try
            {
                DoUpdate(conn);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void TestSQLiteUpdate()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.ConnectionString = "File=" + SQLITE;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            try
            {
                DoUpdate(conn);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
