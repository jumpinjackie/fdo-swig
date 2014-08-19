%module PyFDO

%include "wchar.i"
%include <python.swg>

//
//SWIG is ref-counting aware, so let's take advantage of it!
//

//Do not AddRef() as any pointer returned will either already be AddRef()'d at the C++ level or
//(if freshly allocated) will start off with a refcount of 1
%feature("ref") FdoIDisposable ""
//However, we still do need to release
%feature("unref") FdoIDisposable "FdoCleanup($this);"

//======= Python specific =======
%include "PythonTypemaps.i"

//======= Memory Management =========
%include "../Common/FdoMemory.i"

%{
#include <cstdint>
#include <vector>
#include "Fdo.h"

#include "utils.h"

#include "MemCheck.h"
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

static void FdoCleanup(FdoIDisposable* obj)
{
    if (NULL != obj)
    {
        FdoInt32 refCount = obj->Release();
        if (refCount <= 0)
            printf("Deleted\n");
        else
            printf("Un-refd(%d)\n", refCount);
    }

    //FDO_SAFE_RELEASE(obj);
}

%}

%include "../Common/MemCheck.h"
%include "../Common/FdoIgnore.i"
%include "../Common/FdoMarshal_Ignore.i"
%include "../Common/FdoIncludes.i"
%include "../Common/FdoMarshal.i"