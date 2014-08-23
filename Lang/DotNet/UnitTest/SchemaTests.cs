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
    public class SchemaTests
    {
        private static void VerifyClass(FdoClassDefinition clsDef)
        {
            Assert.AreEqual(FdoClassType.FdoClassType_FeatureClass, clsDef.ClassType);
            Assert.IsInstanceOf<FdoFeatureClass>(clsDef);
            FdoFeatureClass featCls = (FdoFeatureClass)clsDef;

            FdoPropertyDefinitionCollection clsProps = clsDef.GetProperties();
            Assert.AreEqual(5, clsProps.GetCount());

            FdoDataPropertyDefinitionCollection clsIdProps = clsDef.GetIdentityProperties();
            Assert.AreEqual(1, clsIdProps.GetCount());

            FdoGeometricPropertyDefinition geomProp = featCls.GetGeometryProperty();
            Assert.NotNull(geomProp);

            Assert.GreaterOrEqual(clsProps.IndexOf("NAME"), 0);
            Assert.GreaterOrEqual(clsProps.IndexOf("KEY"), 0);
            Assert.GreaterOrEqual(clsProps.IndexOf("MAPKEY"), 0);
            Assert.GreaterOrEqual(clsProps.IndexOf(geomProp), 0);

            var name = clsProps.GetItem("NAME");
            var key = clsProps.GetItem("KEY");
            var mapkey = clsProps.GetItem("MAPKEY");

            Assert.IsInstanceOf<FdoDataPropertyDefinition>(name);
            Assert.IsInstanceOf<FdoDataPropertyDefinition>(key);
            Assert.IsInstanceOf<FdoDataPropertyDefinition>(mapkey);
        }

        [Test]
        public void DescribeSDFSchema()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.SetConnectionString("File=" + TestDataStore.SDF);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            FdoIDescribeSchema desc = conn.CreateCommand((int)FdoCommandType.FdoCommandType_DescribeSchema) as FdoIDescribeSchema;
            Assert.NotNull(desc);
            FdoFeatureSchemaCollection schemas = desc.Execute();
            Assert.AreEqual(1, schemas.GetCount());
            
            FdoFeatureSchema schema = schemas.GetItem(0);
            FdoClassCollection classes = schema.GetClasses();
            Assert.AreEqual(1, classes.GetCount());

            FdoClassDefinition clsDef = classes.GetItem(0);
            VerifyClass(clsDef);
            
            //Re-test with sugar
            clsDef = schemas.GetClassDefinition(schema.Name, "World_Countries");
            Assert.NotNull(clsDef);
            VerifyClass(clsDef);
            clsDef = schemas.GetClassDefinition(null, "World_Countries");
            Assert.NotNull(clsDef);
            VerifyClass(clsDef);
            Assert.Null(schemas.GetClassDefinition(null, "WorldCountries"));
            Assert.Null(schemas.GetClassDefinition("BogusSchema", "World_Countries"));
        }

        [Test]
        public void DescribeSHPSchema()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.SetConnectionString("DefaultFileLocation=" + TestDataStore.SHP);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            FdoIDescribeSchema desc = conn.CreateCommand((int)FdoCommandType.FdoCommandType_DescribeSchema) as FdoIDescribeSchema;
            Assert.NotNull(desc);
            FdoFeatureSchemaCollection schemas = desc.Execute();
            Assert.AreEqual(1, schemas.GetCount());

            FdoFeatureSchema schema = schemas.GetItem(0);
            FdoClassCollection classes = schema.GetClasses();
            Assert.AreEqual(1, classes.GetCount());

            FdoClassDefinition clsDef = classes.GetItem(0);
            VerifyClass(clsDef);

            //Re-test with sugar
            clsDef = schemas.GetClassDefinition(schema.Name, "World_Countries");
            Assert.NotNull(clsDef);
            VerifyClass(clsDef);
            clsDef = schemas.GetClassDefinition(null, "World_Countries");
            Assert.NotNull(clsDef);
            VerifyClass(clsDef);
            Assert.Null(schemas.GetClassDefinition(null, "WorldCountries"));
            Assert.Null(schemas.GetClassDefinition("BogusSchema", "World_Countries"));
        }

        [Test]
        public void DescribeSQLiteSchema()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.SetConnectionString("File=" + TestDataStore.SQLITE);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            FdoIDescribeSchema desc = conn.CreateCommand((int)FdoCommandType.FdoCommandType_DescribeSchema) as FdoIDescribeSchema;
            Assert.NotNull(desc);
            FdoFeatureSchemaCollection schemas = desc.Execute();
            Assert.AreEqual(1, schemas.GetCount());

            FdoFeatureSchema schema = schemas.GetItem(0);
            FdoClassCollection classes = schema.GetClasses();
            Assert.AreEqual(1, classes.GetCount());

            FdoClassDefinition clsDef = classes.GetItem(0);
            VerifyClass(clsDef);

            //Re-test with sugar
            clsDef = schemas.GetClassDefinition(schema.Name, "World_Countries");
            Assert.NotNull(clsDef);
            VerifyClass(clsDef);
            clsDef = schemas.GetClassDefinition(null, "World_Countries");
            Assert.NotNull(clsDef);
            VerifyClass(clsDef);
            Assert.Null(schemas.GetClassDefinition(null, "WorldCountries"));
            Assert.Null(schemas.GetClassDefinition("BogusSchema", "World_Countries"));
        }
    }
}
