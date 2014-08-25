// FdoAttributes.i
//
// Attribute (Property) definitions
//
// NOTES:
//  Only non-pointer basic types and enums are wrapped as attributes
//  Pointer types are not wrapped as we are not certain if they will/won't affect %newobject behavior
//  FdoString* types are not wrapped
//  Most boolean methods not wrapped because they don't follow the Get/Set naming convention that others follow

%include <attribute.i>

%attribute(FdoByteArrayHandle, FdoInt32, Length, GetLength);

%attribute(FdoIDisposable, FdoInt32, RefCount, GetRefCount);

%attribute(FdoArgumentDefinition, FdoDataType, DataType, GetDataType);
%attribute(FdoArgumentDefinition, FdoDataType, PropertyType, GetPropertyType);
%attribute(FdoArgumentDefinition, FdoString*, Description, GetDescription);
%attribute(FdoArgumentDefinition, FdoString*, Name, GetName);

//%attribute(FdoCollection, FdoInt32, Count, GetCount);

//%attribute(FdoSchemaElement, FdoBoolean, CanSetName, CanSetName);
%attribute(FdoSchemaElement, FdoSchemaElementState, ElementState, GetElementState);
//%attribute(FdoSchemaElement, FdoSchemaElement*, Parent, GetParent);
%attribute(FdoSchemaElement, FdoString*, Name, GetName, SetName);
%attribute(FdoSchemaElement, FdoString*, Description, GetDescription, SetDescription);

%attribute(FdoPropertyDefinition, FdoBoolean, IsSystem, GetIsSystem, SetIsSystem);
%attribute(FdoPropertyDefinition, FdoPropertyType, PropertyType, GetPropertyType);

%attribute(FdoDataPropertyDefinition, FdoDataType, DataType, GetDataType, SetDataType)
%attribute(FdoDataPropertyDefinition, FdoString*, DefaultValue, GetDefaultValue, SetDefaultValue);
%attribute(FdoDataPropertyDefinition, FdoBoolean, IsAutoGenerated, GetIsAutoGenerated, SetIsAutoGenerated);
%attribute(FdoDataPropertyDefinition, FdoInt32, Length, GetLength, SetLength);
%attribute(FdoDataPropertyDefinition, FdoBoolean, Nullable, GetNullable, SetNullable);
%attribute(FdoDataPropertyDefinition, FdoInt32, Precision, GetPrecision, SetPrecision);
%attribute(FdoDataPropertyDefinition, FdoBoolean, ReadOnly, GetReadOnly, SetReadOnly);
%attribute(FdoDataPropertyDefinition, FdoInt32, Scale, GetScale, SetScale);
%attribute(FdoDataPropertyDefinition, FdoPropertyValueConstraint*, ValueConstraint, GetValueConstraint, SetValueConstraint);

//%attribute(FdoClassDefinition, FdoClassDefinition*, BaseClass, GetBaseClass, SetBaseClass);
//%attribute(FdoClassDefinition, FdoReadOnlyDataPropertyDefinitionCollection*, BaseIdentityProperties, GetBaseIdentityProperties);
//%attribute(FdoClassDefinition, FdoClassCapabilities*, Capabilities, GetCapabilities, SetCapabilities);
%attribute(FdoClassDefinition, FdoClassType, ClassType, GetClassType);
//%attribute(FdoClassDefinition, FdoReadOnlyDataPropertyDefinitionCollection*, IdentityProperties, GetIdentityProperties);
%attribute(FdoClassDefinition, FdoBoolean, IsAbstract, GetIsAbstract, SetIsAbstract);
%attribute(FdoClassDefinition, FdoBoolean, IsComputed, GetIsComputed, SetIsComputed);
//%attribute(FdoClassDefinition, FdoPropertyDefinitionCollection*, Properties, GetProperties);
//%attribute(FdoClassDefinition, FdoUniqueConstraintCollection*, UniqueConstraints, GetUniqueConstraints);

