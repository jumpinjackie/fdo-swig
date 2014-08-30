////////////////////////////////////////////////////////////////
//
// Ignore directives
//
// These FDO constructs will be ignored by the FDO wrapper.
//
////////////////////////////////////////////////////////////////

//FDO Functions
%ignore FDO_FUNCTION_ABS;
%ignore FDO_FUNCTION_ACOS;
%ignore FDO_FUNCTION_ADDMONTHS;
%ignore FDO_FUNCTION_AREA2D;
%ignore FDO_FUNCTION_ASIN;
%ignore FDO_FUNCTION_ATAN;
%ignore FDO_FUNCTION_ATAN2;
%ignore FDO_FUNCTION_AVG;
%ignore FDO_FUNCTION_CEIL;
%ignore FDO_FUNCTION_CONCAT;
%ignore FDO_FUNCTION_COS;
%ignore FDO_FUNCTION_COUNT;
%ignore FDO_FUNCTION_CURRENTDATE;
%ignore FDO_FUNCTION_EXP;
%ignore FDO_FUNCTION_EXTRACT;
%ignore FDO_FUNCTION_EXTRACTTODOUBLE;
%ignore FDO_FUNCTION_EXTRACTTOINT;
%ignore FDO_FUNCTION_FLOOR;
%ignore FDO_FUNCTION_INSTR;
%ignore FDO_FUNCTION_LENGTH;
%ignore FDO_FUNCTION_LENGTH2D;
%ignore FDO_FUNCTION_LN;
%ignore FDO_FUNCTION_LOG;
%ignore FDO_FUNCTION_LOWER;
%ignore FDO_FUNCTION_LPAD;
%ignore FDO_FUNCTION_LTRIM;
%ignore FDO_FUNCTION_M;
%ignore FDO_FUNCTION_MAX;
%ignore FDO_FUNCTION_MEDIAN;
%ignore FDO_FUNCTION_MIN;
%ignore FDO_FUNCTION_MOD;
%ignore FDO_FUNCTION_MONTHSBETWEEN;
%ignore FDO_FUNCTION_NULLVALUE;
%ignore FDO_FUNCTION_POWER;
%ignore FDO_FUNCTION_REMAINDER;
%ignore FDO_FUNCTION_RPAD;
%ignore FDO_FUNCTION_ROUND;
%ignore FDO_FUNCTION_RTRIM;
%ignore FDO_FUNCTION_SIGN;
%ignore FDO_FUNCTION_SIN;
%ignore FDO_FUNCTION_SOUNDEX;
%ignore FDO_FUNCTION_SPATIALEXTENTS;
%ignore FDO_FUNCTION_SQRT;
%ignore FDO_FUNCTION_STDDEV;
%ignore FDO_FUNCTION_SUBSTR;
%ignore FDO_FUNCTION_SUM;
%ignore FDO_FUNCTION_TAN;
%ignore FDO_FUNCTION_TODATE;
%ignore FDO_FUNCTION_TODOUBLE;
%ignore FDO_FUNCTION_TOFLOAT;
%ignore FDO_FUNCTION_TOINT32;
%ignore FDO_FUNCTION_TOINT64;
%ignore FDO_FUNCTION_TOSTRING;
%ignore FDO_FUNCTION_TRANSLATE;
%ignore FDO_FUNCTION_TRIM;
%ignore FDO_FUNCTION_TRUNC;
%ignore FDO_FUNCTION_UPPER;
%ignore FDO_FUNCTION_X;
%ignore FDO_FUNCTION_Y;
%ignore FDO_FUNCTION_Z;

