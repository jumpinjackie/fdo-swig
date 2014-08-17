// Symbol renaming to avoid collisions with key method names in target languages

//To avoid collision with .net System.Object.GetType
%rename (GetStreamReaderType) FdoIStreamReader::GetType;
%rename (GetStreamReaderType) FdoBLOBStreamReader::GetType;

////////////////////////////////////////////////////////////////
// FDO Common
//
%include <FdoStd.h>
%include <Common/Std.h>
%include <Common/FdoTypes.h>
%include <Common/IDisposable.h>

//We interrupt your normal header parsing to add some custom FDO collections for wrapper convenience
%include "FdoMarshal.i"

%include <Common/Disposable.h>
%include <Common/Exception.h>
%include <Common/Ptr.h>
%include <Common/Array.h>
//%include <Common/ArrayHelper.h>
%include <Common/Compare.h>
%include <Common/StringP.h>
%include <Common/Collection.h>
%template (FdoFeatureSchemaCollectionBase) FdoCollection< FdoFeatureSchema,FdoSchemaException >;
%template (FdoPropertyDefinitionCollectionBase) FdoCollection< FdoPropertyDefinition,FdoSchemaException >;
%template (FdoClassDefinitionCollectionBase) FdoCollection< FdoClassDefinition,FdoSchemaException >;
%template (FdoDataPropertyDefinitionCollectionBase) FdoCollection< FdoDataPropertyDefinition,FdoSchemaException >;
%template (FdoFeatureClassCollectionBase) FdoCollection< FdoFeatureClass,FdoSchemaException >;
%template (FdoDictionaryElementCollectionBase) FdoCollection< FdoDictionaryElement,FdoException >;
%include <Common/NamedCollection.h>
%include <Common/Dimensionality.h>
%include <Common/GeometryType.h>
%include <Common/IStreamReaderTmpl.h>
%template (FdoStringCollectionBase) FdoCollection< FdoStringElement,FdoException >;
%include <Common/StringCollection.h>
%include <Common/Context.h>
%include <Common/DictionaryElement.h>
%template (FdoDictionaryBase) FdoNamedCollection< FdoDictionaryElement,FdoException >;
%include <Common/Dictionary.h>
%include <Common/ReadOnlyNamedCollection.h>
%include <Common/RestrictedNamedCollection.h>
%include <Common/IStreamReader.h>
//%include <Common/Vector.h>
%include <Common/Io/Stream.h>
%include <Common/Io/MemoryStream.h>
%include <Common/Io/FileStream.h>
%include <Common/Io/TextReader.h>
%include <Common/Io/TextWriter.h>
%template (FdoObjectStreamReader) FdoIStreamReaderTmpl< unsigned char >;
%include <Common/Io/ObjectStreamReader.h>
//%include <Common/Io/ByteStreamReader.h>
%template (FdoByteStreamReader) FdoIoObjectStreamReader< FdoByte >;

%include <Common/Xml/Xml.h>
%include <Common/Xml/XmlException.h>
%include <Common/Xml/Attribute.h>
//%include <Common/Xml/AttributeCollection.h>
%include <Common/Xml/SaxContext.h>
%include <Common/Xml/SaxHandler.h>
%include <Common/Xml/CharDataHandler.h>
%include <Common/Xml/SkipElementHandler.h>
%include <Common/Xml/Reader.h>
%include <Common/Xml/Writer.h>
%include <Common/Xml/CopyHandler.h>
%include <Common/Xsl/Transformer.h>
%include <Common/Gml212/Schema.h>
%include <Common/Gml212/Gml212.h>
%include <Common/Gml311/Gml311.h>

