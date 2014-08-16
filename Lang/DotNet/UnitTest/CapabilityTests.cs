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
    public class CapabilityTests
    {
        private static void ValidateCommandType(FdoICommand cmd, int cmdType)
        {
            //Custom FDO commands may fall out of this range, so only test for
            //command types that fall within what's defined in FdoCommandType
            if (Enum.IsDefined(typeof(FdoCommandType), cmdType))
            {
                FdoCommandType ctype = (FdoCommandType)cmdType;
                switch (ctype)
                {
                    case FdoCommandType.FdoCommandType_AcquireLock:
                        Assert.IsInstanceOf<FdoIAcquireLock>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_ActivateLongTransaction:
                        Assert.IsInstanceOf<FdoIActivateLongTransaction>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_ActivateLongTransactionCheckpoint:
                        Assert.IsInstanceOf<FdoIActivateLongTransactionCheckpoint>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_ActivateSpatialContext:
                        Assert.IsInstanceOf<FdoIActivateSpatialContext>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_ApplySchema:
                        Assert.IsInstanceOf<FdoIApplySchema>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_ChangeLongTransactionPrivileges:
                        Assert.IsInstanceOf<FdoIChangeLongTransactionPrivileges>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_ChangeLongTransactionSet:
                        Assert.IsInstanceOf<FdoIChangeLongTransactionSet>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_CommitLongTransaction:
                        Assert.IsInstanceOf<FdoICommitLongTransaction>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_CreateDataStore:
                        Assert.IsInstanceOf<FdoICreateDataStore>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_CreateLongTransaction:
                        Assert.IsInstanceOf<FdoICreateLongTransaction>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_CreateLongTransactionCheckpoint:
                        Assert.IsInstanceOf<FdoICreateLongTransactionCheckpoint>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_CreateMeasureUnit:
                        Assert.IsInstanceOf<FdoICreateMeasureUnit>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_CreateSpatialContext:
                        Assert.IsInstanceOf<FdoICreateSpatialContext>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_DeactivateLongTransaction:
                        Assert.IsInstanceOf<FdoIDeactivateLongTransaction>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_Delete:
                        Assert.IsInstanceOf<FdoIDelete>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_DescribeSchema:
                        Assert.IsInstanceOf<FdoIDescribeSchema>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_DescribeSchemaMapping:
                        Assert.IsInstanceOf<FdoIDescribeSchemaMapping>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_DestroyDataStore:
                        Assert.IsInstanceOf<FdoIDestroyDataStore>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_DestroyMeasureUnit:
                        Assert.IsInstanceOf<FdoIDestroyMeasureUnit>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_DestroySchema:
                        Assert.IsInstanceOf<FdoIDestroySchema>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_DestroySpatialContext:
                        Assert.IsInstanceOf<FdoIDestroySpatialContext>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_ExtendedSelect:
                        Assert.IsInstanceOf<FdoIExtendedSelect>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_FreezeLongTransaction:
                        Assert.IsInstanceOf<FdoIFreezeLongTransaction>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetClassNames:
                        Assert.IsInstanceOf<FdoIGetClassNames>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetLockedObjects:
                        Assert.IsInstanceOf<FdoIGetLockedObjects>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetLockInfo:
                        Assert.IsInstanceOf<FdoIGetLockInfo>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetLockOwners:
                        Assert.IsInstanceOf<FdoIGetLockOwners>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetLongTransactionCheckpoints:
                        Assert.IsInstanceOf<FdoIGetLongTransactionCheckpoints>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetLongTransactionPrivileges:
                        Assert.IsInstanceOf<FdoIGetLongTransactionPrivileges>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetLongTransactions:
                        Assert.IsInstanceOf<FdoIGetLongTransactions>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetLongTransactionsInSet:
                        Assert.IsInstanceOf<FdoIGetLongTransactionsInSet>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetMeasureUnits:
                        Assert.IsInstanceOf<FdoIGetMeasureUnits>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetSchemaNames:
                        Assert.IsInstanceOf<FdoIGetSchemaNames>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetSpatialContexts:
                        Assert.IsInstanceOf<FdoIGetSpatialContexts>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_Insert:
                        Assert.IsInstanceOf<FdoIInsert>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_ListDataStores:
                        Assert.IsInstanceOf<FdoIListDataStores>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_ReleaseLock:
                        Assert.IsInstanceOf<FdoIReleaseLock>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_RollbackLongTransaction:
                        Assert.IsInstanceOf<FdoIRollbackLongTransaction>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_RollbackLongTransactionCheckpoint:
                        Assert.IsInstanceOf<FdoIRollbackLongTransactionCheckpoint>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_Select:
                        Assert.IsInstanceOf<FdoISelect>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_SelectAggregates:
                        Assert.IsInstanceOf<FdoISelectAggregates>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_SQLCommand:
                        Assert.IsInstanceOf<FdoISQLCommand>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_Update:
                        Assert.IsInstanceOf<FdoIUpdate>(cmd);
                        break;
                    default:
                        Assert.Fail("Command for {0} not exposed to FDO wrapper API", ctype);
                        break;
                }
            }
        }

        private static void ProcessCommandCapabilities(FdoIConnection conn)
        {
            Console.WriteLine("Command Capabilities");
            var caps = conn.GetCommandCapabilities();
            var cmds = caps.SupportedCommands();
            Console.WriteLine("\tSupportedCommands:");
            for (int i = 0; i < cmds.GetCount(); i++)
            {
                int cmdType = cmds.GetItem(i);
                try
                {
                    FdoICommand cmd = conn.CreateCommand(cmdType);
                    Console.WriteLine("\t\t{0}", ((FdoCommandType)cmdType).ToString());
                    if (Enum.IsDefined(typeof(FdoCommandType), cmdType))
                    {
                        Assert.NotNull(cmd, "{0}", ((FdoCommandType)cmdType));
                        ValidateCommandType(cmd, cmdType);
                    }
                    else
                    {
                        Console.WriteLine("\tWARNING: Non-standard FDO command {0} encountered", cmdType);
                    }
                }
                catch (ManagedFdoException ex)
                {
                    Assert.Fail("Failed to create command ({0}). Cause: {1}", ((FdoCommandType)cmdType), ex.ToString());
                }
            }
            Console.WriteLine("\tSupportsParameters: {0}", caps.SupportsParameters());
            Console.WriteLine("\tSupportsSelectDistinct: {0}", caps.SupportsSelectDistinct());
            Console.WriteLine("\tSupportsSelectExpressions: {0}", caps.SupportsSelectExpressions());
            Console.WriteLine("\tSupportsSelectFunctions: {0}", caps.SupportsSelectFunctions());
            Console.WriteLine("\tSupportsSelectGrouping: {0}", caps.SupportsSelectGrouping());
            Console.WriteLine("\tSupportsSelectOrdering: {0}", caps.SupportsSelectOrdering());
            Console.WriteLine("\tSupportsTimeout: {0}", caps.SupportsTimeout());
        }

        private static void ProcessTopologyCapabilities(FdoIConnection conn)
        {
            Console.WriteLine("Topology Capabilities");
            var caps = conn.GetTopologyCapabilities();
            Console.WriteLine("\tActivatesTopologyByArea: {0}", caps.ActivatesTopologyByArea());
            Console.WriteLine("\tBreaksCurveCrossingsAutomatically: {0}", caps.BreaksCurveCrossingsAutomatically());
            Console.WriteLine("\tConstrainsFeatureMovements: {0}", caps.ConstrainsFeatureMovements());
            Console.WriteLine("\tSupportsTopologicalHierarchy: {0}", caps.SupportsTopologicalHierarchy());
            Console.WriteLine("\tSupportsTopology: {0}", caps.SupportsTopology());
        }

        private static void ProcessSchemaCapabilities(FdoIConnection conn)
        {
            Console.WriteLine("Schema Capabilities");
            var caps = conn.GetSchemaCapabilities();
            Console.WriteLine("\tMaximumDataValueLength:");
            foreach (FdoDataType dt in Enum.GetValues(typeof(FdoDataType)))
            {
                Console.WriteLine("\t\t{0} - {1}", dt, caps.GetMaximumDataValueLength(dt));
            }
            Console.WriteLine("\tMaximumDecimalPrecision: {0}", caps.GetMaximumDecimalPrecision());
            Console.WriteLine("\tMaximumDecimalScale: {0}", caps.GetMaximumDecimalScale());
            Console.WriteLine("\tNameSizeLimit:");
            foreach (FdoSchemaElementNameType snt in Enum.GetValues(typeof(FdoSchemaElementNameType)))
            {
                Console.WriteLine("\t\t{0} - {1}", snt, caps.GetNameSizeLimit(snt));
            }
            Console.WriteLine("\tReservedCharactersForName: {0}", caps.GetReservedCharactersForName());
            Console.WriteLine("\tSupportedAutogeneratedDataTypes:");
            var dataTypes = caps.SupportedAutogeneratedDataTypes();
            for (int i = 0; i < dataTypes.GetCount(); i++)
            {
                var dt = dataTypes.GetItem(i);
                Console.WriteLine("\t\t{0}", dt);
            }
            var clsTypes = caps.SupportedClassTypes();
            Console.WriteLine("\tSupportedClassTypes:");
            for (int i = 0; i < clsTypes.GetCount(); i++)
            {
                var clsType = clsTypes.GetItem(i);
                Console.WriteLine("\t\t{0}", clsType);
            }
            Console.WriteLine("\tSupportedDataTypes:");
            dataTypes = caps.SupportedDataTypes();
            for (int i = 0; i < dataTypes.GetCount(); i++)
            {
                var dt = dataTypes.GetItem(i);
                Console.WriteLine("\t\t{0}", dt);
            }
            Console.WriteLine("\tSupportedIdentityPropertyTypes:");
            dataTypes = caps.SupportedIdentityPropertyTypes();
            for (int i = 0; i < dataTypes.GetCount(); i++)
            {
                var dt = dataTypes.GetItem(i);
                Console.WriteLine("\t\t{0}", dt);
            }
            Console.WriteLine("\tSupportsAssociationProperties: {0}", caps.SupportsAssociationProperties());
            Console.WriteLine("\tSupportsAutoIdGeneration: {0}", caps.SupportsAutoIdGeneration());
            Console.WriteLine("\tSupportsCompositeId: {0}", caps.SupportsCompositeId());
            Console.WriteLine("\tSupportsCompositeUniqueValueConstraints: {0}", caps.SupportsCompositeUniqueValueConstraints());
            Console.WriteLine("\tSupportsDataStoreScopeUniqueIdGeneration: {0}", caps.SupportsDataStoreScopeUniqueIdGeneration());
            Console.WriteLine("\tSupportsDefaultValue: {0}", caps.SupportsDefaultValue());
            Console.WriteLine("\tSupportsExclusiveValueRangeConstraints: {0}", caps.SupportsExclusiveValueRangeConstraints());
            Console.WriteLine("\tSupportsInclusiveValueRangeConstraints: {0}", caps.SupportsInclusiveValueRangeConstraints());
            Console.WriteLine("\tSupportsInheritance: {0}", caps.SupportsInheritance());
            Console.WriteLine("\tSupportsMultipleSchemas: {0}", caps.SupportsMultipleSchemas());
            Console.WriteLine("\tSupportsNetworkModel: {0}", caps.SupportsNetworkModel());
            Console.WriteLine("\tSupportsNullValueConstraints: {0}", caps.SupportsNullValueConstraints());
            Console.WriteLine("\tSupportsObjectProperties: {0}", caps.SupportsObjectProperties());
            Console.WriteLine("\tSupportsSchemaModification: {0}", caps.SupportsSchemaModification());
            Console.WriteLine("\tSupportsSchemaOverrides: {0}", caps.SupportsSchemaOverrides());
            Console.WriteLine("\tSupportsUniqueValueConstraints: {0}", caps.SupportsUniqueValueConstraints());
            Console.WriteLine("\tSupportsValueConstraintsList: {0}", caps.SupportsValueConstraintsList());
        }

        private static void ProcessRasterCapabilities(FdoIConnection conn)
        {
            var caps = conn.GetRasterCapabilities();
            Console.WriteLine("Raster Capabilities");
            Console.WriteLine("\tSupportsRaster: {0}", caps.SupportsRaster());
            Console.WriteLine("\tSupportsStitching: {0}", caps.SupportsStitching());
            Console.WriteLine("\tSupportsSubsampling: {0}", caps.SupportsSubsampling());
        }

        private static void ProcessGeometryCapabilities(FdoIConnection conn)
        {
            var caps = conn.GetGeometryCapabilities();
            Console.WriteLine("Geometry Capabilities");
            int dims = caps.GetDimensionalities();
            Console.WriteLine("\tDimensionalities:");
            foreach (FdoDimensionality dim in Enum.GetValues(typeof(FdoDimensionality)))
            {
                int d = (int)dim;
                Console.WriteLine("\t\t{0} - {1}", dim, ((dims & d) == d));
            }
            Console.WriteLine("\tSupportedGeometryComponentTypes:");
            var geomCompTypes = caps.SupportedGeometryComponentTypes();
            for (int i = 0; i < geomCompTypes.GetCount(); i++)
            {
                var gct = geomCompTypes.GetItem(i);
                Console.WriteLine("\t\t{0}", gct);
            }
            Console.WriteLine("\tSupportedGeometryTypes:");
            var geomTypes = caps.SupportedGeometryTypes();
            for (int i = 0; i < geomCompTypes.GetCount(); i++)
            {
                var gt = geomCompTypes.GetItem(i);
                Console.WriteLine("\t\t{0}", gt);
            }
        }

        private static void ProcessFilterCapabilities(FdoIConnection conn)
        {
            var caps = conn.GetFilterCapabilities();
            Console.WriteLine("Filter Capabilities");
            Console.WriteLine("\tSupportedConditionTypes:");
            var condTypes = caps.SupportedConditionTypes();
            for (int i = 0; i < condTypes.GetCount(); i++)
            {
                Console.WriteLine("\t\t{0}", condTypes.GetItem(i));
            }
            Console.WriteLine("\tSupportedDistanceOperations:");
            var distOps = caps.SupportedDistanceOperations();
            for (int i = 0; i < distOps.GetCount(); i++)
            {
                Console.WriteLine("\t\t{0}", distOps.GetItem(i));
            }
            Console.WriteLine("\tSupportedSpatialOperations:");
            var spOps = caps.SupportedSpatialOperations();
            for (int i = 0; i < spOps.GetCount(); i++)
            {
                Console.WriteLine("\t\t{0}", spOps.GetItem(i));
            }
            Console.WriteLine("\tSupportsGeodesicDistance: {0}", caps.SupportsGeodesicDistance());
            Console.WriteLine("\tSupportsNonLiteralGeometricOperations: {0}", caps.SupportsNonLiteralGeometricOperations());
        }

        private static void ProcessExpressionCapabilities(FdoIConnection conn)
        {
            var caps = conn.GetExpressionCapabilities();
            Console.WriteLine("Expression Capabilities");
            Console.WriteLine("\tFunctions:");
            var funcs = caps.GetFunctions();
            for (int i = 0; i < funcs.GetCount(); i++)
            {
                var func = funcs.GetItem(i);
                Console.WriteLine("\t\t{0}", func.GetName());
                Console.WriteLine("\t\t\tDescription: {0}", func.GetDescription());
                Console.WriteLine("\t\t\tCategory: {0}", func.GetFunctionCategoryType());
                Console.WriteLine("\t\t\tReturnPropertyType: {0}", func.GetReturnPropertyType());
                Console.WriteLine("\t\t\tReturnType: {0}", func.GetReturnType());
                Console.WriteLine("\t\t\tSupportsVariableArgumentsList: {0}", func.SupportsVariableArgumentsList());
                Console.WriteLine("\t\t\tIsAggregate: {0}", func.IsAggregate());
                var args = func.GetArguments();
                Console.WriteLine("\t\t\tArguments:");
                for (int j = 0; j < args.GetCount(); j++)
                {
                    var arg = args.GetItem(j);
                    Console.WriteLine("\t\t\t\tName: {0}", arg.GetName());
                    Console.WriteLine("\t\t\t\tDescription: {0}", arg.GetDescription());
                    Console.WriteLine("\t\t\t\tDataType: {0}", arg.GetDataType());
                    Console.WriteLine("\t\t\t\tPropertyType: {0}", arg.GetPropertyType());
                    var valueList = arg.GetArgumentValueList();
                    if (valueList != null)
                    {
                        Console.WriteLine("\t\t\t\tArgument Values:");
                        Console.WriteLine("\t\t\t\t\tConstraintType: {0}", valueList.GetConstraintType());
                        Console.WriteLine("\t\t\t\t\tConstraint List:");
                        var list = valueList.GetConstraintList();
                        for (var k = 0; k < list.GetCount(); k++)
                        {
                            var item = list.GetItem(k);
                            Console.WriteLine("\t\t\t\t\t\t{0}", item.ToString());
                        }
                    }
                }
                var sigs = func.GetSignatures();
                Console.WriteLine("\t\t\tSignatures:");
                for (int j = 0; j < sigs.GetCount(); j++)
                {
                    Console.WriteLine("\t\t\t\tSIGNATURE {0}", (j+1));
                    var sig = sigs.GetItem(j);
                    Console.WriteLine("\t\t\t\tReturnType: {0}", sig.GetReturnType());
                    Console.WriteLine("\t\t\t\tReturnPropertyType: {0}", sig.GetReturnPropertyType());
                    var sigArgs = sig.GetArguments();
                    Console.WriteLine("\t\t\t\tArguments:");
                    for (int k = 0; k < sigArgs.GetCount(); k++)
                    {
                        var arg = sigArgs.GetItem(k);
                        Console.WriteLine("\t\t\t\t\tName: {0}", arg.GetName());
                        Console.WriteLine("\t\t\t\t\tDescription: {0}", arg.GetDescription());
                        Console.WriteLine("\t\t\t\t\tDataType: {0}", arg.GetDataType());
                        Console.WriteLine("\t\t\t\t\tPropertyType: {0}", arg.GetPropertyType());
                        var valueList = arg.GetArgumentValueList();
                        if (valueList != null)
                        {
                            Console.WriteLine("\t\t\t\t\tArgument Values:");
                            Console.WriteLine("\t\t\t\t\t\tConstraintType: {0}", valueList.GetConstraintType());
                            Console.WriteLine("\t\t\t\t\t\tConstraint List:");
                            var list = valueList.GetConstraintList();
                            for (var l = 0; l < list.GetCount(); l++)
                            {
                                var item = list.GetItem(l);
                                Console.WriteLine("\t\t\t\t\t\t\t{0}", item.ToString());
                            }
                        }
                    }
                }
            }
            Console.WriteLine("\tSupportedExpressionTypes:");
            var exprTypes = caps.SupportedExpressionTypes();
            for (int i = 0; i < exprTypes.GetCount(); i++)
            {
                Console.WriteLine("\t\t{0}", exprTypes.GetItem(i));
            }
        }

        private static void ProcessConnectionCapabilities(FdoIConnection conn)
        {
            var caps = conn.GetConnectionCapabilities();
            Console.WriteLine("Connection Capabilities");
            int jtypes = caps.GetJoinTypes();
            Console.WriteLine("\tJoinTypes:");
            foreach (FdoJoinType jt in Enum.GetValues(typeof(FdoJoinType)))
            {
                Console.WriteLine("\t\t{0} - {1}", jt, ((jtypes & (int)jt) == (int)jt));
            }
            Console.WriteLine("\tThreadCapability: {0}", caps.GetThreadCapability());
            var ltypes = caps.SupportedLockTypes();
            Console.WriteLine("\tSupportedLockTypes:");
            for (int i = 0; i < ltypes.GetCount(); i++)
            {
                Console.WriteLine("\t\t{0}", ltypes.GetItem(i));
            }
            var sceTypes = caps.SupportedSpatialContextExtentTypes();
            Console.WriteLine("\tSupportedSpatialContextExtentTypes:");
            for (int i = 0; i < sceTypes.GetCount(); i++)
            {
                Console.WriteLine("\t\t{0}", sceTypes.GetItem(i));
            }
            Console.WriteLine("\tSupportsConfiguration: {0}", caps.SupportsConfiguration());
            Console.WriteLine("\tSupportsCSysWKTFromCSysName: {0}", caps.SupportsCSysWKTFromCSysName());
            Console.WriteLine("\tSupportsFlush: {0}", caps.SupportsFlush());
            Console.WriteLine("\tSupportsJoins: {0}", caps.SupportsJoins());
            Console.WriteLine("\tSupportsLocking: {0}", caps.SupportsLocking());
            Console.WriteLine("\tSupportsLongTransactions: {0}", caps.SupportsLongTransactions());
            Console.WriteLine("\tSupportsMultipleSpatialContexts: {0}", caps.SupportsMultipleSpatialContexts());
            Console.WriteLine("\tSupportsMultiUserWrite: {0}", caps.SupportsMultiUserWrite());
            Console.WriteLine("\tSupportsSavePoint: {0}", caps.SupportsSavePoint());
            Console.WriteLine("\tSupportsSQL: {0}", caps.SupportsSQL());
            Console.WriteLine("\tSupportsSubSelects: {0}", caps.SupportsSubSelects());
            Console.WriteLine("\tSupportsTimeout: {0}", caps.SupportsTimeout());
            Console.WriteLine("\tSupportsTransactions: {0}", caps.SupportsTransactions());
            Console.WriteLine("\tSupportsWrite: {0}", caps.SupportsWrite());
        }

        [Test]
        public void TestSDFCapabilities()
        {
            Console.WriteLine("\n====== Testing SDF Capabilities =======\n");
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.SetConnectionString("File=" + TestDataStore.SDF);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            ProcessCommandCapabilities(conn);
            ProcessConnectionCapabilities(conn);
            ProcessExpressionCapabilities(conn);
            ProcessFilterCapabilities(conn);
            ProcessGeometryCapabilities(conn);
            ProcessRasterCapabilities(conn);
            ProcessSchemaCapabilities(conn);
            ProcessTopologyCapabilities(conn);
        }

        [Test]
        public void TestSHPCapabilities()
        {
            Console.WriteLine("\n====== Testing SHP Capabilities =======\n");
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.SetConnectionString("DefaultFileLocation=" + TestDataStore.SHP);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            ProcessCommandCapabilities(conn);
            ProcessConnectionCapabilities(conn);
            ProcessExpressionCapabilities(conn);
            ProcessFilterCapabilities(conn);
            ProcessGeometryCapabilities(conn);
            ProcessRasterCapabilities(conn);
            ProcessSchemaCapabilities(conn);
            ProcessTopologyCapabilities(conn);
        }

        [Test]
        public void TestSQLiteCapabilities()
        {
            Console.WriteLine("\n====== Testing SQLite Capabilities =======\n");
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.SetConnectionString("File=" + TestDataStore.SQLITE);
            Assert.AreEqual(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            ProcessCommandCapabilities(conn);
            ProcessConnectionCapabilities(conn);
            ProcessExpressionCapabilities(conn);
            ProcessFilterCapabilities(conn);
            ProcessGeometryCapabilities(conn);
            ProcessRasterCapabilities(conn);
            ProcessSchemaCapabilities(conn);
            ProcessTopologyCapabilities(conn);
        }
    }
}
