%extend FdoPropertyValueCollection
{
    void SetBooleanValue(FdoString* name, FdoBoolean value)
    {
        FdoPtr<FdoPropertyValue> pv = $self->FindItem(name);
        if (NULL == pv.p)
        {
            FdoPtr<FdoBooleanValue> expr = FdoBooleanValue::Create(value);
            pv = FdoPropertyValue::Create(name, expr);
            $self->Add(pv);
            printf("Added new FdoBooleanValue\n");
        }
        else
        {
            FdoPtr<FdoBooleanValue> bv = dynamic_cast<FdoBooleanValue*>(pv->GetValue());
            if (NULL != bv.p)
            {
                bv->SetBoolean(value);
                printf("Set existing FdoBooleanValue\n");
            }
            else
            {
                FdoPtr<FdoBooleanValue> expr = FdoBooleanValue::Create(value);
                pv->SetValue(expr);
                printf("Overwrite with new FdoBooleanValue\n");
            }
        }
    }
    
    void SetByteValue(FdoString* name, FdoByte value)
    {
        FdoPtr<FdoPropertyValue> pv = $self->FindItem(name);
        if (NULL == pv.p)
        {
            FdoPtr<FdoByteValue> expr = FdoByteValue::Create(value);
            pv = FdoPropertyValue::Create(name, expr);
            $self->Add(pv);
        }
        else
        {
            FdoPtr<FdoByteValue> bv = dynamic_cast<FdoByteValue*>(pv->GetValue());
            if (NULL != bv.p)
            {
                bv->SetByte(value);
            }
            else
            {
                FdoPtr<FdoByteValue> expr = FdoByteValue::Create(value);
                pv->SetValue(expr);
            }
        }
    }
    
    void SetDateTimeValue(FdoString* name, FdoDateTime value)
    {
        FdoPtr<FdoPropertyValue> pv = $self->FindItem(name);
        if (NULL == pv.p)
        {
            FdoPtr<FdoDateTimeValue> expr = FdoDateTimeValue::Create(value);
            pv = FdoPropertyValue::Create(name, expr);
            $self->Add(pv);
        }
        else
        {
            FdoPtr<FdoDateTimeValue> dv = dynamic_cast<FdoDateTimeValue*>(pv->GetValue());
            if (NULL != dv.p)
            {
                dv->SetDateTime(value);
            }
            else
            {
                FdoPtr<FdoDateTimeValue> expr = FdoDateTimeValue::Create(value);
                pv->SetValue(expr);
            }
        }
    }
    
    void SetDoubleValue(FdoString* name, double value)
    {
        FdoPtr<FdoPropertyValue> pv = $self->FindItem(name);
        if (NULL == pv.p)
        {
            FdoPtr<FdoDoubleValue> expr = FdoDoubleValue::Create(value);
            pv = FdoPropertyValue::Create(name, expr);
            $self->Add(pv);
        }
        else
        {
            FdoPtr<FdoDoubleValue> dv = dynamic_cast<FdoDoubleValue*>(pv->GetValue());
            if (NULL != dv.p)
            {
                dv->SetDouble(value);
            }
            else
            {
                FdoPtr<FdoDoubleValue> expr = FdoDoubleValue::Create(value);
                pv->SetValue(expr);
            }
        }
    }
    
    void SetInt16Value(FdoString* name, FdoInt16 value)
    {
        FdoPtr<FdoPropertyValue> pv = $self->FindItem(name);
        if (NULL == pv.p)
        {
            FdoPtr<FdoInt16Value> expr = FdoInt16Value::Create(value);
            pv = FdoPropertyValue::Create(name, expr);
            $self->Add(pv);
        }
        else
        {
            FdoPtr<FdoInt16Value> iv = dynamic_cast<FdoInt16Value*>(pv->GetValue());
            if (NULL != iv.p)
            {
                iv->SetInt16(value);
            }
            else
            {
                FdoPtr<FdoInt16Value> expr = FdoInt16Value::Create(value);
                pv->SetValue(expr);
            }
        }
    }
    
    void SetInt32Value(FdoString* name, FdoInt32 value)
    {
        FdoPtr<FdoPropertyValue> pv = $self->FindItem(name);
        if (NULL == pv.p)
        {
            FdoPtr<FdoInt32Value> expr = FdoInt32Value::Create(value);
            pv = FdoPropertyValue::Create(name, expr);
            $self->Add(pv);
        }
        else
        {
            FdoPtr<FdoInt32Value> iv = dynamic_cast<FdoInt32Value*>(pv->GetValue());
            if (NULL != iv.p)
            {
                iv->SetInt32(value);
            }
            else
            {
                FdoPtr<FdoInt32Value> expr = FdoInt32Value::Create(value);
                pv->SetValue(expr);
            }
        }
    }
    
    void SetInt64Value(FdoString* name, FdoInt64 value)
    {
        FdoPtr<FdoPropertyValue> pv = $self->FindItem(name);
        if (NULL == pv.p)
        {
            FdoPtr<FdoInt64Value> expr = FdoInt64Value::Create(value);
            pv = FdoPropertyValue::Create(name, expr);
            $self->Add(pv);
        }
        else
        {
            FdoPtr<FdoInt64Value> iv = dynamic_cast<FdoInt64Value*>(pv->GetValue());
            if (NULL != iv.p)
            {
                iv->SetInt64(value);
            }
            else
            {
                FdoPtr<FdoInt64Value> expr = FdoInt64Value::Create(value);
                pv->SetValue(expr);
            }
        }
    }
    
    void SetSingleValue(FdoString* name, float value)
    {
        FdoPtr<FdoPropertyValue> pv = $self->FindItem(name);
        if (NULL == pv.p)
        {
            FdoPtr<FdoSingleValue> expr = FdoSingleValue::Create(value);
            pv = FdoPropertyValue::Create(name, expr);
            $self->Add(pv);
        }
        else
        {
            FdoPtr<FdoSingleValue> sv = dynamic_cast<FdoSingleValue*>(pv->GetValue());
            if (NULL != sv.p)
            {
                sv->SetSingle(value);
            }
            else
            {
                FdoPtr<FdoSingleValue> expr = FdoSingleValue::Create(value);
                pv->SetValue(expr);
            }
        }
    }
    
    void SetStringValue(FdoString* name, FdoString* value)
    {
        FdoPtr<FdoPropertyValue> pv = $self->FindItem(name);
        if (NULL == pv.p)
        {
            FdoPtr<FdoStringValue> expr = FdoStringValue::Create(value);
            pv = FdoPropertyValue::Create(name, expr);
            $self->Add(pv);
        }
        else
        {
            FdoPtr<FdoStringValue> sv = dynamic_cast<FdoStringValue*>(pv->GetValue());
            if (NULL != sv.p)
            {
                sv->SetString(value);
            }
            else
            {
                FdoPtr<FdoStringValue> expr = FdoStringValue::Create(value);
                pv->SetValue(expr);
            }
        }
    }
    
    void SetBLOBValue(FdoString* name, FdoByteArrayHandle* value)
    {
        FdoPtr<FdoPropertyValue> pv = $self->FindItem(name);
        if (NULL == pv.p)
        {
            FdoPtr<FdoByteArray> ba = value->GetInternalArray();
            FdoPtr<FdoBLOBValue> expr = FdoBLOBValue::Create(ba);
            pv = FdoPropertyValue::Create(name, expr);
            $self->Add(pv);
        }
        else
        {
            FdoPtr<FdoByteArray> ba = value->GetInternalArray();
            FdoPtr<FdoBLOBValue> bv = dynamic_cast<FdoBLOBValue*>(pv->GetValue());
            if (NULL != bv.p)
            {
                bv->SetData(ba);
            }
            else
            {
                FdoPtr<FdoBLOBValue> expr = FdoBLOBValue::Create(ba);
                pv->SetValue(expr);
            }
        }
    }
    
    void SetCLOBValue(FdoString* name, FdoByteArrayHandle* value)
    {
        FdoPtr<FdoPropertyValue> pv = $self->FindItem(name);
        if (NULL == pv.p)
        {
            FdoPtr<FdoByteArray> ba = value->GetInternalArray();
            FdoPtr<FdoCLOBValue> expr = FdoCLOBValue::Create(ba);
            pv = FdoPropertyValue::Create(name, expr);
            $self->Add(pv);
        }
        else
        {
            FdoPtr<FdoByteArray> ba = value->GetInternalArray();
            FdoPtr<FdoCLOBValue> bv = dynamic_cast<FdoCLOBValue*>(pv->GetValue());
            if (NULL != bv.p)
            {
                bv->SetData(ba);
            }
            else
            {
                FdoPtr<FdoCLOBValue> expr = FdoCLOBValue::Create(ba);
                pv->SetValue(expr);
            }
        }
    }
    
    void SetGeometryValue(FdoString* name, FdoByteArrayHandle* value)
    {
        FdoPtr<FdoPropertyValue> pv = $self->FindItem(name);
        if (NULL == pv.p)
        {
            FdoPtr<FdoByteArray> ba = value->GetInternalArray();
            FdoPtr<FdoGeometryValue> expr = FdoGeometryValue::Create(ba);
            pv = FdoPropertyValue::Create(name, expr);
            $self->Add(pv);
        }
        else
        {
            FdoPtr<FdoByteArray> ba = value->GetInternalArray();
            FdoPtr<FdoGeometryValue> gv = dynamic_cast<FdoGeometryValue*>(pv->GetValue());
            if (NULL != gv.p)
            {
                gv->SetGeometry(ba);
            }
            else
            {
                FdoPtr<FdoGeometryValue> expr = FdoGeometryValue::Create(ba);
                pv->SetValue(expr);
            }
        }
    }
    
    void SetValueNull(FdoString* name)
    {
        FdoPtr<FdoPropertyValue> pv = $self->FindItem(name);
        if (NULL != pv.p)
        {
            FdoPtr<FdoValueExpression> vex = pv->GetValue();
            FdoDataValue* dv = dynamic_cast<FdoDataValue*>(vex.p);
            FdoGeometryValue* gv = dynamic_cast<FdoGeometryValue*>(vex.p);
            if (NULL != dv)
            {
                dv->SetNull();
            }
            else if (NULL != gv)
            {
                gv->SetNullValue();
            }
        }
    }
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
        if (NULL == clsDef.p)
        {
            std::wstring msg = L"Class not found ";
            if (NULL != schemaName)
            {
                msg += schemaName;
                msg += L":";
            }
            msg += className;
            throw FdoSchemaException::Create(msg.c_str());
        }
        return clsDef.Detach();
    }
};