////////////////////////////////////////////////////////////////
// FDO Geometry
//
%include <Geometry/IEnvelope.h>
%template (FdoGeometryCollectionBase) FdoCollection< FdoIGeometry,FdoException >;
%include <Geometry/IGeometry.h>
%include <Common/Dimensionality.h>
%include <Common/GeometryType.h>
%include <Geometry/IRingAbstract.h>
%include <Geometry/IGeometricAggregateAbstract.h>
%include <Geometry/ISurfaceAbstract.h>
%include <Geometry/ICurveAbstract.h>
%template (FdoCurveSegmentAbstractCollectionBase) FdoCollection< FdoICurveSegmentAbstract,FdoException >;
%include <Geometry/ICurveSegmentAbstract.h>
%include <Geometry/IArcSegmentAbstract.h>
%template (FdoDirectPositionCollectionBase) FdoCollection< FdoIDirectPosition,FdoException >;
%include <Geometry/IDirectPosition.h>
%include <Geometry/DirectPositionImpl.h>
%template (FdoLineStringCollectionBase) FdoCollection< FdoILineString,FdoException >;
%include <Geometry/ILineString.h>
%template (FdoPointCollectionBase) FdoCollection< FdoIPoint,FdoException >;
%include <Geometry/IPoint.h>
%template (FdoLinearRingCollectionBase) FdoCollection< FdoILinearRing,FdoException >;
%include <Geometry/ILinearRing.h>
%include <Geometry/ILineStringSegment.h>
%template (FdoPolygonCollectionBase) FdoCollection< FdoIPolygon,FdoException >;
%include <Geometry/IPolygon.h>
%include <Geometry/IMultiPoint.h>
%include <Geometry/IMultiGeometry.h>
%include <Geometry/IMultiLineString.h>
%include <Geometry/IMultiPolygon.h>
%include <Geometry/ICircularArcSegment.h>
%template (FdoCurveStringCollectionBase) FdoCollection< FdoICurveString,FdoException >;
%include <Geometry/ICurveString.h>
%include <Geometry/IMultiCurveString.h>
%template (FdoRingCollectionBase) FdoCollection< FdoIRing,FdoException >;
%include <Geometry/IRing.h>
%template (FdoCurvePolygonCollectionBase) FdoCollection< FdoICurvePolygon,FdoException >;
%include <Geometry/ICurvePolygon.h>
%include <Geometry/IMultiCurvePolygon.h>

%include <Geometry/GeometryStream/GeometryStreamFactory.h>
%include <Geometry/GeometryStream/GeometryStreamReader.h>
%include <Geometry/GeometryStream/GeometryStreamWriter.h>

%include <Geometry/Fgf/Factory.h>

//
// FDO Core
//
%include <Fdo/IDisposableCollection.h>
%include <Fdo/ReadOnlyCollection.h>
%include <Fdo/ReadOnlyUnnamedCollection.h>

%include <Fdo/Connections/IPropertyDictionary.h>

////////////////////////////////////////////////////////////////
// Commands
//
%template (FdoBatchParameterValueCollectionBase) FdoCollection<FdoParameterValueCollection, FdoCommandException>;
%include <Fdo/Commands/BatchParameterValueCollection.h>
%include <Fdo/Commands/CommandException.h>
%include <Fdo/Commands/CommandType.h>
%include <Fdo/Commands/ICommand.h>
%template (FdoIdentifierCollectionBase) FdoCollection<FdoIdentifier, FdoCommandException>;
%include <Fdo/Commands/IdentifierCollection.h>
%include <Fdo/Commands/IFeatureCommand.h>
%include <Fdo/Commands/OrderingOption.h>
%include <Fdo/Commands/ParameterDirection.h>
%include <Fdo/Commands/ParameterValue.h>
%template (FdoParameterValueCollectionBase) FdoCollection<FdoParameterValue, FdoCommandException>;
%include <Fdo/Commands/ParameterValueCollection.h>
%include <Fdo/Commands/PropertyValue.h>
%template (FdoPropertyValueCollectionBase) FdoCollection<FdoPropertyValue, FdoCommandException>;
%include <Fdo/Commands/PropertyValueCollection.h>

%include <Fdo/Commands/DataStore/IDataStorePropertyDictionary.h>
%include <Fdo/Commands/DataStore/ICreateDataStore.h>
%include <Fdo/Commands/DataStore/IDestroyDataStore.h>
%include <Fdo/Commands/DataStore/IDataStoreReader.h>
%include <Fdo/Commands/DataStore/IListDataStores.h>

