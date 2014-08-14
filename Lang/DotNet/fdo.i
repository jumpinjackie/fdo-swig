%module FDO

%include "wchar.i"

//SWIG is ref-counting aware, so let's take advantage of it!
%feature("ref") FdoIDisposable "FDO_SAFE_ADDREF($this);"
%feature("unref") FdoIDisposable "FDO_SAFE_RELEASE($this);"
%newobject *::Create;

%include "../Common/FdoIgnore.i"
%include "../Common/FdoIncludes.i"
%{
// FDO headers to include into SWIG wrapper
#include "Fdo.h"
%}