//%attribute(FdoAssociationPropertyDefinition, FdoClassDefinition*, AssociatedClass, GetAssociatedClass, SetAssociatedClass);
%attribute(FdoAssociationPropertyDefinition, FdoDeleteRule, DeleteRule, GetDeleteRule, SetDeleteRule);
//%attribute(FdoAssociationPropertyDefinition, FdoDataPropertyDefinitionCollection*, IdentityProperties, GetIdentityProperties);
%attribute(FdoAssociationPropertyDefinition, FdoBoolean, IsReadOnly, GetIsReadOnly, SetIsReadOnly);
%attribute(FdoAssociationPropertyDefinition, FdoBoolean, LockCascade, GetLockCascade, SetLockCascade);
//%attribute(FdoAssociationPropertyDefinition, FdoDataPropertyDefinitionCollection*, ReverseIdentityProperties, GetReverseIdentityProperties);
%attribute(FdoAssociationPropertyDefinition, FdoString*, Multiplicity, GetMultiplicity, SetMultiplicity);
%attribute(FdoAssociationPropertyDefinition, FdoString*, ReverseMultiplicity, GetReverseMultiplicity, SetReverseMultiplicity);
%attribute(FdoAssociationPropertyDefinition, FdoString*, ReverseName, GetReverseName, SetReverseName);

%attribute(FdoObjectPropertyDefinition, FdoObjectType, ObjectType, GetObjectType, SetObjectType);
%attribute(FdoObjectPropertyDefinition, FdoOrderType, OrderType, GetOrderType, SetOrderType);

%attribute(FdoGeometricPropertyDefinition, FdoInt32, GeometryTypes, GetGeometryTypes, SetGeometryTypes);
%attribute(FdoGeometricPropertyDefinition, FdoBoolean, HasElevation, GetHasElevation, SetHasElevation);
%attribute(FdoGeometricPropertyDefinition, FdoBoolean, HasMeasure, GetHasMeasure, SetHasMeasure);
%attribute(FdoGeometricPropertyDefinition, FdoBoolean, ReadOnly, GetReadOnly, SetReadOnly);
%attribute(FdoGeometricPropertyDefinition, FdoString*, SpatialContextAssociation, GetSpatialContextAssociation, SetSpatialContextAssociation);

%attribute(FdoRasterPropertyDefinition, FdoInt32, DefaultImageXSize, GetDefaultImageXSize, SetDefaultImageXSize);
%attribute(FdoRasterPropertyDefinition, FdoInt32, DefaultImageYSize, GetDefaultImageYSize, SetDefaultImageYSize);
%attribute(FdoRasterPropertyDefinition, FdoBoolean, Nullable, GetNullable, SetNullable);
%attribute(FdoRasterPropertyDefinition, FdoBoolean, ReadOnly, GetReadOnly, SetReadOnly);
%attribute(FdoRasterPropertyDefinition, FdoRasterDataModel, DefaultDataModel, GetDefaultDataModel, SetDefaultDataModel);
%attribute(FdoRasterPropertyDefinition, FdoString*, SpatialContextAssociation, GetSpatialContextAssociation, SetSpatialContextAssociation);

%attribute(FdoExpression, FdoExpressionItemType, ExpressionType, GetExpressionType);

//%attribute(FdoBinaryExpression, FdoExpression*, LeftExpression, GetLeftExpression, SetLeftExpression);
//%attribute(FdoBinaryExpression, FdoExpression*, RightExpression, GetRightExpression, SetRightExpression);
%attribute(FdoBinaryExpression, FdoBinaryOperations, Operation, GetOperation, SetOperation);

%attribute(FdoLiteralValue, FdoLiteralValueType, LiteralValueType, GetLiteralValueType);

%attribute(FdoDataValue, FdoDataType, DataType, GetDataType);
//%attribute(FdoDataValue, FdoBoolean, IsNull, IsNull);

%attribute(FdoBooleanValue, FdoBoolean, Boolean, GetBoolean, SetBoolean);

%attribute(FdoByteValue, FdoByte, Byte, GetByte, SetByte);

%attribute(FdoDecimalValue, double, Decimal, GetDecimal, SetDecimal);

%attribute(FdoDoubleValue, double, Double, GetDouble, SetDouble);

%attribute(FdoInt16Value, FdoInt16, Int16, GetInt16, SetInt16);

%attribute(FdoInt32Value, FdoInt32, Int32, GetInt32, SetInt32);

%attribute(FdoInt64Value, FdoInt64, Int64, GetInt64, SetInt64);

%attribute(FdoSingleValue, float, Single, GetSingle, SetSingle);

%attribute(FdoStringValue, FdoString*, String, GetString, SetString);