%ignore FDO_ACTIVELONGTRANSACTION;
%ignore FDO_ROOTLONGTRANSACTION;
%ignore FDO_STRING_COLLECTION;
%ignore FDO_STRINGP_H;
%ignore FDO_VECTOR_H;
%ignore FDO_STRING_COLLECTION_H;
%ignore MAX_GEOMETRY_TYPE_SIZE;
%ignore MAX_GEOMETRIC_TYPE_SIZE;
%ignore FDO_NAMED_COLLECTION_H;
%ignore FDO_COLL_MAP_THRESHOLD;
%ignore FdoInt64Max;
%ignore FdoInt64Min;
%ignore FdoFloatMax;
%ignore FdoFloatMin;

// C++ refcounting facets should not be exposed
%ignore FdoIDisposable::AddRef;
%ignore FdoIDisposable::Release;
%ignore FdoIDisposable::Dispose;
%ignore FdoIDisposable::EnableObjectThreadLocking;

%ignore FdoGml212;
%ignore FdoGml212Schema;
%ignore FdoGml311;
%ignore FdoXml;

%ignore FdoExpression::ToStringInternal;
%ignore FdoFilter::ToStringInternal;

%ignore FdoXmlSpatialContextReader::XmlEndElement;
%ignore FdoXmlSpatialContextReader::XmlStartDocument;

%ignore FdoBLOBStreamReader;

%ignore FdoContext;
%ignore FdoStringP;
%ignore FdoStringsP;
%ignore FdoProviderNameTokensP;
%ignore FdoArrayHelper;
%ignore FdoSchemaMergeContext;
%ignore FdoSchemaAttributeDictionary::XmlStartElement;
%ignore FdoSchemaAttributeDictionary::XmlEndElement;
%ignore FdoDirectPositionImpl;
%ignore FdoXmlFeatureFlags::Create;
%ignore FdoSubSelectExpression::ToStringInternal;
%ignore FdoGeometryStreamFactory::CreateGeometryStreamReader;

%ignore FdoFunction::Create(FdoString*, FdoExpression**, FdoInt32);
%ignore FdoArgumentDefinitionCollection::Create(FdoArgumentDefinition**, FdoInt32);
%ignore FdoSignatureDefinitionCollection::Create(FdoSignatureDefinition**, FdoInt32);
%ignore FdoFunctionDefinitionCollection::Create(FdoFunctionDefinition**, FdoInt32);
%ignore FdoIoFileStream::Create(FILE*);

%ignore FdoStringElement::operator =;
%ignore FdoStringCollection::operator +;
%ignore FdoStringCollection::operator +=;

%ignore FdoStringCollection::Create(FdoStringCollection const *);
%ignore FdoStringCollection::Create(FdoStringCollection const &);

%ignore FdoAssociationPropertyDefinition::CheckReferences;
%ignore FdoAssociationPropertyDefinition::InitFromXml;
%ignore FdoAssociationPropertyDefinition::Set;
%ignore FdoAssociationPropertyDefinition::_writeXml;
%ignore FdoDataPropertyDefinition::InitFromXml;
%ignore FdoDataPropertyDefinition::Set;
%ignore FdoDataPropertyDefinition::_writeXml;
%ignore FdoGeometricPropertyPropertyDefinition::InitFromXml;
%ignore FdoGeometricPropertyPropertyDefinition::Set;
%ignore FdoGeometricPropertyPropertyDefinition::_writeXml;
%ignore FdoObjectPropertyDefinition::InitFromXml;
%ignore FdoObjectPropertyDefinition::Set;
%ignore FdoObjectPropertyDefinition::_writeXml;
%ignore FdoRasterPropertyDefinition::InitFromXml;
%ignore FdoRasterPropertyDefinition::Set;
%ignore FdoRasterPropertyDefinition::_writeXml;
%ignore FdoClassDefinition::CheckReferences;
%ignore FdoClassDefinition::InitFromXml;
%ignore FdoClassDefinition::Set;
%ignore FdoClassDefinition::_writeXml;
%ignore FdoClass::InitFromXml;
%ignore FdoClass::_writeXml;
%ignore FdoFeatureClass::InitFromXml;
%ignore FdoFeatureClass::Set;
%ignore FdoFeatureClass::_writeBaseXml;
%ignore FdoFeatureClass::_writeXml;

