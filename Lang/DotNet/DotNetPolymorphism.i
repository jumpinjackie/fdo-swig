// DotNetPolymorphism.i
//
// Polymorphism support for the FDO .net wrapper
//
// NOTE: We're only targeting known cases in the API where polymorphic types
// are returned

// Case 1: FdoIConnection::CreateCommand(FdoInt32 commandType)
// HACK: Note the hard-coded "commandType" parameter name. If this name ever changes in the C++ header
// we must update it here. If there was a way in SWIG to do this (ala. $1) we would've done that!
%typemap(csout, excode=SWIGEXCODE) FdoICommand* FdoIConnection::CreateCommand {
    global::System.IntPtr cPtr = $imcall;$excode;
    if (cPtr != global::System.IntPtr.Zero && global::System.Enum.IsDefined(typeof(FdoCommandType), commandType))
    {
        //NOTE: The full FDO command suite is not supported here. Only the most common ones are handled
        FdoCommandType ctype = (FdoCommandType)commandType;
        switch (ctype)
        {
            case FdoCommandType.FdoCommandType_ApplySchema:
                return new FdoIApplySchema(cPtr, $owner);
            case FdoCommandType.FdoCommandType_CreateDataStore:
                return new FdoICreateDataStore(cPtr, $owner);
            case FdoCommandType.FdoCommandType_CreateSpatialContext:
                return new FdoICreateSpatialContext(cPtr, $owner);
            case FdoCommandType.FdoCommandType_Delete:
                return new FdoIDelete(cPtr, $owner);
            case FdoCommandType.FdoCommandType_DescribeSchema:
                return new FdoIDescribeSchema(cPtr, $owner);
            case FdoCommandType.FdoCommandType_DescribeSchemaMapping:
                return new FdoIDescribeSchemaMapping(cPtr, $owner);
            case FdoCommandType.FdoCommandType_DestroyDataStore:
                return new FdoIDestroyDataStore(cPtr, $owner);
            case FdoCommandType.FdoCommandType_DestroySchema:
                return new FdoIDestroySchema(cPtr, $owner);
            case FdoCommandType.FdoCommandType_DestroySpatialContext:
                return new FdoIDestroySpatialContext(cPtr, $owner);
            case FdoCommandType.FdoCommandType_ExtendedSelect:
                return new FdoIExtendedSelect(cPtr, $owner);
            case FdoCommandType.FdoCommandType_GetClassNames:
                return new FdoIGetClassNames(cPtr, $owner);
            case FdoCommandType.FdoCommandType_GetSchemaNames:
                return new FdoIGetSchemaNames(cPtr, $owner);
            case FdoCommandType.FdoCommandType_GetSpatialContexts:
                return new FdoIGetSpatialContexts(cPtr, $owner);
            case FdoCommandType.FdoCommandType_Insert:
                return new FdoIInsert(cPtr, $owner);
            case FdoCommandType.FdoCommandType_ListDataStores:
                return new FdoIListDataStores(cPtr, $owner);
            case FdoCommandType.FdoCommandType_Select:
                return new FdoISelect(cPtr, $owner);
            case FdoCommandType.FdoCommandType_SelectAggregates:
                return new FdoISelectAggregates(cPtr, $owner);
            case FdoCommandType.FdoCommandType_SQLCommand:
                return new FdoISQLCommand(cPtr, $owner);
            case FdoCommandType.FdoCommandType_Update:
                return new FdoIUpdate(cPtr, $owner);
            default:
                throw new global::System.NotSupportedException(global::System.String.Format("Command type {0} is either invalid or not supported/implemented by this wrapper API", ctype));
        }
    }
    else
    {
        return null;
    }
}

//Case 2: Anything that returns FdoClassDefinition*
%typemap(csout, excode=SWIGEXCODE) FdoClassDefinition* {
    global::System.IntPtr cPtr = $imcall;$excode;
    if (cPtr != global::System.IntPtr.Zero)
    {
        $csclassname tmp = new $csclassname(cPtr, false);
        FdoClassType ctype = tmp.GetClassType();
        switch (ctype)
        {
            case FdoClassType.FdoClassType_Class:
                return new FdoClass(cPtr, $owner);
            case FdoClassType.FdoClassType_FeatureClass:
                return new FdoFeatureClass(cPtr, $owner);
            default:
                throw new global::System.NotSupportedException(global::System.String.Format("class type {0} is either invalid or not supported/implemented by this wrapper API", ctype));
        }
    }
    else
    {
        return null;
    }
}