//%attribute(FdoBinaryLogicalOperator, FdoFilter*, LeftOperand, GetLeftOperand, SetLeftOperand);
//%attribute(FdoBinaryLogicalOperator, FdoFilter*, RightOperand, GetRightOperand, SetRightOperand);
%attribute(FdoBinaryLogicalOperator, FdoBinaryLogicalOperations, Operation, GetOperation, SetOperation);

%attribute(FdoDistanceCondition, double, Distance, GetDistance, SetDistance);
%attribute(FdoDistanceCondition, FdoDistanceOperations, Operation, GetOperation, SetOperation);
//%attribute(FdoDistanceCondition, FdoExpression*, Geometry, GetGeometry, SetGeometry);

%attribute(FdoFunctionDefinition, FdoFunctionCategoryType, FunctionCategoryType, GetFunctionCategoryType);
%attribute(FdoFunctionDefinition, FdoPropertyType, ReturnPropertyType, GetReturnPropertyType);
%attribute(FdoFunctionDefinition, FdoDataType, ReturnType, GetReturnType);
//%attribute(FdoFunctionDefinition, FdoBoolean, IsAggregate, IsAggregate);
//%attribute(FdoFunctionDefinition, FdoBoolean, SupportsVariableArgumentsList, SupportsVariableArgumentsList);
%attribute(FdoFunctionDefinition, FdoString*, Description, GetDescription);
%attribute(FdoFunctionDefinition, FdoString*, Name, GetName);

%attribute(FdoIConnection, FdoConnectionState, ConnectionState, GetConnectionState);

%attribute(FdoIConnectionInfo, FdoString*, FeatureDataObjectsVersion, GetFeatureDataObjectsVersion);
%attribute(FdoIConnectionInfo, FdoString*, ProviderDescription, GetProviderDescription);
%attribute(FdoIConnectionInfo, FdoString*, ProviderDisplayName, GetProviderDisplayName);
%attribute(FdoIConnectionInfo, FdoString*, ProviderName, GetProviderName);
%attribute(FdoIConnectionInfo, FdoString*, ProviderVersion, GetProviderVersion);
%attribute(FdoIConnectionInfo, FdoProviderDatastoreType, ProviderDatastoreType, GetProviderDatastoreType);

%attribute(FdoIDataReader, FdoInt32, PropertyCount, GetPropertyCount);

%attribute(FdoIDataStoreReader, FdoBoolean, IsFdoEnabled, GetIsFdoEnabled);

%attribute(FdoIDirectPosition, double, X, GetX);
%attribute(FdoIDirectPosition, double, Y, GetY);
%attribute(FdoIDirectPosition, double, Z, GetZ);
%attribute(FdoIDirectPosition, double, M, GetM);
%attribute(FdoIDirectPosition, FdoInt32, Dimensionality, GetDimensionality);

%attribute(FdoIEnvelope, FdoBoolean, IsEmpty, GetIsEmpty);
%attribute(FdoIEnvelope, double, MinX, GetMinX);
%attribute(FdoIEnvelope, double, MaxX, GetMaxX);
%attribute(FdoIEnvelope, double, MinY, GetMinY);
%attribute(FdoIEnvelope, double, MaxY, GetMaxY);
%attribute(FdoIEnvelope, double, MinZ, GetMinZ);
%attribute(FdoIEnvelope, double, MaxZ, GetMaxZ);

%attribute(FdoIGeometry, FdoGeometryType, DerivedType, GetDerivedType);
%attribute(FdoIGeometry, FdoInt32, Dimensionality, GetDimensionality);
%attribute(FdoIGeometry, FdoString*, Text, GetText);

%attribute(FdoICurveAbstract, FdoBoolean, IsClosed, GetIsClosed);

%attribute(FdoICurveString, FdoInt32, Count, GetCount);

%attribute(FdoICurvePolygon, FdoInt32, InteriorRingCount, GetInteriorRingCount);

%attribute(FdoIGeometricAggregateAbstract, FdoInt32, Count, GetCount);

%attribute(FdoIPolygon, FdoInt32, InteriorRingCount, GetInteriorRingCount);

%attribute(FdoILinearRing, FdoInt32, Count, GetCount);

%attribute(FdoILineString, FdoInt32, Count, GetCount);

%attribute(FdoILineStringSegment, FdoInt32, Count, GetCount);