%ignore FdoGeometricPropertyDefinition::InitFromXml;
%ignore FdoPropertyDefinition::InitFromXml;
%ignore FdoSchemaElement::InitFromXml;
%ignore FdoGeometricPropertyDefinition::_writeXml;
%ignore FdoPropertyDefinition::_writeXml;

%ignore FdoFgfGeometryFactory::GetByteArray;
%ignore FdoFgfGeometryFactory::TakeReleasedByteArray;

%ignore FdoFeatureSchema::CheckReferences;
%ignore FdoObjectPropertyDefinition::CheckReferences;
%ignore FdoSchemaElement::CheckReferences;
%ignore FdoGeometricPropertyDefinition::Set;
%ignore FdoPropertyDefinition::Set;
%ignore FdoPropertyValueConstraint::Set;
%ignore FdoPropertyValueConstraintList::Set;
%ignore FdoPropertyValueConstraintRange::Set;
%ignore FdoSchemaElement::Set;
%ignore FdoFeatureSchema::GetFromInternalStylesheet;

%ignore FdoStringElement::Create;

%ignore FdoGeometricPropertyDefinition::SetSpecificGeometryTypes;

%ignore FdoXmlCopyHandler::Create(FdoXmlWriter*, FdoString*, FdoString*, FdoString*, FdoXmlAttributeCollection*);
%ignore FdoXmlCopyHandler::Create(FdoXmlWriter*, FdoString*, FdoString*, FdoString*, FdoXmlAttributeCollection*, FdoDictionary*);
%ignore FdoGeometricPropertyDefinition::InitFromXml;
%ignore FdoPhysicalClassMapping::InitFromXml;
%ignore FdoPhysicalElementMapping::InitFromXml;
%ignore FdoPhysicalPropertyMapping::InitFromXml;
%ignore FdoPropertyDefinition::InitFromXml;
%ignore FdoSchemaElement::InitFromXml;
%ignore FdoFeatureSchemaCollection::XmlStartElement;
%ignore FdoPhysicalSchemaMappingCollection::XmlStartElement;
%ignore FdoXmlSaxHandler::XmlStartElement;

%ignore FdoPhysicalClassMapping::_writeXml;
%ignore FdoPhysicalElementMapping::_writeXml;
%ignore FdoPhysicalPropertyMapping::_writeXml;

%ignore FdoPhysicalElementMapping::ChoiceSubElementError;

%ignore FdoInCondition::Create(FdoIdentifier*,FdoString**,FdoInt32);
%ignore FdoInCondition::Create(FdoString*,FdoString**,FdoInt32);

%ignore FdoValueExpressionCollection::Create(FdoString**, FdoInt32);

%ignore FdoIDirectPosition::GetOrdinates;
%ignore FdoIEnvelope::GetOrdinates;
%ignore FdoILinearRing::GetOrdinates;
%ignore FdoILinearRing::GetItemByMembers;
%ignore FdoILineString::GetOrdinates;
%ignore FdoILineString::GetItemByMembers;
%ignore FdoILineStringSegment::GetOrdinates;
%ignore FdoIMultiPoint::GetOrdinates;
%ignore FdoIPoint::GetOrdinates;
%ignore FdoIPoint::GetPositionByMembers;
%ignore FdoGeometryStreamFactory;
%ignore FdoGeometryStreamWriter;
%ignore FdoGeometryStreamReader;

%ignore FdoIFeatureReader::GetGeometry(FdoInt32, FdoInt32*);
%ignore FdoIFeatureReader::GetGeometry(FdoString*, FdoInt32*);

%ignore FdoLOBValue::operator FdoByteArray*;

