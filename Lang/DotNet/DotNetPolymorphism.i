// DotNetPolymorphism.i
//
// Polymorphism support for the FDO .net wrapper
//
// NOTE: We're only targeting known cases in the API where polymorphic types
// are returned

// Case 1: FdoIConnection::CreateCommand(FdoInt32 commandType)
// HACK: Note the hard-coded "commandType" parameter name. If this name ever changes in the C++ header
// we must update it here. If there was a way in SWIG to do this (ala. $1) we would've done that!
%typemap(csout) FdoICommand* FdoIConnection::CreateCommand {
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