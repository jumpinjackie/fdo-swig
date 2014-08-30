using OSGeo.FDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class CapabilityTests
    {
        static void Log(string format, params object[] args)
        {
            if (false)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(string.Format(format, args));
#else
                System.Diagnostics.Trace.WriteLine(string.Format(format, args));
#endif
            }
        }

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
                        Assert.IsAssignableFrom<FdoIAcquireLock>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_ActivateLongTransaction:
                        Assert.IsAssignableFrom<FdoIActivateLongTransaction>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_ActivateLongTransactionCheckpoint:
                        Assert.IsAssignableFrom<FdoIActivateLongTransactionCheckpoint>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_ActivateSpatialContext:
                        Assert.IsAssignableFrom<FdoIActivateSpatialContext>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_ApplySchema:
                        Assert.IsAssignableFrom<FdoIApplySchema>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_ChangeLongTransactionPrivileges:
                        Assert.IsAssignableFrom<FdoIChangeLongTransactionPrivileges>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_ChangeLongTransactionSet:
                        Assert.IsAssignableFrom<FdoIChangeLongTransactionSet>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_CommitLongTransaction:
                        Assert.IsAssignableFrom<FdoICommitLongTransaction>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_CreateDataStore:
                        Assert.IsAssignableFrom<FdoICreateDataStore>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_CreateLongTransaction:
                        Assert.IsAssignableFrom<FdoICreateLongTransaction>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_CreateLongTransactionCheckpoint:
                        Assert.IsAssignableFrom<FdoICreateLongTransactionCheckpoint>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_CreateMeasureUnit:
                        Assert.IsAssignableFrom<FdoICreateMeasureUnit>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_CreateSpatialContext:
                        Assert.IsAssignableFrom<FdoICreateSpatialContext>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_DeactivateLongTransaction:
                        Assert.IsAssignableFrom<FdoIDeactivateLongTransaction>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_Delete:
                        Assert.IsAssignableFrom<FdoIDelete>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_DescribeSchema:
                        Assert.IsAssignableFrom<FdoIDescribeSchema>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_DescribeSchemaMapping:
                        Assert.IsAssignableFrom<FdoIDescribeSchemaMapping>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_DestroyDataStore:
                        Assert.IsAssignableFrom<FdoIDestroyDataStore>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_DestroyMeasureUnit:
                        Assert.IsAssignableFrom<FdoIDestroyMeasureUnit>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_DestroySchema:
                        Assert.IsAssignableFrom<FdoIDestroySchema>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_DestroySpatialContext:
                        Assert.IsAssignableFrom<FdoIDestroySpatialContext>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_ExtendedSelect:
                        Assert.IsAssignableFrom<FdoIExtendedSelect>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_FreezeLongTransaction:
                        Assert.IsAssignableFrom<FdoIFreezeLongTransaction>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetClassNames:
                        Assert.IsAssignableFrom<FdoIGetClassNames>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetLockedObjects:
                        Assert.IsAssignableFrom<FdoIGetLockedObjects>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetLockInfo:
                        Assert.IsAssignableFrom<FdoIGetLockInfo>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetLockOwners:
                        Assert.IsAssignableFrom<FdoIGetLockOwners>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetLongTransactionCheckpoints:
                        Assert.IsAssignableFrom<FdoIGetLongTransactionCheckpoints>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetLongTransactionPrivileges:
                        Assert.IsAssignableFrom<FdoIGetLongTransactionPrivileges>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetLongTransactions:
                        Assert.IsAssignableFrom<FdoIGetLongTransactions>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetLongTransactionsInSet:
                        Assert.IsAssignableFrom<FdoIGetLongTransactionsInSet>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetMeasureUnits:
                        Assert.IsAssignableFrom<FdoIGetMeasureUnits>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetSchemaNames:
                        Assert.IsAssignableFrom<FdoIGetSchemaNames>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_GetSpatialContexts:
                        Assert.IsAssignableFrom<FdoIGetSpatialContexts>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_Insert:
                        Assert.IsAssignableFrom<FdoIInsert>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_ListDataStores:
                        Assert.IsAssignableFrom<FdoIListDataStores>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_ReleaseLock:
                        Assert.IsAssignableFrom<FdoIReleaseLock>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_RollbackLongTransaction:
                        Assert.IsAssignableFrom<FdoIRollbackLongTransaction>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_RollbackLongTransactionCheckpoint:
                        Assert.IsAssignableFrom<FdoIRollbackLongTransactionCheckpoint>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_Select:
                        Assert.IsAssignableFrom<FdoISelect>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_SelectAggregates:
                        Assert.IsAssignableFrom<FdoISelectAggregates>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_SQLCommand:
                        Assert.IsAssignableFrom<FdoISQLCommand>(cmd);
                        break;
                    case FdoCommandType.FdoCommandType_Update:
                        Assert.IsAssignableFrom<FdoIUpdate>(cmd);
                        break;
                    default:
                        Assert.False(false, string.Format("Command for {0} not exposed to FDO wrapper API", ctype));
                        break;
                }
            }
        }

        private static void ProcessCommandCapabilities(FdoIConnection conn)
        {
            Log("Command Capabilities");
            using (var caps = conn.GetCommandCapabilities())
            {
                var cmds = caps.SupportedCommands();
                Log("\tSupportedCommands:");
                for (int i = 0; i < cmds.GetCount(); i++)
                {
                    int cmdType = cmds.GetItem(i);
                    try
                    {
                        FdoICommand cmd = conn.CreateCommand(cmdType);
                        Log("\t\t{0}", ((FdoCommandType)cmdType).ToString());
                        if (Enum.IsDefined(typeof(FdoCommandType), cmdType))
                        {
                            Assert.NotNull(cmd);
                            ValidateCommandType(cmd, cmdType);
                        }
                        else
                        {
                            Log("\tWARNING: Non-standard FDO command {0} encountered", cmdType);
                        }
                    }
                    catch (ManagedFdoException ex)
                    {
                        Assert.True(false, string.Format("Failed to create command ({0}). Cause: {1}", ((FdoCommandType)cmdType), ex.ToString()));
                    }
                }
                Log("\tSupportsParameters: {0}", caps.SupportsParameters());
                Log("\tSupportsSelectDistinct: {0}", caps.SupportsSelectDistinct());
                Log("\tSupportsSelectExpressions: {0}", caps.SupportsSelectExpressions());
                Log("\tSupportsSelectFunctions: {0}", caps.SupportsSelectFunctions());
                Log("\tSupportsSelectGrouping: {0}", caps.SupportsSelectGrouping());
                Log("\tSupportsSelectOrdering: {0}", caps.SupportsSelectOrdering());
                Log("\tSupportsTimeout: {0}", caps.SupportsTimeout());
            }
        }

        private static void ProcessTopologyCapabilities(FdoIConnection conn)
        {
            Log("Topology Capabilities");
            using (var caps = conn.GetTopologyCapabilities())
            {
                Log("\tActivatesTopologyByArea: {0}", caps.ActivatesTopologyByArea());
                Log("\tBreaksCurveCrossingsAutomatically: {0}", caps.BreaksCurveCrossingsAutomatically());
                Log("\tConstrainsFeatureMovements: {0}", caps.ConstrainsFeatureMovements());
                Log("\tSupportsTopologicalHierarchy: {0}", caps.SupportsTopologicalHierarchy());
                Log("\tSupportsTopology: {0}", caps.SupportsTopology());
            }
        }

        private static void ProcessSchemaCapabilities(FdoIConnection conn)
        {
            Log("Schema Capabilities");
            using (var caps = conn.GetSchemaCapabilities())
            {
                Log("\tMaximumDataValueLength:");
                foreach (FdoDataType dt in Enum.GetValues(typeof(FdoDataType)))
                {
                    Log("\t\t{0} - {1}", dt, caps.GetMaximumDataValueLength(dt));
                }
                Log("\tMaximumDecimalPrecision: {0}", caps.GetMaximumDecimalPrecision());
                Log("\tMaximumDecimalScale: {0}", caps.GetMaximumDecimalScale());
                Log("\tNameSizeLimit:");
                foreach (FdoSchemaElementNameType snt in Enum.GetValues(typeof(FdoSchemaElementNameType)))
                {
                    Log("\t\t{0} - {1}", snt, caps.GetNameSizeLimit(snt));
                }
                Log("\tReservedCharactersForName: {0}", caps.GetReservedCharactersForName());
                Log("\tSupportedAutogeneratedDataTypes:");
                var dataTypes = caps.SupportedAutogeneratedDataTypes();
                for (int i = 0; i < dataTypes.GetCount(); i++)
                {
                    var dt = dataTypes.GetItem(i);
                    Log("\t\t{0}", dt);
                }
                var clsTypes = caps.SupportedClassTypes();
                Log("\tSupportedClassTypes:");
                for (int i = 0; i < clsTypes.GetCount(); i++)
                {
                    var clsType = clsTypes.GetItem(i);
                    Log("\t\t{0}", clsType);
                }
                Log("\tSupportedDataTypes:");
                dataTypes = caps.SupportedDataTypes();
                for (int i = 0; i < dataTypes.GetCount(); i++)
                {
                    var dt = dataTypes.GetItem(i);
                    Log("\t\t{0}", dt);
                }
                Log("\tSupportedIdentityPropertyTypes:");
                dataTypes = caps.SupportedIdentityPropertyTypes();
                for (int i = 0; i < dataTypes.GetCount(); i++)
                {
                    var dt = dataTypes.GetItem(i);
                    Log("\t\t{0}", dt);
                }
                Log("\tSupportsAssociationProperties: {0}", caps.SupportsAssociationProperties());
                Log("\tSupportsAutoIdGeneration: {0}", caps.SupportsAutoIdGeneration());
                Log("\tSupportsCompositeId: {0}", caps.SupportsCompositeId());
                Log("\tSupportsCompositeUniqueValueConstraints: {0}", caps.SupportsCompositeUniqueValueConstraints());
                Log("\tSupportsDataStoreScopeUniqueIdGeneration: {0}", caps.SupportsDataStoreScopeUniqueIdGeneration());
                Log("\tSupportsDefaultValue: {0}", caps.SupportsDefaultValue());
                Log("\tSupportsExclusiveValueRangeConstraints: {0}", caps.SupportsExclusiveValueRangeConstraints());
                Log("\tSupportsInclusiveValueRangeConstraints: {0}", caps.SupportsInclusiveValueRangeConstraints());
                Log("\tSupportsInheritance: {0}", caps.SupportsInheritance());
                Log("\tSupportsMultipleSchemas: {0}", caps.SupportsMultipleSchemas());
                Log("\tSupportsNetworkModel: {0}", caps.SupportsNetworkModel());
                Log("\tSupportsNullValueConstraints: {0}", caps.SupportsNullValueConstraints());
                Log("\tSupportsObjectProperties: {0}", caps.SupportsObjectProperties());
                Log("\tSupportsSchemaModification: {0}", caps.SupportsSchemaModification());
                Log("\tSupportsSchemaOverrides: {0}", caps.SupportsSchemaOverrides());
                Log("\tSupportsUniqueValueConstraints: {0}", caps.SupportsUniqueValueConstraints());
                Log("\tSupportsValueConstraintsList: {0}", caps.SupportsValueConstraintsList());
            }
        }

        private static void ProcessRasterCapabilities(FdoIConnection conn)
        {
            using (var caps = conn.GetRasterCapabilities())
            {
                Log("Raster Capabilities");
                Log("\tSupportsRaster: {0}", caps.SupportsRaster());
                Log("\tSupportsStitching: {0}", caps.SupportsStitching());
                Log("\tSupportsSubsampling: {0}", caps.SupportsSubsampling());
            }
        }

        private static void ProcessGeometryCapabilities(FdoIConnection conn)
        {
            using (var caps = conn.GetGeometryCapabilities())
            {
                Log("Geometry Capabilities");
                int dims = caps.GetDimensionalities();
                Log("\tDimensionalities:");
                foreach (FdoDimensionality dim in Enum.GetValues(typeof(FdoDimensionality)))
                {
                    int d = (int)dim;
                    Log("\t\t{0} - {1}", dim, ((dims & d) == d));
                }
                Log("\tSupportedGeometryComponentTypes:");
                var geomCompTypes = caps.SupportedGeometryComponentTypes();
                for (int i = 0; i < geomCompTypes.GetCount(); i++)
                {
                    var gct = geomCompTypes.GetItem(i);
                    Log("\t\t{0}", gct);
                }
                Log("\tSupportedGeometryTypes:");
                var geomTypes = caps.SupportedGeometryTypes();
                for (int i = 0; i < geomTypes.GetCount(); i++)
                {
                    var gt = geomTypes.GetItem(i);
                    Log("\t\t{0}", gt);
                }
            }
        }

        private static void ProcessFilterCapabilities(FdoIConnection conn)
        {
            using (var caps = conn.GetFilterCapabilities())
            {
                Log("Filter Capabilities");
                Log("\tSupportedConditionTypes:");
                var condTypes = caps.SupportedConditionTypes();
                for (int i = 0; i < condTypes.GetCount(); i++)
                {
                    Log("\t\t{0}", condTypes.GetItem(i));
                }
                Log("\tSupportedDistanceOperations:");
                var distOps = caps.SupportedDistanceOperations();
                for (int i = 0; i < distOps.GetCount(); i++)
                {
                    Log("\t\t{0}", distOps.GetItem(i));
                }
                Log("\tSupportedSpatialOperations:");
                var spOps = caps.SupportedSpatialOperations();
                for (int i = 0; i < spOps.GetCount(); i++)
                {
                    Log("\t\t{0}", spOps.GetItem(i));
                }
                Log("\tSupportsGeodesicDistance: {0}", caps.SupportsGeodesicDistance());
                Log("\tSupportsNonLiteralGeometricOperations: {0}", caps.SupportsNonLiteralGeometricOperations());
            }
        }

        private static void ProcessExpressionCapabilities(FdoIConnection conn)
        {
            using (var caps = conn.GetExpressionCapabilities())
            {
                Log("Expression Capabilities");
                Log("\tFunctions:");
                var funcs = caps.GetFunctions();
                for (int i = 0; i < funcs.GetCount(); i++)
                {
                    var func = funcs.GetItem(i);
                    Log("\t\t{0}", func.Name);
                    Log("\t\t\tDescription: {0}", func.Description);
                    Log("\t\t\tCategory: {0}", func.FunctionCategoryType);
                    Log("\t\t\tReturnPropertyType: {0}", func.ReturnPropertyType);
                    Log("\t\t\tReturnType: {0}", func.ReturnType);
                    Log("\t\t\tSupportsVariableArgumentsList: {0}", func.SupportsVariableArgumentsList());
                    Log("\t\t\tIsAggregate: {0}", func.IsAggregate());
                    var args = func.GetArguments();
                    Log("\t\t\tArguments:");
                    for (int j = 0; j < args.GetCount(); j++)
                    {
                        var arg = args.GetItem(j);
                        Log("\t\t\t\tName: {0}", arg.Name);
                        Log("\t\t\t\tDescription: {0}", arg.Description);
                        Log("\t\t\t\tDataType: {0}", arg.DataType);
                        Log("\t\t\t\tPropertyType: {0}", arg.PropertyType);
                        var valueList = arg.GetArgumentValueList();
                        if (valueList != null)
                        {
                            Log("\t\t\t\tArgument Values:");
                            Log("\t\t\t\t\tConstraintType: {0}", valueList.GetConstraintType());
                            Log("\t\t\t\t\tConstraint List:");
                            var list = valueList.GetConstraintList();
                            for (var k = 0; k < list.GetCount(); k++)
                            {
                                var item = list.GetItem(k);
                                Log("\t\t\t\t\t\t{0}", item.ToString());
                            }
                        }
                    }
                    var sigs = func.GetSignatures();
                    Log("\t\t\tSignatures:");
                    for (int j = 0; j < sigs.GetCount(); j++)
                    {
                        Log("\t\t\t\tSIGNATURE {0}", (j+1));
                        var sig = sigs.GetItem(j);
                        Log("\t\t\t\tReturnType: {0}", sig.ReturnType);
                        Log("\t\t\t\tReturnPropertyType: {0}", sig.ReturnPropertyType);
                        var sigArgs = sig.GetArguments();
                        Log("\t\t\t\tArguments:");
                        for (int k = 0; k < sigArgs.GetCount(); k++)
                        {
                            var arg = sigArgs.GetItem(k);
                            Log("\t\t\t\t\tName: {0}", arg.Name);
                            Log("\t\t\t\t\tDescription: {0}", arg.Description);
                            Log("\t\t\t\t\tDataType: {0}", arg.DataType);
                            Log("\t\t\t\t\tPropertyType: {0}", arg.PropertyType);
                            var valueList = arg.GetArgumentValueList();
                            if (valueList != null)
                            {
                                Log("\t\t\t\t\tArgument Values:");
                                Log("\t\t\t\t\t\tConstraintType: {0}", valueList.GetConstraintType());
                                Log("\t\t\t\t\t\tConstraint List:");
                                var list = valueList.GetConstraintList();
                                for (var l = 0; l < list.GetCount(); l++)
                                {
                                    var item = list.GetItem(l);
                                    Log("\t\t\t\t\t\t\t{0}", item.ToString());
                                }
                            }
                        }
                    }
                }
                Log("\tSupportedExpressionTypes:");
                var exprTypes = caps.SupportedExpressionTypes();
                for (int i = 0; i < exprTypes.GetCount(); i++)
                {
                    Log("\t\t{0}", exprTypes.GetItem(i));
                }
            }
        }

        private static void ProcessConnectionCapabilities(FdoIConnection conn)
        {
            using (var caps = conn.GetConnectionCapabilities())
            {
                Log("Connection Capabilities");
                int jtypes = caps.GetJoinTypes();
                Log("\tJoinTypes:");
                foreach (FdoJoinType jt in Enum.GetValues(typeof(FdoJoinType)))
                {
                    Log("\t\t{0} - {1}", jt, ((jtypes & (int)jt) == (int)jt));
                }
                Log("\tThreadCapability: {0}", caps.GetThreadCapability());
                var ltypes = caps.SupportedLockTypes();
                Log("\tSupportedLockTypes:");
                for (int i = 0; i < ltypes.GetCount(); i++)
                {
                    Log("\t\t{0}", ltypes.GetItem(i));
                }
                var sceTypes = caps.SupportedSpatialContextExtentTypes();
                Log("\tSupportedSpatialContextExtentTypes:");
                for (int i = 0; i < sceTypes.GetCount(); i++)
                {
                    Log("\t\t{0}", sceTypes.GetItem(i));
                }
                Log("\tSupportsConfiguration: {0}", caps.SupportsConfiguration());
                Log("\tSupportsCSysWKTFromCSysName: {0}", caps.SupportsCSysWKTFromCSysName());
                Log("\tSupportsFlush: {0}", caps.SupportsFlush());
                Log("\tSupportsJoins: {0}", caps.SupportsJoins());
                Log("\tSupportsLocking: {0}", caps.SupportsLocking());
                Log("\tSupportsLongTransactions: {0}", caps.SupportsLongTransactions());
                Log("\tSupportsMultipleSpatialContexts: {0}", caps.SupportsMultipleSpatialContexts());
                Log("\tSupportsMultiUserWrite: {0}", caps.SupportsMultiUserWrite());
                Log("\tSupportsSavePoint: {0}", caps.SupportsSavePoint());
                Log("\tSupportsSQL: {0}", caps.SupportsSQL());
                Log("\tSupportsSubSelects: {0}", caps.SupportsSubSelects());
                Log("\tSupportsTimeout: {0}", caps.SupportsTimeout());
                Log("\tSupportsTransactions: {0}", caps.SupportsTransactions());
                Log("\tSupportsWrite: {0}", caps.SupportsWrite());
            }
        }

        [Fact]
        public void TestSDFCapabilities()
        {
            Log("\n====== Testing SDF Capabilities =======\n");
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SDF");
            conn.ConnectionString = "File=" + TestDataStore.SDF;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            ProcessCommandCapabilities(conn);
            ProcessConnectionCapabilities(conn);
            ProcessExpressionCapabilities(conn);
            ProcessFilterCapabilities(conn);
            ProcessGeometryCapabilities(conn);
            ProcessRasterCapabilities(conn);
            ProcessSchemaCapabilities(conn);
            ProcessTopologyCapabilities(conn);
        }

        [Fact]
        public void TestSHPCapabilities()
        {
            Log("\n====== Testing SHP Capabilities =======\n");
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SHP");
            conn.ConnectionString = "DefaultFileLocation=" + TestDataStore.SHP;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
            ProcessCommandCapabilities(conn);
            ProcessConnectionCapabilities(conn);
            ProcessExpressionCapabilities(conn);
            ProcessFilterCapabilities(conn);
            ProcessGeometryCapabilities(conn);
            ProcessRasterCapabilities(conn);
            ProcessSchemaCapabilities(conn);
            ProcessTopologyCapabilities(conn);
        }

        [Fact]
        public void TestSQLiteCapabilities()
        {
            Log("\n====== Testing SQLite Capabilities =======\n");
            IConnectionManager connMgr = FdoFeatureAccessManager.GetConnectionManager();
            FdoIConnection conn = connMgr.CreateConnection("OSGeo.SQLite");
            conn.ConnectionString = "File=" + TestDataStore.SQLITE;
            Assert.Equal(FdoConnectionState.FdoConnectionState_Open, conn.Open());
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