%ignore FdoFgfGeometryFactory::GetPrivateInstance;
%ignore FdoFgfGeometryFactory::CreateLinearRing(FdoInt32, FdoInt32, double*);
%ignore FdoFgfGeometryFactory::CreateLineString(FdoInt32, FdoInt32, double*);
%ignore FdoFgfGeometryFactory::CreateLineStringSegment(FdoInt32, FdoInt32, double*);
%ignore FdoFgfGeometryFactory::CreateMultiPoint(FdoInt32, FdoInt32, double*);
%ignore FdoFgfGeometryFactory::CreatePoint(FdoInt32, double*);

/* Ignore all Topology classes */
%ignore FdoIActivateTopologyArea;
%ignore FdoIActivateTopologyInCommandResults;
%ignore FdoIDeactivateTopologyArea;
%ignore FdoIDeactivateTopologyInCommandResults;
%ignore FdoIMoveTopoNode;
%ignore FdoIReconnectTopoEdge;
%ignore FdoTopoFeaturePropertyDefinition;
%ignore FdoTopoGeometryPropertyDefinition;
%ignore FdoTopology;

/* Ignore the specialized operators used for Datavalue typecasting */
%ignore FdoStringValue::operator wchar_t*;
%ignore FdoBLOBValue::operator FdoByteArray*;
%ignore FdoByteValue::operator FdoByte;
%ignore FdoCLOBValue::operator FdoByteArray*;
%ignore FdoDateTimeValue::operator FdoDateTime;
%ignore FdoDecimalValue::operator double;
%ignore FdoInt16Value::operator FdoInt16;
%ignore FdoInt32Value::operator FdoInt32;
%ignore FdoBooleanValue::operator bool;
%ignore FdoSingleValue::operator float;
%ignore FdoDoubleValue::operator double;
%ignore FdoInt64Value::operator FdoInt64;

/* Ignore all FdoGeometryAbstract methods */
%ignore FdoGeometryFactoryAbstract;

/* Ignore these schema methods */
%ignore FdoClassCapabilities::Set(FdoClassCapabilities*);
%ignore FdoSchemaElement::SetParent(FdoSchemaElement*);
%ignore FdoReadOnlyArgumentDefinitionCollection::Dispose;
%ignore FdoISchemaCapabilities::SupportsInheritence;
%ignore FdoFeatureSchema::GetRelations;
%ignore FdoGeometricPropertyDefinition::GetAllDefaults;
%ignore FdoGeometricPropertyDefinition::GetSpecificGeometryTypes;
%ignore FdoIdentifierCollection::Dispose();
%ignore FdoParameterValueCollection::Dispose();
%ignore FdoSchemaElement::XmlEndElement;
%ignore FdoSchemaElement::XmlStartElement;

/* Ignore the FdoFeatureSchema methods */ 
%ignore FdoFeatureSchema::_getFromInternalStylesheet();
%ignore FdoFeatureSchema::Set;
%ignore FdoFeatureSchema::XmlStartElement;
%ignore FdoFeatureSchema::_writeXml; 

/* Ignore certain long transaction methods */
%ignore FdoIChangeLongTransactionPrivileges::GetUserName;
%ignore FdoILongTransactionPrivilegeReader::GetUserName;

/* Ignore the Feature Access manager method Reset */
%ignore FdoFeatureAccessManager::Reset;
%ignore FdoDataValue::Create(FdoString* value, FdoDataType dataType);
%ignore FdoDataValue::GetXmlValue();

%ignore FdoISpatialContextReader::Dispose();
%ignore FdoClassDefinition::writeXmlBaseProperties;
%ignore FdoClassDefinition::_writeXmlBaseProperties;
//Abstract FDO classes that have public ctors
%ignore FdoICircularArcSegment::FdoICircularArcSegment();
%ignore FdoBLOBStreamReader::FdoBLOBStreamReader();
%ignore FdoIConnectionPropertyDictionary::FdoIConnectionPropertyDictionary();
%ignore FdoIDataStorePropertyDictionary::FdoIDataStorePropertyDictionary();