%include <Fdo/Commands/Feature/IReader.h>
%include <Fdo/Commands/Feature/IBaseSelect.h>
%include <Fdo/Commands/Feature/IDataReader.h>
%include <Fdo/Commands/Feature/IDelete.h>
%include <Fdo/Commands/Feature/IFeatureReader.h>
%include <Fdo/Commands/Feature/IScrollableFeatureReader.h>
%include <Fdo/Commands/Feature/IInsert.h>
%include <Fdo/Commands/Feature/ISelect.h>
%include <Fdo/Commands/Feature/IExtendedSelect.h>
%include <Fdo/Commands/Feature/ISelectAggregates.h>
%include <Fdo/Commands/Feature/IUpdate.h>

%include <Fdo/Commands/Locking/LockType.h>
%include <Fdo/Commands/Locking/IAcquireLock.h>
%include <Fdo/Commands/Locking/IGetLockedObjects.h>
%include <Fdo/Commands/Locking/IGetLockInfo.h>
%include <Fdo/Commands/Locking/IGetLockOwners.h>
%include <Fdo/Commands/Locking/ILockConflictReader.h>
%include <Fdo/Commands/Locking/ILockedObjectReader.h>
%include <Fdo/Commands/Locking/ILockOwnersReader.h>
%include <Fdo/Commands/Locking/IReleaseLock.h>
%include <Fdo/Commands/Locking/LockStrategy.h>
%include <Fdo/Commands/Locking/ConflictType.h>

%include <Fdo/Commands/LongTransaction/IActivateLongTransaction.h>
%include <Fdo/Commands/LongTransaction/IActivateLongTransactionCheckpoint.h>
%include <Fdo/Commands/LongTransaction/IChangeLongTransactionPrivileges.h>
%include <Fdo/Commands/LongTransaction/IChangeLongTransactionSet.h>
%include <Fdo/Commands/LongTransaction/ICommitLongTransaction.h>
%include <Fdo/Commands/LongTransaction/ICreateLongTransaction.h>
%include <Fdo/Commands/LongTransaction/ICreateLongTransactionCheckpoint.h>
%include <Fdo/Commands/LongTransaction/IDeactivateLongTransaction.h>
%include <Fdo/Commands/LongTransaction/IFreezeLongTransaction.h>
%include <Fdo/Commands/LongTransaction/IGetLongTransactionCheckpoints.h>
%include <Fdo/Commands/LongTransaction/IGetLongTransactionPrivileges.h>
%include <Fdo/Commands/LongTransaction/IGetLongTransactions.h>
%include <Fdo/Commands/LongTransaction/IGetLongTransactionsInSet.h>
%include <Fdo/Commands/LongTransaction/ILongTransactionCheckpointReader.h>
%include <Fdo/Commands/LongTransaction/ILTConflictDirectiveEnumerator.h>
%include <Fdo/Commands/LongTransaction/ILongTransactionPrivilegeReader.h>
%include <Fdo/Commands/LongTransaction/ILongTransactionReader.h>
%include <Fdo/Commands/LongTransaction/ILongTransactionSetReader.h>
%include <Fdo/Commands/LongTransaction/IRollbackLongTransaction.h>
%include <Fdo/Commands/LongTransaction/IRollbackLongTransactionCheckpoint.h>
%include <Fdo/Commands/LongTransaction/LongTransactionConflictResolution.h>
%include <Fdo/Commands/LongTransaction/LongTransactionConstants.h>
%include <Fdo/Commands/LongTransaction/LongTransactionFreezeOperations.h>
%include <Fdo/Commands/LongTransaction/LongTransactionPrivilegeOperations.h>
%include <Fdo/Commands/LongTransaction/LongTransactionPrivileges.h>
%include <Fdo/Commands/LongTransaction/LongTransactionSetOperations.h>