%attribute(FdoIRing, FdoInt32, Count, GetCount);

%attribute(FdoIRingAbstract, FdoInt32, Dimensionality, GetDimensionality);

%attribute(FdoIRaster, FdoInt32, CurrentBand, GetCurrentBand, SetCurrentBand);
%attribute(FdoIRaster, FdoInt32, ImageXSize, GetImageXSize, SetImageXSize);
%attribute(FdoIRaster, FdoInt32, ImageYSize, GetImageYSize, SetImageYSize);
%attribute(FdoIRaster, FdoInt32, NumberOfBands, GetNumberOfBands, SetNumberOfBands);
%attribute(FdoIRaster, FdoString*, VerticalUnits, GetVerticalUnits, SetVerticalUnits);

%attribute(FdoISpatialContextReader, FdoSpatialContextExtentType, ExtentType, GetExtentType);
%attribute(FdoISpatialContextReader, double, XYTolerance, GetXYTolerance);
%attribute(FdoISpatialContextReader, double, ZTolerance, GetZTolerance);
//%attribute(FdoISpatialContextReader, FdoBoolean, IsActive, IsActive);
%attribute(FdoISpatialContextReader, FdoString*, CoordinateSystem, GetCoordinateSystem);
%attribute(FdoISpatialContextReader, FdoString*, CoordinateSystemWkt, GetCoordinateSystemWkt);
%attribute(FdoISpatialContextReader, FdoString*, Description, GetDescription);
%attribute(FdoISpatialContextReader, FdoString*, Name, GetName);

%attribute(FdoJoinCriteria, FdoJoinType, JoinType, GetJoinType, SetJoinType);
//%attribute(FdoJoinCriteria, FdoBoolean, HasAlias, HasAlias);
%attribute(FdoJoinCriteria, FdoString*, Alias, GetAlias, SetAlias);

%attribute(FdoParameterValue, FdoParameterDirection, Direction, GetDirection, SetDirection);

%attribute(FdoProvider, FdoString*, Description, GetDescription);
%attribute(FdoProvider, FdoString*, DisplayName, GetDisplayName);
%attribute(FdoProvider, FdoString*, FeatureDataObjectsVersion, GetFeatureDataObjectsVersion);
%attribute(FdoProvider, FdoString*, LibraryPath, GetLibraryPath);
%attribute(FdoProvider, FdoString*, Name, GetName);
%attribute(FdoProvider, FdoString*, Version, GetVersion);
%attribute(FdoProvider, FdoBoolean, IsManaged, GetIsManaged);

%attribute(FdoSignatureDefinition, FdoPropertyType, ReturnPropertyType, GetReturnPropertyType);
%attribute(FdoSignatureDefinition, FdoDataType, ReturnType, GetReturnType);

%attribute(FdoSpatialCondition, FdoSpatialOperations, Operation, GetOperation, SetOperation);

%attribute(FdoXmlSpatialContextReader, FdoSpatialContextExtentType, ExtentType, GetExtentType);
%attribute(FdoXmlSpatialContextReader, double, XYTolerance, GetXYTolerance);
%attribute(FdoXmlSpatialContextReader, double, ZTolerance, GetZTolerance);
//%attribute(FdoXmlSpatialContextReader, FdoBoolean, IsActive, IsActive);
%attribute(FdoXmlSpatialContextReader, FdoString*, CoordinateSystem, GetCoordinateSystem);
%attribute(FdoXmlSpatialContextReader, FdoString*, CoordinateSystemWkt, GetCoordinateSystemWkt);
%attribute(FdoXmlSpatialContextReader, FdoString*, Description, GetDescription);
%attribute(FdoXmlSpatialContextReader, FdoString*, Name, GetName);

%attribute(FdoXmlSpatialContextWriter, FdoSpatialContextExtentType, ExtentType, GetExtentType, SetExtentType);
%attribute(FdoXmlSpatialContextWriter, double, XYTolerance, GetXYTolerance, SetXYTolerance);
%attribute(FdoXmlSpatialContextWriter, double, ZTolerance, GetZTolerance, SetZTolerance);
%attribute(FdoXmlSpatialContextReader, FdoString*, CoordinateSystem, GetCoordinateSystem, SetCoordinateSystem);
%attribute(FdoXmlSpatialContextReader, FdoString*, CoordinateSystemWkt, GetCoordinateSystemWkt, SetCoordinateSystemWkt);
%attribute(FdoXmlSpatialContextReader, FdoString*, Description, GetDescription, SetDescription);
%attribute(FdoXmlSpatialContextReader, FdoString*, Name, GetName, SetName);

