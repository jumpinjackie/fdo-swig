%module FdoDotNet

%include "wchar.i"
%include <csharp.swg>

//
//SWIG is ref-counting aware, so let's take advantage of it!
//

//Do not AddRef() as any pointer returned will either already be AddRef()'d at the C++ level or
//(if freshly allocated) will start off with a refcount of 1
%feature("ref") FdoIDisposable ""
//However, we still do need to release
%feature("unref") FdoIDisposable "FDO_SAFE_RELEASE($this);"
%newobject *::Create;

//======= C#-specific ==========
//Need to override Dispose() for proxy classes derived from FdoIDisposable
%typemap(csdestruct_derived, methodname="Dispose", methodmodifiers="public") SWIGTYPE {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          //Anything derived from FdoIDisposable can simply chain up to the parent Dispose()
          //where it will be properly de-referenced', otherwise call the SWIG generated
          //free function
          //
          //HACK: This should not be a runtime check, it should be a check we should ideally do from SWIG
          if (!typeof(FdoIDisposable).IsAssignableFrom(this.GetType())) {
            $imcall;
          }
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

//Make all classes partial, so we can customize said classes outside of swig
%typemap(csclassmodifiers) SWIGTYPE "public partial class"

typedef unsigned char FdoByte;
typedef short         FdoInt8;
typedef short         FdoInt16;
typedef int           FdoInt32;
typedef long long     FdoInt64;

%include "arrays_csharp.i"
%apply unsigned char INOUT[] { unsigned char* buffer }
%apply unsigned char INOUT[] { unsigned char* data }
%apply unsigned char INOUT[] { unsigned char* bytes }
%apply unsigned char INOUT[] { unsigned char* byteArray }
%apply unsigned char INOUT[] { unsigned char* value }
%apply unsigned char INPUT[] { const FdoByte* array }

%include "DotNetPolymorphism.i"

%{
#include <vector>
#include "Fdo.h"

#include "ByteArrayHandle.h"

//FDO helper collections. Not in official FDO API
#include "BasicValueCollection.h"
#include "ClassTypeCollection.h"
#include "ConditionTypeCollection.h"
#include "DataTypeCollection.h"
#include "DistanceOperationsCollection.h"
#include "ExpressionTypeCollection.h"
#include "GeometryComponentTypeCollection.h"
#include "GeometryTypeCollection.h"
#include "Int32Collection.h"
#include "DoubleCollection.h"
#include "LockTypeCollection.h"
#include "SpatialOperationsCollection.h"
#include "SpatialContextExtentTypeCollection.h"

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
%include "../Common/FdoIgnore.i"
%include "../Common/FdoMarshal_Ignore.i"
%include "../Common/FdoMarshal.i"
%include "../Common/FdoIncludes.i"