%include <Fdo/Commands/Schema/IApplySchema.h>
%include <Fdo/Commands/Schema/IDescribeSchema.h>
%include <Fdo/Commands/Schema/IDescribeSchemaMapping.h>
%include <Fdo/Commands/Schema/IDestroySchema.h>
%include <Fdo/Commands/Schema/IGetClassNames.h>
%include <Fdo/Commands/Schema/IGetSchemaNames.h>
%include <Fdo/Commands/Schema/PhysicalElementMapping.h>
%include <Fdo/Commands/Schema/PhysicalClassMapping.h>
%include <Fdo/Commands/Schema/PhysicalElementMappingCollection.h>
%include <Fdo/Commands/Schema/PhysicalPropertyMapping.h>
%include <Fdo/Commands/Schema/PhysicalSchemaMapping.h>
%template (FdoPhysicalSchemaMappingCollectionBase) FdoCollection<FdoPhysicalSchemaMapping, FdoCommandException>;
%include <Fdo/Commands/Schema/PhysicalSchemaMappingCollection.h>

%include <Fdo/Commands/SpatialContext/SpatialContextExtentType.h>
%include <Fdo/Commands/SpatialContext/SpatialContextMismatchException.h>
%include <Fdo/Commands/SpatialContext/IActivateSpatialContext.h>
%include <Fdo/Commands/SpatialContext/ICreateSpatialContext.h>
%include <Fdo/Commands/SpatialContext/IDestroySpatialContext.h>
%include <Fdo/Commands/SpatialContext/IGetSpatialContexts.h>
%include <Fdo/Commands/SpatialContext/ISpatialContextReader.h>

%include <Fdo/Commands/Sql/ISQLCommand.h>
%include <Fdo/Commands/Sql/ISQLDataReader.h>

%include <Fdo/Commands/UnitOfMeasure/BaseUnit.h>
%include <Fdo/Commands/UnitOfMeasure/ICreateMeasureUnit.h>
%include <Fdo/Commands/UnitOfMeasure/IDestroyMeasureUnit.h>
%include <Fdo/Commands/UnitOfMeasure/IGetMeasureUnits.h>
%include <Fdo/Commands/UnitOfMeasure/IMeasureUnitReader.h>

////////////////////////////////////////////////////////////////
// Expression
//
%include <Fdo/Expression/JoinType.h>
%include <Fdo/Expression/Expression.h>
%include <Fdo/Expression/ValueExpression.h>
%include <Fdo/Expression/LiteralValue.h>
%include <Fdo/Expression/DataValue.h>
%include <Fdo/Expression/Identifier.h>
%include <Fdo/Expression/BinaryExpression.h>
%include <Fdo/Expression/BinaryOperations.h>
%include <Fdo/Expression/BooleanValue.h>
%include <Fdo/Expression/ByteValue.h>
%include <Fdo/Expression/LOBValue.h>
%include <Fdo/Expression/BLOBValue.h>
%include <Fdo/Expression/CLOBValue.h>
%include <Fdo/Expression/BLOBStreamReader.h>
%include <Fdo/Expression/ComputedIdentifier.h>
%template (FdoDataValueCollectionBase) FdoCollection<FdoDataValue, FdoExpressionException>;
%include <Fdo/Expression/DataValueCollection.h>
%include <Fdo/Expression/DateTimeValue.h>
%include <Fdo/Expression/DecimalValue.h>
%include <Fdo/Expression/DoubleValue.h>
%template (FdoExpressionCollectionBase) FdoCollection<FdoExpression, FdoExpressionException>;
%include <Fdo/Expression/ExpressionCollection.h>
%include <Fdo/Expression/ExpressionException.h>
%include <Fdo/Expression/ExpressionType.h>
%include <Fdo/Expression/Function.h>
%include <Fdo/Expression/GeometryValue.h>
%include <Fdo/Expression/Int16Value.h>
%include <Fdo/Expression/Int32Value.h>
%include <Fdo/Expression/Int64Value.h>
%include <Fdo/Expression/IExpressionProcessor.h>
//%include <Fdo/Expression/LiteralValueCollection.h>
%include <Fdo/Expression/LiteralValueType.h>
%include <Fdo/Expression/Parameter.h>
%include <Fdo/Expression/SingleValue.h>
%include <Fdo/Expression/StringValue.h>
%include <Fdo/Expression/UnaryExpression.h>
%include <Fdo/Expression/UnaryOperations.h>
%include <Fdo/Expression/ExpressionItemType.h>
%include <Fdo/Expression/JoinCriteria.h>
%template (FdoJoinCriteriaCollectionBase) FdoCollection<FdoJoinCriteria, FdoCommandException>;
%include <Fdo/Expression/JoinCriteriaCollection.h>
%include <Fdo/Expression/SubSelectExpression.h>