%attribute(FdoIApplySchema, FdoBoolean, IgnoreStates, GetIgnoreStates, SetIgnoreStates);

%attribute(FdoICreateLongTransaction, FdoString*, Description, GetDescription, SetDescription);
%attribute(FdoICreateLongTransaction, FdoString*, Name, GetName, SetName);

%attribute(FdoICreateLongTransactionCheckpoint, FdoString*, CheckpointDescription, GetCheckpointDescription, SetCheckpointDescription);
%attribute(FdoICreateLongTransactionCheckpoint, FdoString*, CheckpointName, GetCheckpointName, SetCheckpointName);
%attribute(FdoICreateLongTransactionCheckpoint, FdoString*, LongTransactionName, GetLongTransactionName, SetLongTransactionName);

%attribute(FdoICreateSpatialContext, FdoString*, CoordinateSystem, GetCoordinateSystem, SetCoordinateSystem);
%attribute(FdoICreateSpatialContext, FdoString*, CoordinateSystemWkt, GetCoordinateSystemWkt, SetCoordinateSystemWkt);
%attribute(FdoICreateSpatialContext, FdoString*, Description, GetDescription, SetDescription);
%attribute(FdoICreateSpatialContext, FdoString*, Name, GetName, SetName);
%attribute(FdoICreateSpatialContext, FdoString*, CoordinateSystem, GetCoordinateSystem, SetCoordinateSystem);

%attribute(FdoIDataStoreReader, FdoString*, Description, GetDescription);
%attribute(FdoIDataStoreReader, FdoString*, Name, GetName);

%attribute(FdoIdentifier, FdoString*, Name, GetName);
%attribute(FdoIdentifier, FdoString*, SchemaName, GetSchemaName);
%attribute(FdoIdentifier, FdoString*, Text, GetText, SetText);

%attribute(FdoIDescribeSchema, FdoString*, SchemaName, GetSchemaName);

%attribute(FdoIDescribeSchemaMapping, FdoBoolean, IncludeDefaults, GetIncludeDefaults, SetIncludeDefaults);
%attribute(FdoIDescribeSchemaMapping, FdoString*, SchemaName, GetSchemaName, SetSchemaName);

%attribute(FdoIDestroySchema, FdoString*, SchemaName, GetSchemaName, SetSchemaName);

%attribute(FdoIDestroySpatialContext, FdoString*, Name, GetName, SetName);

%attribute(FdoIFreezeLongTransaction, FdoString*, Name, GetName, SetName);
%attribute(FdoIFreezeLongTransaction, FdoLongTransactionFreezeOperations, Operation, GetOperation, SetOperation);

%attribute(FdoIGetClassNames, FdoString*, SchemaName, GetSchemaName, SetSchemaName);

%attribute(FdoIGetLongTransactionCheckpoints, FdoString*, LongTransactionName, GetLongTransactionName, SetLongTransactionName);

%attribute(FdoIGetLongTransactionPrivileges, FdoString*, LongTransactionName, GetLongTransactionName, SetLongTransactionName);

%attribute(FdoIGetLongTransactions, FdoString*, Name, GetName, SetName);

%attribute(FdoIGetSpatialContexts, FdoBoolean, ActiveOnly, GetActiveOnly, SetActiveOnly);

%attribute(FdoIListDataStores, FdoBoolean, IncludeNonFdoEnabledDatastores, GetIncludeNonFdoEnabledDatastores, SetIncludeNonFdoEnabledDatastores);

%attribute(FdoILongTransactionCheckpointReader, FdoString*, CheckpointDescription, GetCheckpointDescription);
%attribute(FdoILongTransactionCheckpointReader, FdoString*, CheckpointName, GetCheckpointName);

%attribute(FdoILongTransactionConflictDirectiveEnumerator, FdoString*, FeatureClassName, GetFeatureClassName);
%attribute(FdoILongTransactionConflictDirectiveEnumerator, FdoLongTransactionConflictResolution, Resolution, GetResolution, SetResolution);

%attribute(FdoILongTransactionPrivilegeReader, FdoInt32, Privileges, GetPrivileges);

