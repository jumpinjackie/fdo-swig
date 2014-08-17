// FdoMarshal.i
//
// This defines helper collection classes and typemaps to "box" methods in FDO
// that are not easy to wrap in their existing forms.
//
// TODO:
// We probably should use typemaps here, but until we can actually grok how they
// work, we'll take the wrap and box approach that we're doing here
//

//Ignore these methods as the collections being returned should be read-only
%ignore FdoBasicValueCollection::Add;
%ignore FdoBasicValueCollection::Clear;

%include "../Common/BasicValueCollection.h"
%template (FdoClassTypeCollectionBase) FdoBasicValueCollection<FdoClassType>;
%include "../Common/ClassTypeCollection.h"
%template (FdoConditionTypeCollectionBase) FdoBasicValueCollection<FdoConditionType>;
%include "../Common/ConditionTypeCollection.h"
%template (FdoDataTypeCollectionBase) FdoBasicValueCollection<FdoDataType>;
%include "../Common/DataTypeCollection.h"
%template (FdoDoubleCollectionBase) FdoBasicValueCollection<double>;
%include "../Common/DoubleCollection.h"
%template (FdoDistanceOperationsCollectionBase) FdoBasicValueCollection<FdoDistanceOperations>;
%include "../Common/DistanceOperationsCollection.h"
%template (FdoExpressionTypeCollectionBase) FdoBasicValueCollection<FdoExpressionType>;
%include "../Common/ExpressionTypeCollection.h"
%template (FdoGeometryComponentTypeCollectionBase) FdoBasicValueCollection<FdoGeometryComponentType>;
%include "../Common/GeometryComponentTypeCollection.h"
%template (FdoGeometryTypeCollectionBase) FdoBasicValueCollection<FdoGeometryType>;
%include "../Common/GeometryTypeCollection.h"
%template (FdoInt32CollectionBase) FdoBasicValueCollection<FdoInt32>;
%include "../Common/Int32Collection.h"
%template (FdoLockTypeCollectionBase) FdoBasicValueCollection<FdoLockType>;
%include "../Common/LockTypeCollection.h"
%template (FdoSpatialOperationsCollectionBase) FdoBasicValueCollection<FdoSpatialOperations>;
%include "../Common/SpatialOperationsCollection.h"
%template (FdoSpatialContextExtentTypeCollectionBase) FdoBasicValueCollection<FdoSpatialContextExtentType>;
%include "../Common/SpatialContextExtentTypeCollection.h"

//
//These %extend directives provide a collection-based API as a replacement for the original APIs that deal with C-arrays
//
%extend FdoISchemaCapabilities
{
    FdoClassTypeCollection* SupportedClassTypes()
    {
        FdoInt32 count = 0;
        FdoClassType* types = $self->GetClassTypes(count);
        FdoPtr<FdoClassTypeCollection> ret = FdoClassTypeCollection::Create();
        for (FdoInt32 i = 0; i < count; i++)
        {
            ret->Add(types[i]);
        }
        return ret.Detach();
    }
    
    FdoDataTypeCollection* SupportedDataTypes()
    {
        FdoInt32 count = 0;
        FdoDataType* types = $self->GetDataTypes(count);
        FdoPtr<FdoDataTypeCollection> ret = FdoDataTypeCollection::Create();
        for (FdoInt32 i = 0; i < count; i++)
        {
            ret->Add(types[i]);
        }
        return ret.Detach();
    }
    
    FdoDataTypeCollection* SupportedAutogeneratedDataTypes()
    {
        FdoInt32 count = 0;
        FdoDataType* types = $self->GetSupportedAutoGeneratedTypes(count);
        FdoPtr<FdoDataTypeCollection> ret = FdoDataTypeCollection::Create();
        for (FdoInt32 i = 0; i < count; i++)
        {
            ret->Add(types[i]);
        }
        return ret.Detach();
    }
    
    FdoDataTypeCollection* SupportedIdentityPropertyTypes()
    {
        FdoInt32 count = 0;
        FdoDataType* types = $self->GetSupportedIdentityPropertyTypes(count);
        FdoPtr<FdoDataTypeCollection> ret = FdoDataTypeCollection::Create();
        for (FdoInt32 i = 0; i < count; i++)
        {
            ret->Add(types[i]);
        }
        return ret.Detach();
    }
};

