$extend FdoPropertyValueCollection
{
    
};

%extend FdoIdentifierCollection
{
    FdoInt32 AddIdentifier(FdoString* name)
    {
        FdoPtr<FdoIdentifier> ident = FdoIdentifier::Create(name);
        return $self->Add(ident);
    }
    
    FdoInt32 AddComputedIdentifier(FdoString* name, FdoString* exprText)
    {
        FdoPtr<FdoExpression> expr = FdoExpression::Parse(exprText);
        FdoPtr<FdoComputedIdentifier> compIdent = FdoComputedIdentifier::Create(name, expr);
        return $self->Add(compIdent);
    }
};

%extend FdoFeatureSchemaCollection
{
    FdoClassDefinition* GetClassDefinition(FdoString* schemaName, FdoString* className)
    {
        FdoPtr<FdoClassDefinition> clsDef;
        if (NULL != schemaName)
        {
            //Find first matching class name in matching schema
            for (FdoInt32 i = 0; i < $self->GetCount(); i++)
            {
                FdoPtr<FdoFeatureSchema> schema = $self->GetItem(i);
                if (wcscmp(schema->GetName(), schemaName) == 0)
                {
                    FdoPtr<FdoClassCollection> classes = schema->GetClasses();
                    FdoInt32 cidx = classes->IndexOf(className);
                    if (cidx >= 0)
                    {
                        clsDef = classes->GetItem(cidx);
                        break;
                    }
                }
            }
        }
        else
        {
            //Find first matching class name in all schemas
            for (FdoInt32 i = 0; i < $self->GetCount(); i++)
            {
                FdoPtr<FdoFeatureSchema> schema = $self->GetItem(i);
                FdoPtr<FdoClassCollection> classes = schema->GetClasses();
                FdoInt32 cidx = classes->IndexOf(className);
                if (cidx >= 0)
                {
                    clsDef = classes->GetItem(cidx);
                    break;
                }
            }
        }
        return clsDef.Detach();
    }
};