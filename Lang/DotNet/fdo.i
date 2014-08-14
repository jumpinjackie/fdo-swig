%module FDO

%include "wchar.i"

//SWIG is ref-counting aware, so let's take advantage of it!
%feature("ref") FdoIDisposable "FDO_SAFE_ADDREF($this);"
%feature("unref") FdoIDisposable "FDO_SAFE_RELEASE($this);"
%newobject *::Create;

//======= C#-specific ==========
//Make all classes partial, so we can customize said classes outside of swig
%typemap(csclassmodifiers) SWIGTYPE "public partial class"

%include "../Common/FdoIgnore.i"
%include "../Common/FdoIncludes.i"
%{
// FDO headers to include into SWIG wrapper
#include "Fdo.h"
%}