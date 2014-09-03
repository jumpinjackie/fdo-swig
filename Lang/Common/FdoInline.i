%{
//Define this value to activate refcounting debugging code
//#define DEBUG_FDO_REFCOUNTING
//Define this value to print class types in FdoIDisposable pointers via RTTI
//#define HAS_RTTI
#include <cstdint>
#include <vector>
#include "Fdo.h"
#ifdef HAS_RTTI
#include <typeinfo.h>
#endif
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

#ifdef DEBUG_FDO_REFCOUNTING
static void FdoLogRefCount(FdoIDisposable* obj)
{
    char sAddress[80];
    if (NULL != obj)
    {
        sprintf(sAddress, "%x", obj);
        printf("Refcount of %s", sAddress);
#ifdef HAS_RTTI
        printf(" (%s): ", typeid(*obj).name());
#else
        printf(": ");
#endif
        printf("%d\n", obj->GetRefCount());
    }
}
#else
#define FdoLogRefCount(obj)
#endif

static void FdoCleanup(FdoIDisposable* obj)
{
#ifdef DEBUG_FDO_REFCOUNTING
    char sAddress[80];
    if (NULL != obj)
    {
        sprintf(sAddress, "%x", obj);
        printf("Releasing %s", sAddress);
#ifdef HAS_RTTI
        printf(" (%s): ", typeid(*obj).name());
#else
        printf(": ");
#endif
        FdoInt32 refCount = obj->Release();
        if (refCount <= 0)
            printf("Deleted\n");
        else
            printf("Un-refd (new refcount: %d)\n", refCount);
    }
#else
    FDO_SAFE_RELEASE(obj);
#endif
}

%}