//Case 3: Anything that returns FdoIGeometry*
%typemap(csout, excode=SWIGEXCODE) FdoIGeometry* {
    global::System.IntPtr cPtr = $imcall;$excode;
    if (cPtr != global::System.IntPtr.Zero)
    {
        $csclassname tmp = new $csclassname(cPtr, false);
        FdoGeometryType gtype = tmp.GetDerivedType();
        switch (gtype)
        {
            case FdoGeometryType.FdoGeometryType_Point:
                return new FdoIPoint(cPtr, $owner);
            case FdoGeometryType.FdoGeometryType_LineString:
                return new FdoILineString(cPtr, $owner);
            case FdoGeometryType.FdoGeometryType_Polygon:
                return new FdoIPolygon(cPtr, $owner);
            case FdoGeometryType.FdoGeometryType_MultiPoint:
                return new FdoIMultiPoint(cPtr, $owner);
            case FdoGeometryType.FdoGeometryType_MultiLineString:
                return new FdoIMultiLineString(cPtr, $owner);
            case FdoGeometryType.FdoGeometryType_MultiPolygon:
                return new FdoIMultiPolygon(cPtr, $owner);
            case FdoGeometryType.FdoGeometryType_MultiGeometry:
                return new FdoIMultiGeometry(cPtr, $owner);
            case FdoGeometryType.FdoGeometryType_CurveString:
                return new FdoICurveString(cPtr, $owner);
            case FdoGeometryType.FdoGeometryType_CurvePolygon:
                return new FdoICurvePolygon(cPtr, $owner);
            case FdoGeometryType.FdoGeometryType_MultiCurveString:
                return new FdoIMultiCurveString(cPtr, $owner);
            case FdoGeometryType.FdoGeometryType_MultiCurvePolygon:
                return new FdoIMultiCurvePolygon(cPtr, $owner);
            default:
                throw new global::System.NotSupportedException(global::System.String.Format("geometry type {0} is either invalid or not supported/implemented by this wrapper API", gtype));
        }
    }
    else
    {
        return null;
    }
}

//Case 4: Anything that returns FdoExpression*
%typemap(csout, excode=SWIGEXCODE) FdoExpression* {
    global::System.IntPtr cPtr = $imcall;$excode;
    if (cPtr != global::System.IntPtr.Zero)
    {
        $csclassname tmp = new $csclassname(cPtr, false);
        FdoExpressionItemType etype = tmp.GetExpressionType();
        switch (etype)
        {
            case FdoExpressionItemType.FdoExpressionItemType_Identifier:
                return new FdoIdentifier(cPtr, $owner);
            case FdoExpressionItemType.FdoExpressionItemType_ComputedIdentifier:
                return new FdoComputedIdentifier(cPtr, $owner);
            case FdoExpressionItemType.FdoExpressionItemType_SubSelectExpression:
                return new FdoSubSelectExpression(cPtr, $owner);
            case FdoExpressionItemType.FdoExpressionItemType_Parameter:
                return new FdoParameter(cPtr, $owner);
            case FdoExpressionItemType.FdoExpressionItemType_Function:
                return new FdoFunction(cPtr, $owner);
            case FdoExpressionItemType.FdoExpressionItemType_BinaryExpression:
                return new FdoBinaryExpression(cPtr, $owner);
            case FdoExpressionItemType.FdoExpressionItemType_UnaryExpression:
                return new FdoUnaryExpression(cPtr, $owner);
            case FdoExpressionItemType.FdoExpressionItemType_DataValue:
                {
                    FdoDataValue tmpData = new FdoDataValue(cPtr, false);
                    FdoDataType dt = tmpData.GetDataType();
                    switch (dt)
                    {
                        case FdoDataType.FdoDataType_BLOB:
                            return new FdoBLOBValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_Boolean:
                            return new FdoBooleanValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_Byte:
                            return new FdoByteValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_CLOB:
                            return new FdoCLOBValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_DateTime:
                            return new FdoDateTimeValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_Decimal:
                            return new FdoDecimalValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_Double:
                            return new FdoDoubleValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_Int16:
                            return new FdoInt16Value(cPtr, $owner);
                        case FdoDataType.FdoDataType_Int32:
                            return new FdoInt32Value(cPtr, $owner);
                        case FdoDataType.FdoDataType_Int64:
                            return new FdoInt64Value(cPtr, $owner);
                        case FdoDataType.FdoDataType_Single:
                            return new FdoSingleValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_String:
                            return new FdoStringValue(cPtr, $owner);
                        default:
                            throw new global::System.NotSupportedException(global::System.String.Format("expression type {0} is either an invalid data type or not supported/implemented by this wrapper API", etype));
                    }
                }
            case FdoExpressionItemType.FdoExpressionItemType_GeometryValue:
                return new FdoGeometryValue(cPtr, $owner);
            default:
                throw new global::System.NotSupportedException(global::System.String.Format("expression type {0} is either invalid or not supported/implemented by this wrapper API", etype));
        }
    }
    else
    {
        return null;
    }
}