%attribute(FdoILongTransactionReader, FdoString*, Description, GetDescription);
%attribute(FdoILongTransactionReader, FdoString*, Name, GetName);
%attribute(FdoILongTransactionReader, FdoString*, Owner, GetOwner);

%attribute(FdoILongTransactionSetReader, FdoString*, LongTransactionName, GetLongTransactionName);
%attribute(FdoILongTransactionSetReader, FdoString*, Owner, GetOwner);

%attribute(FdoIoStream, FdoInt64, Index, GetIndex);
%attribute(FdoIoStream, FdoInt64, Length, GetLength);

%attribute(FdoIRollbackLongTransaction, FdoBoolean, KeepLongTransaction, GetKeepLongTransaction, SetKeepLongTransaction);
%attribute(FdoIRollbackLongTransaction, FdoString*, Name, GetName, SetName);

%attribute(FdoIRollbackLongTransactionCheckpoint, FdoString*, CheckpointName, GetCheckpointName, SetCheckpointName);
%attribute(FdoIRollbackLongTransactionCheckpoint, FdoString*, LongTransactionName, GetLongTransactionName, SetLongTransactionName);

%attribute(FdoIBaseSelect, FdoInt32, FetchSize, GetFetchSize, SetFetchSize);
%attribute(FdoIBaseSelect, FdoOrderingOption, OrderingOption, GetOrderingOption, SetOrderingOption);

%attribute(FdoISelect, FdoString*, Alias, GetAlias, SetAlias);
%attribute(FdoISelect, FdoLockStrategy, LockStrategy, GetLockStrategy, SetLockStrategy);
%attribute(FdoISelect, FdoLockType, LockType, GetLockType, SetLockType);

%attribute(FdoISelectAggregates, FdoString*, Alias, GetAlias, SetAlias);
%attribute(FdoISelectAggregates, FdoBoolean, Distinct, GetDistinct, SetDistinct);

%attribute(FdoISQLCommand, FdoInt32, FetchSize, GetFetchSize, SetFetchSize);
%attribute(FdoISQLCommand, FdoString*, SQLStatement, GetSQLStatement, SetSQLStatement);

%attribute(FdoParameter, FdoString*, Name, GetName, SetName);

%attribute(FdoParameterValue, FdoString*, Name, GetName, SetName);

%attribute(FdoProviderNameTokens, FdoString*, LocalName, GetLocalName);

%attribute(FdoRasterDataModel, FdoInt32, BitsPerPixel, GetBitsPerPixel, SetBitsPerPixel);
%attribute(FdoRasterDataModel, FdoRasterDataModelType, DataModelType, GetDataModelType, SetDataModelType);
%attribute(FdoRasterDataModel, FdoRasterDataType, DataType, GetDataType, SetDataType);
%attribute(FdoRasterDataModel, FdoRasterDataOrganization, Organization, GetOrganization, SetOrganization);
%attribute(FdoRasterDataModel, FdoInt32, TileSizeX, GetTileSizeX, SetTileSizeX);
%attribute(FdoRasterDataModel, FdoInt32, TileSizeY, GetTileSizeY, SetTileSizeY);

%attribute(FdoXmlFlags, FdoBoolean, ElementDefaultNullability, GetElementDefaultNullability, SetElementDefaultNullability);
//%attribute(FdoXmlFlags, FdoXmlFlags::ErrorLevel, ErrorLevel, GetErrorLevel, SetErrorLevel);
%attribute(FdoXmlFlags, FdoGmlVersion, GmlVersion, GetGmlVersion, SetGmlVersion);
%attribute(FdoXmlFlags, FdoBoolean, NameAdjust, GetNameAdjust, SetNameAdjust);
%attribute(FdoXmlFlags, FdoBoolean, SchemaNameAsPrefix, GetSchemaNameAsPrefix, SetSchemaNameAsPrefix);
%attribute(FdoXmlFlags, FdoString*, Url, GetUrl, SetUrl);
%attribute(FdoXmlFlags, FdoBoolean, UseGmlId, GetUseGmlId, SetUseGmlId);