////////////////////////////////////////////////////////////////
// Filter
//
%include <Fdo/Filter/Filter.h>
%include <Fdo/Filter/LogicalOperator.h>
%include <Fdo/Filter/SearchCondition.h>
%include <Fdo/Filter/GeometricCondition.h>
%include <Fdo/Filter/BinaryLogicalOperations.h>
%include <Fdo/Filter/BinaryLogicalOperator.h>
%include <Fdo/Filter/ComparisonCondition.h>
%include <Fdo/Filter/ComparisonOperations.h>
%include <Fdo/Filter/ConditionType.h>
%include <Fdo/Filter/DistanceCondition.h>
%include <Fdo/Filter/DistanceOperations.h>
%include <Fdo/Filter/FilterException.h>
%include <Fdo/Filter/IFilterProcessor.h>
%include <Fdo/Filter/InCondition.h>
%include <Fdo/Filter/NullCondition.h>
%include <Fdo/Filter/SpatialCondition.h>
%include <Fdo/Filter/SpatialOperations.h>
%include <Fdo/Filter/UnaryLogicalOperations.h>
%include <Fdo/Filter/UnaryLogicalOperator.h>
%template (FdoValueExpressionCollectionBase) FdoCollection<FdoValueExpression, FdoFilterException>;
%include <Fdo/Filter/ValueExpressionCollection.h>

