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

%ignore FdoContext;
%ignore FdoArray;
%ignore FdoStringP;
%ignore FdoStringsP;
%ignore FdoProviderNameTokensP;
%ignore FdoArrayHelper;
%ignore FdoSchemaMergeContext;
%ignore FdoSchemaAttributeDictionary;
%ignore FdoDirectPositionImpl;
%ignore FdoXmlFeatureFlags::Create;
%ignore FdoSubSelectExpression::ToStringInternal;

%ignore FdoFunctionDefinition::Create;
%ignore FdoIoFileStream::Create(FILE*);

%ignore FdoStringElement::operator =;
%ignore FdoStringCollection::operator +;
%ignore FdoStringCollection::operator +=;

%ignore FdoStringCollection::Create(FdoStringCollection const *);
%ignore FdoStringCollection::Create(FdoStringCollection const &);

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