%extend FdoIFilterCapabilities
{
    FdoConditionTypeCollection* SupportedConditionTypes()
    {
        FdoInt32 count = 0;
        FdoConditionType* types = $self->GetConditionTypes(count);
        FdoPtr<FdoConditionTypeCollection> ret = FdoConditionTypeCollection::Create();
        for (FdoInt32 i = 0; i < count; i++)
        {
            ret->Add(types[i]);
        }
        return ret.Detach();
    }
    
    FdoSpatialOperationsCollection* SupportedSpatialOperations()
    {
        FdoInt32 count = 0;
        FdoSpatialOperations* types = $self->GetSpatialOperations(count);
        FdoPtr<FdoSpatialOperationsCollection> ret = FdoSpatialOperationsCollection::Create();
        for (FdoInt32 i = 0; i < count; i++)
        {
            ret->Add(types[i]);
        }
        return ret.Detach();
    }
    
    FdoDistanceOperationsCollection* SupportedDistanceOperations()
    {
        FdoInt32 count = 0;
        FdoDistanceOperations* types = $self->GetDistanceOperations(count);
        FdoPtr<FdoDistanceOperationsCollection> ret = FdoDistanceOperationsCollection::Create();
        for (FdoInt32 i = 0; i < count; i++)
        {
            ret->Add(types[i]);
        }
        return ret.Detach();
    }
};

%extend FdoIExpressionCapabilities
{
    FdoExpressionTypeCollection* SupportedExpressionTypes()
    {
        FdoInt32 count = 0;
        FdoExpressionType* types = $self->GetExpressionTypes(count);
        FdoPtr<FdoExpressionTypeCollection> ret = FdoExpressionTypeCollection::Create();
        for (FdoInt32 i = 0; i < count; i++)
        {
            ret->Add(types[i]);
        }
        return ret.Detach();
    }
};

%extend FdoIGeometryCapabilities
{
    FdoGeometryComponentTypeCollection* SupportedGeometryComponentTypes()
    {
        FdoInt32 count = 0;
        FdoGeometryComponentType* types = $self->GetGeometryComponentTypes(count);
        FdoPtr<FdoGeometryComponentTypeCollection> ret = FdoGeometryComponentTypeCollection::Create();
        for (FdoInt32 i = 0; i < count; i++)
        {
            ret->Add(types[i]);
        }
        return ret.Detach();
    }
    
    FdoGeometryTypeCollection* SupportedGeometryTypes()
    {
        FdoInt32 count = 0;
        FdoGeometryType* types = $self->GetGeometryTypes(count);
        FdoPtr<FdoGeometryTypeCollection> ret = FdoGeometryTypeCollection::Create();
        for (FdoInt32 i = 0; i < count; i++)
        {
            ret->Add(types[i]);
        }
        return ret.Detach();
    }
};

%extend FdoIConnectionCapabilities
{
    FdoLockTypeCollection* SupportedLockTypes()
    {
        FdoInt32 count = 0;
        FdoLockType* types = $self->GetLockTypes(count);
        FdoPtr<FdoLockTypeCollection> ret = FdoLockTypeCollection::Create();
        for (FdoInt32 i = 0; i < count; i++)
        {
            ret->Add(types[i]);
        }
        return ret.Detach();
    }
    
    FdoSpatialContextExtentTypeCollection* SupportedSpatialContextExtentTypes()
    {
        FdoInt32 count = 0;
        FdoSpatialContextExtentType* types = $self->GetSpatialContextTypes(count);
        FdoPtr<FdoSpatialContextExtentTypeCollection> ret = FdoSpatialContextExtentTypeCollection::Create();
        for (FdoInt32 i = 0; i < count; i++)
        {
            ret->Add(types[i]);
        }
        return ret.Detach();
    }
};

