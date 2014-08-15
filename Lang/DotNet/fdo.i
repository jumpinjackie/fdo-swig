%module FdoDotNet

%include "wchar.i"

//SWIG is ref-counting aware, so let's take advantage of it!
%feature("ref") FdoIDisposable "FDO_SAFE_ADDREF($this);"
%feature("unref") FdoIDisposable "FDO_SAFE_RELEASE($this);"
%newobject *::Create;

//======= C#-specific ==========
//Make all classes partial, so we can customize said classes outside of swig
%typemap(csclassmodifiers) SWIGTYPE "public partial class"
//typedef unsigned char FdoByte;
//typedef short         FdoInt8;
//typedef short         FdoInt16;
//typedef int           FdoInt32;
//typedef long long     FdoInt64;

%{
// FDO headers to include into SWIG wrapper
#include "Fdo.h"

static std::string W2A_SLOW(const wchar_t* input)
{
    size_t wlen = wcslen(input);
    int mbslen = (int) wlen * 4 + 1;
    char* mbs = (char*)alloca(mbslen);
    wcstombs(mbs, input, mbslen);
    return std::string(mbs);
}

%}

%include "FdoCollections.i"
%include "FdoExceptions.i"
%include "../Common/FdoMarshal.i"
%include "../Common/FdoIgnore.i"
%include "../Common/FdoIncludes.i"