//Case 5: Anything that returns FdoValueExpression*
%typemap(csout, excode=SWIGEXCODE) FdoValueExpression* {
    global::System.IntPtr cPtr = $imcall;$excode;
    if (cPtr != global::System.IntPtr.Zero)
    {
        $csclassname tmp = new $csclassname(cPtr, false);
        FdoExpressionItemType etype = tmp.GetExpressionType();
        switch (etype)
        {
            case FdoExpressionItemType.FdoExpressionItemType_SubSelectExpression:
                return new FdoSubSelectExpression(cPtr, $owner);
            case FdoExpressionItemType.FdoExpressionItemType_Parameter:
                return new FdoParameter(cPtr, $owner);
            case FdoExpressionItemType.FdoExpressionItemType_DataValue:
                {
                    FdoDataValue tmpData = new FdoDataValue(cPtr, false);
                    FdoDataType dt = tmpData.GetDataType();
                    switch (dt)
                    {
                        case FdoDataType.FdoDataType_BLOB:
                            return new FdoBLOBValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_Boolean:
                            return new FdoBooleanValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_Byte:
                            return new FdoByteValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_CLOB:
                            return new FdoCLOBValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_DateTime:
                            return new FdoDateTimeValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_Decimal:
                            return new FdoDecimalValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_Double:
                            return new FdoDoubleValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_Int16:
                            return new FdoInt16Value(cPtr, $owner);
                        case FdoDataType.FdoDataType_Int32:
                            return new FdoInt32Value(cPtr, $owner);
                        case FdoDataType.FdoDataType_Int64:
                            return new FdoInt64Value(cPtr, $owner);
                        case FdoDataType.FdoDataType_Single:
                            return new FdoSingleValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_String:
                            return new FdoStringValue(cPtr, $owner);
                        default:
                            throw new global::System.NotSupportedException(global::System.String.Format("expression type {0} is either an invalid data type or not supported/implemented by this wrapper API", etype));
                    }
                }
            case FdoExpressionItemType.FdoExpressionItemType_GeometryValue:
                return new FdoGeometryValue(cPtr, $owner);
            default:
                throw new global::System.NotSupportedException(global::System.String.Format("expression type {0} is either invalid or not supported/implemented by this wrapper API", etype));
        }
    }
    else
    {
        return null;
    }
}

//Case 6: Anything that returns FdoLiteralValue*
%typemap(csout, excode=SWIGEXCODE) FdoLiteralValue* {
    global::System.IntPtr cPtr = $imcall;$excode;
    if (cPtr != global::System.IntPtr.Zero)
    {
        $csclassname tmp = new $csclassname(cPtr, false);
        FdoLiteralValueType lt = tmp.GetLiteralValueType();
        switch (lt)
        {
            case FdoLiteralValueType.FdoLiteralValueType_Data:
                {
                    FdoDataValue tmpData = new FdoDataValue(cPtr, false);
                    FdoDataType dt = tmpData.GetDataType();
                    switch (dt)
                    {
                        case FdoDataType.FdoDataType_BLOB:
                            return new FdoBLOBValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_Boolean:
                            return new FdoBooleanValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_Byte:
                            return new FdoByteValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_CLOB:
                            return new FdoCLOBValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_DateTime:
                            return new FdoDateTimeValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_Decimal:
                            return new FdoDecimalValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_Double:
                            return new FdoDoubleValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_Int16:
                            return new FdoInt16Value(cPtr, $owner);
                        case FdoDataType.FdoDataType_Int32:
                            return new FdoInt32Value(cPtr, $owner);
                        case FdoDataType.FdoDataType_Int64:
                            return new FdoInt64Value(cPtr, $owner);
                        case FdoDataType.FdoDataType_Single:
                            return new FdoSingleValue(cPtr, $owner);
                        case FdoDataType.FdoDataType_String:
                            return new FdoStringValue(cPtr, $owner);
                        default:
                            throw new global::System.NotSupportedException(global::System.String.Format("data type {0} is either an invalid data type or not supported/implemented by this wrapper API", dt));
                    }
                }
            case FdoLiteralValueType.FdoLiteralValueType_Geometry:
                return new FdoGeometryValue(cPtr, $owner);
            default:
                throw new global::System.NotSupportedException(global::System.String.Format("literal value type {0} is either an invalid data type or not supported/implemented by this wrapper API", lt));
        }
    }
    else
    {
        return null;
    }
}

