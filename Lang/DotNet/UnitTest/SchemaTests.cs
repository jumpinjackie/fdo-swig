using OSGeo.FDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class SchemaTests
    {
        private static void VerifyClass(FdoClassDefinition clsDef)
        {
            Assert.Equal(FdoClassType.FdoClassType_FeatureClass, clsDef.ClassType);
            Assert.IsAssignableFrom<FdoFeatureClass>(clsDef);
            FdoFeatureClass featCls = (FdoFeatureClass)clsDef;

            FdoPropertyDefinitionCollection clsProps = clsDef.GetProperties();
            Assert.Equal(5, clsProps.Count);

            FdoDataPropertyDefinitionCollection clsIdProps = clsDef.GetIdentityProperties();
            Assert.Equal(1, clsIdProps.Count);

            FdoGeometricPropertyDefinition geomProp = featCls.GetGeometryProperty();
            Assert.NotNull(geomProp);

            Assert.True(clsProps.IndexOf("NAME") >= 0);
            Assert.True(clsProps.IndexOf("KEY") >= 0);
            Assert.True(clsProps.IndexOf("MAPKEY") >= 0);
            Assert.True(clsProps.IndexOf(geomProp) >= 0);

            var name = clsProps.GetItem("NAME");
            var key = clsProps.GetItem("KEY");
            var mapkey = clsProps.GetItem("MAPKEY");

            Assert.IsAssignableFrom<FdoDataPropertyDefinition>(name);
            Assert.IsAssignableFrom<FdoDataPropertyDefinition>(key);
            Assert.IsAssignableFrom<FdoDataPropertyDefinition>(mapkey);
        }

        [Fact]
        public void DescribeSDFSchema()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.ConnectionString = "File=" + TestDataStore.SDF;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            FdoIDescribeSchema desc = conn.CreateCommand((int)FdoCommandType.FdoCommandType_DescribeSchema) as FdoIDescribeSchema;
            Assert.NotNull(desc);
            FdoFeatureSchemaCollection schemas = desc.Execute();
            Assert.Equal(1, schemas.Count);
            
            FdoFeatureSchema schema = schemas.GetItem(0);
            FdoClassCollection classes = schema.GetClasses();
            Assert.Equal(1, classes.Count);

            FdoClassDefinition clsDef = classes.GetItem(0);
            VerifyClass(clsDef);
            
            //Re-test with sugar
            clsDef = schemas.GetClassDefinition(schema.Name, "World_Countries");
            Assert.NotNull(clsDef);
            VerifyClass(clsDef);
            clsDef = schemas.GetClassDefinition(null, "World_Countries");
            Assert.NotNull(clsDef);
            VerifyClass(clsDef);
            Assert.Throws<ManagedFdoException>(() => schemas.GetClassDefinition(null, "WorldCountries"));
            Assert.Throws<ManagedFdoException>(() => schemas.GetClassDefinition("BogusSchema", "World_Countries"));
        }

        [Fact]
        public void DescribeSHPSchema()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.ConnectionString = "DefaultFileLocation=" + TestDataStore.SHP;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            FdoIDescribeSchema desc = conn.CreateCommand((int)FdoCommandType.FdoCommandType_DescribeSchema) as FdoIDescribeSchema;
            Assert.NotNull(desc);
            FdoFeatureSchemaCollection schemas = desc.Execute();
            Assert.Equal(1, schemas.Count);

            FdoFeatureSchema schema = schemas.GetItem(0);
            FdoClassCollection classes = schema.GetClasses();
            Assert.Equal(1, classes.Count);

            FdoClassDefinition clsDef = classes.GetItem(0);
            VerifyClass(clsDef);

            //Re-test with sugar
            clsDef = schemas.GetClassDefinition(schema.Name, "World_Countries");
            Assert.NotNull(clsDef);
            VerifyClass(clsDef);
            clsDef = schemas.GetClassDefinition(null, "World_Countries");
            Assert.NotNull(clsDef);
            VerifyClass(clsDef);
            Assert.Throws<ManagedFdoException>(() => schemas.GetClassDefinition(null, "WorldCountries"));
            Assert.Throws<ManagedFdoException>(() => schemas.GetClassDefinition("BogusSchema", "World_Countries"));
        }

        [Fact]
        public void DescribeSQLiteSchema()
        {
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.ConnectionString = "File=" + TestDataStore.SQLITE;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());

            FdoIDescribeSchema desc = conn.CreateCommand((int)FdoCommandType.FdoCommandType_DescribeSchema) as FdoIDescribeSchema;
            Assert.NotNull(desc);
            FdoFeatureSchemaCollection schemas = desc.Execute();
            Assert.Equal(1, schemas.Count);

            FdoFeatureSchema schema = schemas.GetItem(0);
            FdoClassCollection classes = schema.GetClasses();
            Assert.Equal(1, classes.Count);

            FdoClassDefinition clsDef = classes.GetItem(0);
            VerifyClass(clsDef);

            //Re-test with sugar
            clsDef = schemas.GetClassDefinition(schema.Name, "World_Countries");
            Assert.NotNull(clsDef);
            VerifyClass(clsDef);
            clsDef = schemas.GetClassDefinition(null, "World_Countries");
            Assert.NotNull(clsDef);
            VerifyClass(clsDef);
            Assert.Throws<ManagedFdoException>(() => schemas.GetClassDefinition(null, "WorldCountries"));
            Assert.Throws<ManagedFdoException>(() => schemas.GetClassDefinition("BogusSchema", "World_Countries"));
        }
    }
}