////////////////////////////////////////////////////////////////
// Schema
//
%include <Fdo/Schema/SchemaElement.h>
%include <Fdo/Schema/SchemaException.h>
%include <Fdo/Schema/PropertyDefinition.h>
%include <Fdo/Schema/ClassDefinition.h>
%template (FdoPropertyDefinitionNamedCollectionBase) FdoNamedCollection< FdoPropertyDefinition,FdoSchemaException >;
%template (FdoClassDefinitionNamedCollectionBase) FdoNamedCollection< FdoClassDefinition,FdoSchemaException >;
%template (FdoDataPropertyDefinitionNamedCollectionBase) FdoNamedCollection< FdoDataPropertyDefinition,FdoSchemaException >;
%template (FdoFeatureClassNamedCollectionBase) FdoNamedCollection< FdoFeatureClass,FdoSchemaException >;
%include <Fdo/Schema/SchemaCollection.h>
%template (FdoPropertyDefinitionSchemaCollectionBase) FdoSchemaCollection< FdoPropertyDefinition >;
%include <Fdo/Schema/PropertyDefinitionCollection.h>
%include <Fdo/Schema/AssociationPropertyDefinition.h>
%include <Fdo/Schema/AutogenerationException.h>
%include <Fdo/Schema/Class.h>
%include <Fdo/Schema/ClassCapabilities.h>
%template (FdoClassSchemaCollectionBase) FdoSchemaCollection<FdoClassDefinition>;
%include <Fdo/Schema/ClassCollection.h>
%include <Fdo/Schema/ClassType.h>
%include <Fdo/Schema/DataPropertyDefinition.h>
%template (FdoDataPropertyDefinitionSchemaCollectionBase) FdoSchemaCollection<FdoDataPropertyDefinition>;
%include <Fdo/Schema/DataPropertyDefinitionCollection.h>
%include <Fdo/Schema/DataType.h>
%include <Fdo/Schema/DeleteRule.h>
%include <Fdo/Schema/FeatureClass.h>
%template (FdoFeatureClassSchemaCollectionBase) FdoSchemaCollection<FdoFeatureClass>;
%include <Fdo/Schema/FeatureClassCollection.h>
%include <Fdo/Schema/FeatureSchema.h>
%template (FdoFeatureSchemaNamedCollectionBase) FdoNamedCollection< FdoFeatureSchema,FdoSchemaException >;
%template (FdoFeatureSchemaSchemaCollectionBase) FdoSchemaCollection< FdoFeatureSchema >;
%include <Fdo/Schema/FeatureSchemaCollection.h>
%include <Fdo/Schema/GeometricPropertyDefinition.h>
%include <Fdo/Schema/GeometricType.h>
%include <Fdo/Schema/MergeContext.h>
%include <Fdo/Schema/ObjectPropertyDefinition.h>
%include <Fdo/Schema/ObjectType.h>
%include <Fdo/Schema/OrderType.h>
%include <Fdo/Schema/PropertyType.h>
%include <Fdo/Schema/PropertyValueConstraint.h>
%include <Fdo/Schema/PropertyValueConstraintRange.h>
%include <Fdo/Schema/PropertyValueConstraintType.h>
%include <Fdo/Schema/PropertyValueConstraintList.h>
%template (FdoReadOnlyPropertyDefinitionCollectionBase) FdoReadOnlyCollection<FdoPropertyDefinition, FdoIDisposableCollection, FdoSchemaException>;
%include <Fdo/Schema/ReadOnlyPropertyDefinitionCollection.h>
%include <Fdo/Schema/SchemaAttributeDictionary.h>
%include <Fdo/Schema/SchemaElementState.h>
%include <Fdo/Schema/RasterPropertyDefinition.h>
%template (FdoReadOnlyDataPropertyDefinitionCollectionBase) FdoReadOnlyCollection<FdoDataPropertyDefinition, FdoIDisposableCollection, FdoSchemaException>;
%include <Fdo/Schema/ReadOnlyDataPropertyDefinitionCollection.h>
//%include <Fdo/Schema/NetworkFeatureClass.h>
//%include <Fdo/Schema/NetworkLayerClass.h>
//%include <Fdo/Schema/NetworkNodeFeatureClass.h>
//%include <Fdo/Schema/NetworkLinkFeatureClass.h>
//%include <Fdo/Schema/NetworkClass.h>
//%include <Fdo/Schema/TopoFeaturePropertyDefinition.h>
//%include <Fdo/Schema/TopoGeometryPropertyDefinition.h>
//%include <Fdo/Schema/Topology.h>
%include <Fdo/Schema/UniqueConstraint.h>
%template (FdoUniqueConstraintCollectionBase) FdoCollection<FdoUniqueConstraint, FdoSchemaException>;
%include <Fdo/Schema/UniqueConstraintCollection.h>
%include <Fdo/Schema/PolygonVertexOrderRule.h>
%include <Fdo/Schema/PropertyValueConstraint.h>
%include <Fdo/Schema/PropertyValueConstraintList.h>
%include <Fdo/Schema/PropertyValueConstraintRange.h>
%include <Fdo/Schema/PropertyValueConstraintList.h>


////////////////////////////////////////////////////////////////
// Raster
//
%include <Fdo/Raster/IRaster.h>
%include <Fdo/Raster/IRasterPropertyDictionary.h>
%include <Fdo/Raster/RasterDataOrganization.h>
%include <Fdo/Raster/RasterDataModelType.h>
%include <Fdo/Raster/RasterDataModel.h>
%include <Fdo/Raster/RasterDataType.h>

////////////////////////////////////////////////////////////////
// Connections
//
%include <Fdo/Connections/ConnectionException.h>
%include <Fdo/Connections/ConnectionState.h>
%include <Fdo/Connections/IConnection.h>
%include <Fdo/Connections/IConnectionInfo.h>
%include <Fdo/Connections/IConnectionPropertyDictionary.h>
%include <Fdo/Connections/ITransaction.h>

%include <Fdo/Connections/ProviderDatastoreType.h>
%include <Fdo/Connections/Capabilities/FunctionCategoryType.h>
%include <Fdo/Connections/Capabilities/SchemaElementNameType.h>
%include <Fdo/Connections/Capabilities/ArgumentDefinition.h>
%template (FdoArgumentDefinitionCollectionBase) FdoCollection<FdoArgumentDefinition, FdoConnectionException>;
%include <Fdo/Connections/Capabilities/ArgumentDefinitionCollection.h>
%include <Fdo/Connections/Capabilities/FunctionDefinition.h>
%template (FdoFunctionDefinitionCollectionBase) FdoNamedCollection<FdoFunctionDefinition, FdoConnectionException>;

