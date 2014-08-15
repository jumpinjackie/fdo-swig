// Ignore FDO Exception classes, because we'll have our own managed ones
%ignore FdoException;
%ignore FdoClientServiceException;
%ignore FdoAutogenerationException;
%ignore FdoCommandException;
%ignore FdoConnectionException;
%ignore FdoExpressionException;
%ignore FdoFilterException;
%ignore FdoSchemaException;
%ignore FdoSpatialContextMismatchException;
%ignore FdoXmlException;

// Infrastructure to allow custom C# exceptions to be thrown
// Copy-pasta'd from the SWIG example: http://www.swig.org/Doc3.0/SWIGDocumentation.html#CSharp_exceptions
//
%insert(runtime) %{
  // Code to handle throwing of C# ManagedFdoException from C/C++ code.
  // The equivalent delegate to the callback, CSharpExceptionCallback_t, is CustomExceptionDelegate
  // and the equivalent customExceptionCallback instance is customDelegate
  typedef void (SWIGSTDCALL* CSharpExceptionCallback_t)(const char *);
  CSharpExceptionCallback_t customExceptionCallback = NULL;

  extern "C" SWIGEXPORT
  void SWIGSTDCALL CustomExceptionRegisterCallback(CSharpExceptionCallback_t customCallback) {
    customExceptionCallback = customCallback;
  }

  // Note that SWIG detects any method calls named starting with
  // SWIG_CSharpSetPendingException for warning 845
  static void SWIG_CSharpSetPendingExceptionCustomFdo(const char *msg) {
    customExceptionCallback(msg);
  }
%}

%pragma(csharp) imclasscode=%{
  class CustomExceptionHelper {
    // C# delegate for the C/C++ customExceptionCallback
    public delegate void CustomExceptionDelegate(string message);
    static CustomExceptionDelegate customDelegate =
                                   new CustomExceptionDelegate(SetPendingCustomException);

    [global::System.Runtime.InteropServices.DllImport("$dllimport", EntryPoint="CustomExceptionRegisterCallback")]
    public static extern
           void CustomExceptionRegisterCallback(CustomExceptionDelegate customCallback);

    static void SetPendingCustomException(string message) {
      SWIGPendingException.Set(new ManagedFdoException(message));
    }

    static CustomExceptionHelper() {
      CustomExceptionRegisterCallback(customDelegate);
    }
  }
  static CustomExceptionHelper exceptionHelper = new CustomExceptionHelper();
%}

//Inject this try/catch block into all API operations, catch any FdoExceptions and rethrow them on
//the .net side
%exception {
    try {
        $action
    }
    catch (FdoException *ex) {
        FdoString* msg = ex->GetExceptionMessage();
        std::string mbMsg = W2A_SLOW(msg);
        SWIG_CSharpSetPendingExceptionCustomFdo(mbMsg.c_str());
        FDO_SAFE_RELEASE(ex);
    }
}