%extend FdoICommandCapabilities
{
    FdoInt32Collection* SupportedCommands()
    {
        FdoInt32 count = 0;
        FdoInt32* types = $self->GetCommands(count);
        FdoPtr<FdoInt32Collection> ret = FdoInt32Collection::Create();
        for (FdoInt32 i = 0; i < count; i++)
        {
            ret->Add(types[i]);
        }
        return ret.Detach();
    }
};

%extend FdoClassCapabilities
{
    FdoLockTypeCollection* GetSupportedLockTypes()
    {
        FdoInt32 count = 0;
        FdoLockType* types = $self->GetLockTypes(count);
        FdoPtr<FdoLockTypeCollection> ret = FdoLockTypeCollection::Create();
        for (FdoInt32 i = 0; i < count; i++)
        {
            ret->Add(types[i]);
        }
        return ret.Detach();
    }
    
    void SetSupportedLockTypes(FdoLockTypeCollection* value)
    {
        FdoInt32 count = value->GetCount();
        FdoLockType* types = new FdoLockType[count];
        
        for (FdoInt32 i = 0; i < count; i++)
        {
            types[i] = value->GetItem(i);
        }
        
        $self->SetLockTypes(types, count);
        
        delete [] types;
    }
};

%extend FdoGeometricPropertyDefinition
{
    void SetSpecificApplicableGeometryTypes(FdoGeometryTypeCollection* value)
    {
        FdoInt32 count = value->GetCount();
        FdoGeometryType* types = new FdoGeometryType[count];
        
        for (FdoInt32 i = 0; i < count; i++)
        {
            types[i] = value->GetItem(i);
        }
        
        $self->SetSpecificGeometryTypes(types, count);
        
        delete [] types;
    }
};

//
// These %extend directives provide replacement methods for ones that take FdoStringP arguments
//
%extend FdoStringCollection
{
    FdoStringCollection* Create(FdoString* inString, FdoString* delimiters, bool bNullTokens = false)
    {
        return FdoStringCollection::Create(FdoStringP(inString), delimiters, bNullTokens);
    }

    int Add(FdoString* value)
    {
        return $self->Add(FdoStringP(value));
    }
    
    int IndexOf(FdoString* value, bool caseSensitive = true)
    {
        return $self->IndexOf(FdoStringP(value), caseSensitive);
    }
    
    FdoString* ToString()
    {
        FdoStringP value = $self->ToString();
        return (FdoString*)value;
    }
    
    FdoString* ToString(FdoString* separator)
    {
        FdoStringP value = $self->ToString(separator);
        return (FdoString*)value;
    }
};

%extend FdoProviderNameTokens
{
    FdoString* GetLocalName()
    {
        FdoStringP value = $self->GetLocalName();
        return (FdoString*)value;
    }

    FdoStringCollection* GetNameTokens()
    {
        FdoPtr<FdoStringCollection> ret;
        
        FdoStringsP tokens = $self->GetNameTokens();
        ret = tokens;
        
        return ret.Detach();
    }
    
    FdoDoubleCollection* GetVersionTokens()
    {
        FdoPtr<FdoDoubleCollection> ret = FdoDoubleCollection::Create();
        
        FdoVectorP tokens = $self->GetVersionTokens();
        for (FdoInt32 i = 0; i < tokens->GetCount(); i++)
        {
            FdoPtr<FdoVectorElement> vecEl = tokens->GetItem(i);
            ret->Add(vecEl->GetValue());
        }
        
        return ret.Detach();
    }
};