//Case 7: Anything that returns FdoDataValue*
%typemap(csout, excode=SWIGEXCODE) FdoDataValue* {
    global::System.IntPtr cPtr = $imcall;$excode;
    if (cPtr != global::System.IntPtr.Zero)
    {
        $csclassname tmp = new $csclassname(cPtr, false);
        FdoDataType dt = tmp.GetDataType();
        switch (dt)
        {
            case FdoDataType.FdoDataType_BLOB:
                return new FdoBLOBValue(cPtr, $owner);
            case FdoDataType.FdoDataType_Boolean:
                return new FdoBooleanValue(cPtr, $owner);
            case FdoDataType.FdoDataType_Byte:
                return new FdoByteValue(cPtr, $owner);
            case FdoDataType.FdoDataType_CLOB:
                return new FdoCLOBValue(cPtr, $owner);
            case FdoDataType.FdoDataType_DateTime:
                return new FdoDateTimeValue(cPtr, $owner);
            case FdoDataType.FdoDataType_Decimal:
                return new FdoDecimalValue(cPtr, $owner);
            case FdoDataType.FdoDataType_Double:
                return new FdoDoubleValue(cPtr, $owner);
            case FdoDataType.FdoDataType_Int16:
                return new FdoInt16Value(cPtr, $owner);
            case FdoDataType.FdoDataType_Int32:
                return new FdoInt32Value(cPtr, $owner);
            case FdoDataType.FdoDataType_Int64:
                return new FdoInt64Value(cPtr, $owner);
            case FdoDataType.FdoDataType_Single:
                return new FdoSingleValue(cPtr, $owner);
            case FdoDataType.FdoDataType_String:
                return new FdoStringValue(cPtr, $owner);
            default:
                throw new global::System.NotSupportedException(global::System.String.Format("data type {0} is either an invalid data type or not supported/implemented by this wrapper API", dt));
        }
    }
    else
    {
        return null;
    }
}

//Case 8: Anything that returns FdoPropertyDefinition*
%typemap(csout, excode=SWIGEXCODE) FdoPropertyDefinition* {
    global::System.IntPtr cPtr = $imcall;$excode;
    if (cPtr != global::System.IntPtr.Zero)
    {
        $csclassname tmp = new $csclassname(cPtr, false);
        FdoPropertyType pt = tmp.GetPropertyType();
        switch (pt)
        {
            case FdoPropertyType.FdoPropertyType_DataProperty:
                return new FdoDataPropertyDefinition(cPtr, $owner);
            case FdoPropertyType.FdoPropertyType_GeometricProperty:
                return new FdoGeometricPropertyDefinition(cPtr, $owner);
            case FdoPropertyType.FdoPropertyType_RasterProperty:
                return new FdoRasterPropertyDefinition(cPtr, $owner);
            case FdoPropertyType.FdoPropertyType_ObjectProperty:
                return new FdoObjectPropertyDefinition(cPtr, $owner);
            case FdoPropertyType.FdoPropertyType_AssociationProperty:
                return new FdoAssociationPropertyDefinition(cPtr, $owner);
            default:
                throw new global::System.NotSupportedException(global::System.String.Format("property type {0} is either an invalid data type or not supported/implemented by this wrapper API", pt));
        }
    }
    else
    {
        return null;
    }
}

//Case 9: Anything that returns FdoFilter*
//HACK: FdoFilter does not have a type code that gives us a hint as to what to cast to, so we'll monkey-patch this API in
%extend FdoFilter
{
    FdoInt32 GetFilterType()
    {
        if (dynamic_cast<FdoBinaryLogicalOperator*>($self) != NULL)
            return 1;
        if (dynamic_cast<FdoUnaryLogicalOperator*>($self) != NULL)
            return 2;
        if (dynamic_cast<FdoComparisonCondition*>($self) != NULL)
            return 3;
        if (dynamic_cast<FdoDistanceCondition*>($self) != NULL)
            return 4;
        if (dynamic_cast<FdoSpatialCondition*>($self) != NULL)
            return 5;
        if (dynamic_cast<FdoInCondition*>($self) != NULL)
            return 6;
        if (dynamic_cast<FdoNullCondition*>($self) != NULL)
            return 7;
        return -1;
    }
};
%typemap(csout, excode=SWIGEXCODE) FdoFilter* {
    global::System.IntPtr cPtr = $imcall;$excode;
    if (cPtr != global::System.IntPtr.Zero)
    {
        $csclassname tmp = new $csclassname(cPtr, false);
        int filterType = tmp.GetFilterType();
        switch (filterType)
        {
            case 1:
                return new FdoBinaryLogicalOperator(cPtr, $owner);
            case 2:
                return new FdoUnaryLogicalOperator(cPtr, $owner);
            case 3:
                return new FdoComparisonCondition(cPtr, $owner);
            case 4:
                return new FdoDistanceCondition(cPtr, $owner);
            case 5:
                return new FdoSpatialCondition(cPtr, $owner);
            case 6:
                return new FdoInCondition(cPtr, $owner);
            case 7:
                return new FdoNullCondition(cPtr, $owner);
            default:
                throw new global::System.NotSupportedException(global::System.String.Format("filter type {0} is either invalid or not supported/implemented by this wrapper API", filterType));
        }
    }
    else
    {
        return null;
    }
}