%attribute(FdoXmlFeatureFlags, FdoString*, CollectionName, GetCollectionName, SetCollectionName);
%attribute(FdoXmlFeatureFlags, FdoString*, CollectionUri, GetCollectionUri, SetCollectionUri);
//%attribute(FdoXmlFeatureFlags, FdoXmlFeatureFlags::ConflictOption, ConflictOption, GetConflictOption, SetConflictOption);
%attribute(FdoXmlFeatureFlags, FdoString*, DefaultNamespace, GetDefaultNamespace, SetDefaultNamespace);
%attribute(FdoXmlFeatureFlags, FdoString*, DefaultNamespacePrefix, GetDefaultNamespacePrefix, SetDefaultNamespacePrefix);
%attribute(FdoXmlFeatureFlags, FdoString*, GmlDescriptionRelatePropertyName, GetGmlDescriptionRelatePropertyName, SetGmlDescriptionRelatePropertyName);
%attribute(FdoXmlFeatureFlags, FdoString*, GmlIdPrefix, GetGmlIdPrefix, SetGmlIdPrefix);
%attribute(FdoXmlFeatureFlags, FdoString*, GmlNameRelatePropertyName, GetGmlNameRelatePropertyName, SetGmlNameRelatePropertyName);
%attribute(FdoXmlFeatureFlags, FdoString*, MemberName, GetMemberName, SetMemberName);
%attribute(FdoXmlFeatureFlags, FdoString*, MemberUri, GetMemberUri, SetMemberUri);
%attribute(FdoXmlFeatureFlags, FdoString*, SrsName, GetSrsName, SetSrsName);
%attribute(FdoXmlFeatureFlags, FdoBoolean, WriteCollection, GetWriteCollection, SetWriteCollection);
%attribute(FdoXmlFeatureFlags, FdoBoolean, WriteMember, GetWriteMember, SetWriteMember);

//%attribute(FdoXmlSpatialContextFlags, FdoXmlSpatialContextFlags::ConflictOption, ConflictOption, GetConflictOption, SetConflictOption);
%attribute(FdoXmlSpatialContextFlags, FdoBoolean, IncludeDefault, GetIncludeDefault, SetIncludeDefault);

%attribute(FdoXmlWriter, FdoBoolean, DefaultRoot, GetDefaultRoot, SetDefaultRoot);

%attribute(FdoArgumentDefinitionCollection, FdoInt32, Count, GetCount);
%attribute(FdoBatchParameterValueCollection, FdoInt32, Count, GetCount);
%attribute(FdoClassCollection, FdoInt32, Count, GetCount);
%attribute(FdoCurvePolygonCollection, FdoInt32, Count, GetCount);
%attribute(FdoCurveStringCollection, FdoInt32, Count, GetCount);
%attribute(FdoDataPropertyDefinitionCollection, FdoInt32, Count, GetCount);
%attribute(FdoDataValueCollection, FdoInt32, Count, GetCount);
%attribute(FdoDirectPositionCollection, FdoInt32, Count, GetCount);
%attribute(FdoExpressionCollection, FdoInt32, Count, GetCount);
%attribute(FdoFeatureClassCollection, FdoInt32, Count, GetCount);
%attribute(FdoFeatureSchemaCollection, FdoInt32, Count, GetCount);
%attribute(FdoFunctionDefintionCollection, FdoInt32, Count, GetCount);
%attribute(FdoIdentifierCollection, FdoInt32, Count, GetCount);
%attribute(FdoJoinCriteriaCollection, FdoInt32, Count, GetCount);
%attribute(FdoLinearRingCollection, FdoInt32, Count, GetCount);
%attribute(FdoLineStringCollection, FdoInt32, Count, GetCount);
%attribute(FdoParameterValueCollection, FdoInt32, Count, GetCount);
%attribute(FdoPhysicalSchemaMappingCollection, FdoInt32, Count, GetCount);
%attribute(FdoPropertyDefinitionCollection, FdoInt32, Count, GetCount);
%attribute(FdoRingCollection, FdoInt32, Count, GetCount);
%attribute(FdoSignatureDefinitionCollection, FdoInt32, Count, GetCount);

%attribute(FdoReadOnlyArgumentDefinitionCollection, FdoInt32, Count, GetCount);
%attribute(FdoReadOnlyDataPropertyDefinitionCollection, FdoInt32, Count, GetCount);
%attribute(FdoReadOnlyPropertyDefinitionCollection, FdoInt32, Count, GetCount);
%attribute(FdoReadOnlySignatureDefinitionCollection, FdoInt32, Count, GetCount);