%extend FdoIPropertyDictionary
{
    FdoStringCollection* EnumeratePropertyValues(FdoString* name)
    {
        FdoPtr<FdoStringCollection> ret = FdoStringCollection::Create();
        
        FdoInt32 count = 0;
        FdoString** values = $self->EnumeratePropertyValues(name, count);
        for (FdoInt32 i = 0; i < count; i++)
        {
            ret->Add(values[i]);
        }
        
        return ret.Detach();
    }
    
    FdoStringCollection* GetPropertyNames()
    {
        FdoPtr<FdoStringCollection> ret = FdoStringCollection::Create();
        
        FdoInt32 count = 0;
        FdoString** values = $self->GetPropertyNames(count);
        for (FdoInt32 i = 0; i < count; i++)
        {
            ret->Add(values[i]);
        }
        
        return ret.Detach();
    }
};

%extend FdoSchemaAttributeDictionary
{
    FdoStringCollection* GetAttributeNames()
    {
        FdoPtr<FdoStringCollection> ret = FdoStringCollection::Create();
        
        FdoInt32 count = 0;
        FdoString** values = $self->GetAttributeNames(count);
        for (FdoInt32 i = 0; i < count; i++)
        {
            ret->Add(values[i]);
        }
        
        return ret.Detach();
    }
}

%extend FdoIdentifier
{
    FdoStringCollection* GetScope()
    {
        FdoPtr<FdoStringCollection> ret = FdoStringCollection::Create();
        
        FdoInt32 count = 0;
        FdoString** values = $self->GetScope(count);
        for (FdoInt32 i = 0; i < count; i++)
        {
            ret->Add(values[i]);
        }
        
        return ret.Detach();
    }
}

%extend FdoXmlReader
{
    FdoString* DecodeName(FdoString* name)
    {
        FdoStringP value = $self->DecodeName(FdoStringP(name));
        return (FdoString*) value;
    }
};

%extend FdoXmlWriter
{
    FdoString* EncodeName(FdoString* name)
    {
        FdoStringP value = $self->EncodeName(FdoStringP(name));
        return (FdoString*) value;
    }
    
    bool IsValidName(FdoString* name)
    {
        return $self->IsValidName(FdoStringP(name));
    }
    
    FdoString* UriToQName(FdoString* uri, FdoString* localName, FdoBoolean isElement = true)
    {
        FdoStringP value = $self->UriToQName(uri, localName, isElement);
        return (FdoString*)value;
    }
};

%extend FdoXmlAttribute
{
    FdoString* GetLocalName()
    {
        FdoStringP value = $self->GetLocalName();
        return (FdoString*) value;
    }
    
    FdoString* GetLocalValue()
    {
        FdoStringP value = $self->GetLocalValue();
        return (FdoString*) value;
    }
    
    FdoString* GetPrefix()
    {
        FdoStringP value = $self->GetPrefix();
        return (FdoString*) value;
    }
    
    FdoString* GetQName()
    {
        FdoStringP value = $self->GetQName();
        return (FdoString*) value;
    }
    
    FdoString* GetUri()
    {
        FdoStringP value = $self->GetUri();
        return (FdoString*) value;
    }
    
    FdoString* GetValuePrefix()
    {
        FdoStringP value = $self->GetValuePrefix();
        return (FdoString*) value;
    }
    
    FdoString* GetValueUri()
    {
        FdoStringP value = $self->GetValueUri();
        return (FdoString*) value;
    }
};

%extend FdoClassDefinition
{
    FdoString* GetQualifiedName()
    {
        FdoStringP value = $self->GetQualifiedName();
        return (FdoString*) value;
    }
};

%extend FdoPhysicalElementMapping
{
    FdoString* GetQualifiedName()
    {
        FdoStringP value = $self->GetQualifiedName();
        return (FdoString*) value;
    }
};

%extend FdoPropertyDefinition
{
    FdoString* GetQualifiedName()
    {
        FdoStringP value = $self->GetQualifiedName();
        return (FdoString*) value;
    }
};

%extend FdoSchemaElement
{
    FdoString* GetQualifiedName()
    {
        FdoStringP value = $self->GetQualifiedName();
        return (FdoString*) value;
    }
};

%extend FdoStringElement
{
    FdoString* GetString()
    {
        FdoStringP value = $self->GetString();
        return (FdoString*) value;
    }
};