//HACK/BOGUS: SWIG couldn't find GetCount() definition, so define it here
%extend FdoFunctionDefinitionCollection
{
    FdoInt32 GetCount()
    {
        return $self->GetCount();
    }
};

%include <Fdo/Connections/Capabilities/FunctionDefinitionCollection.h>
%include <Fdo/Connections/Capabilities/ThreadCapability.h>
%include <Fdo/Connections/Capabilities/ICommandCapabilities.h>
%include <Fdo/Connections/Capabilities/IConnectionCapabilities.h>
%include <Fdo/Connections/Capabilities/IExpressionCapabilities.h>
%include <Fdo/Connections/Capabilities/IFilterCapabilities.h>
%include <Fdo/Connections/Capabilities/IGeometryCapabilities.h>
%include <Fdo/Connections/Capabilities/ISchemaCapabilities.h>
%include <Fdo/Connections/Capabilities/IRasterCapabilities.h>
%include <Fdo/Connections/Capabilities/ITopologyCapabilities.h>
%template (FdoReadOnlyArgumentDefinitionCollectionBase) FdoReadOnlyCollection<FdoArgumentDefinition, FdoArgumentDefinitionCollection, FdoConnectionException>;
%include <Fdo/Connections/Capabilities/ReadOnlyArgumentDefinitionCollection.h>
%template (FdoSignatureDefinitionCollectionBase) FdoCollection< FdoSignatureDefinition,FdoConnectionException >;
%template (FdoReadOnlySignatureDefinitionCollectionBase) FdoReadOnlyUnnamedCollection< FdoSignatureDefinition,FdoSignatureDefinitionCollection,FdoSchemaException >;
%include <Fdo/Connections/Capabilities/SignatureDefinition.h>

////////////////////////////////////////////////////////////////
// Client Services
//
%include <Fdo/IConnectionManager.h>
%include <Fdo/IProviderRegistry.h>
%include <Fdo/ClientServices/ClientServiceException.h>
%include <Fdo/ClientServices/ConnectionManager.h>
%include <Fdo/ClientServices/FeatureAccessManager.h>
%include <Fdo/ClientServices/Provider.h>
%include <Fdo/ClientServices/ProviderNameTokens.h>
%include <Fdo/ClientServices/ProviderRegistry.h>
%include <Fdo/ClientServices/ClientServices.h>
%include <Fdo/ClientServices/ProviderCollection.h>

////////////////////////////////////////////////////////////////
// XML Services
//
/*
%include <Fdo/Xml/ClassMapping.h>
//%include <Fdo/Xml/ClassMappingCollection.h>
%include <Fdo/Xml/Context.h>
%include <Fdo/Xml/Deserializable.h>
%include <Fdo/Xml/ElementMapping.h>
//%include <Fdo/Xml/ElementMappingCollection.h>
%include <Fdo/Xml/FeatureContext.h>
%include <Fdo/Xml/FeatureFlags.h>
%include <Fdo/Xml/FeatureHandler.h>
%include <Fdo/Xml/FeaturePropertyReader.h>
%include <Fdo/Xml/FeaturePropertyWriter.h>
%include <Fdo/Xml/FeatureReader.h>
%include <Fdo/Xml/FeatureSerializer.h>
%include <Fdo/Xml/FeatureWriter.h>
%include <Fdo/Xml/Flags.h>
%include <Fdo/Xml/NameCollectionHandler.h>
%include <Fdo/Xml/SchemaMapping.h>
%include <Fdo/Xml/Serializable.h>
%include <Fdo/Xml/SpatialContextFlags.h>
%include <Fdo/Xml/SpatialContextReader.h>
%include <Fdo/Xml/SpatialContextSerializer.h>
%include <Fdo/Xml/SpatialContextWriter.h>
%include <Fdo/Xml/GmlVersion.h>
*/