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
%include "PythonCasts.i"

//======= Memory Management =========
%include "../Common/FdoMemory.i"

%{
#include <cstdint>
#include <vector>
#include "Fdo.h"

#include "utils.h"
#include "StringBuffer.h"

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

// PyObjects representing Fdo exceptions
PyObject *pFdoException;
PyObject *pFdoCommandException;
PyObject *pFdoConnectionException;
PyObject *pFdoExpressionException;
PyObject *pFdoFilterException;
PyObject *pFdoSchemaException;
PyObject *pFdoXmlException;
PyObject *pFdoSpatialContextMismatchException;
// Add any new FDO exception pointers here.

%}

/* Handle operator overloading for all classes*/

%rename(__divide__) *::operator/; 
%rename(__multiply__) *::operator*; 
%rename(__subtract__) *::operator-; 
%rename(__add__) *::operator+; 
%rename(__assign__) *::operator=; 
%rename(__equals__) *::operator==; 
%rename(__notequals__) *::operator!=;

/* -------------------------------------------------------------
 * Wrapper initialization
 *	Description:	These commands are inserted during the initialization
 *					of the wrapper.  The FdoException classes are
 *					reFdotered here, and single pointers to the exceptions
 *					are created.
 * -------------------------------------------------------------
 */
 
%init %{
    // Custom initialization code from FdoExceptions.i
    // Create and initialize Python exception pointers used to represent
    // FDO pointers 
    pFdoException = PyErr_NewException( "FDOw.FdoException", NULL, NULL );
    if (pFdoException != NULL ) {		
        PyDict_SetItemString( d, "FdoException", pFdoException );
    }

    pFdoException = PyErr_NewException( "FDOw.FdoException", NULL, NULL );
    if (pFdoException != NULL ) {		
        PyDict_SetItemString( d, "FdoException", pFdoException );
    }	

    pFdoCommandException = PyErr_NewException( "FDOw.FdoCommandException", NULL, NULL );
    if (pFdoCommandException != NULL ) {
        PyDict_SetItemString( d, "FdoCommandException", pFdoCommandException );
    }

    pFdoConnectionException = PyErr_NewException( "FDOw.FdoConnectionException", NULL, NULL );
    if (pFdoConnectionException != NULL ) {
        PyDict_SetItemString( d, "FdoConnectionException", pFdoConnectionException );
    }

    pFdoExpressionException = PyErr_NewException( "FDOw.FdoExpressionException", NULL, NULL );
    if (pFdoExpressionException != NULL ) {
        PyDict_SetItemString( d, "FdoExpressionException", pFdoExpressionException );
    }

    pFdoFilterException = PyErr_NewException( "FDOw.FdoFilterException", NULL, NULL );
    if (pFdoFilterException != NULL ) {
        PyDict_SetItemString( d, "FdoFilterException", pFdoFilterException );
    }

    pFdoSchemaException = PyErr_NewException( "FDOw.FdoSchemaException", NULL, NULL );
    if (pFdoSchemaException != NULL ) {
        PyDict_SetItemString( d, "FdoSchemaException", pFdoSchemaException );
    }

    pFdoXmlException = PyErr_NewException( "FDOw.FdoXmlException", NULL, NULL );
    if (pFdoXmlException != NULL ) {
        PyDict_SetItemString( d, "FdoXmlException", pFdoXmlException );
    }

    pFdoSpatialContextMismatchException = PyErr_NewException( "FDOw.FdoSpatialContextMismatchException", NULL, NULL );
    if (pFdoSpatialContextMismatchException != NULL ) {
        PyDict_SetItemString( d, "FdoSpatialContextMismatchException", pFdoSpatialContextMismatchException );
    }
    // End custom initialization code from Main.i
%}

%include "FdoExceptions.i"
%include "../Common/MemCheck.h"
%include "../Common/FdoIgnore.i"
%include "../Common/FdoMarshal_Ignore.i"
%include "../Common/FdoIncludes.i"
%include "../Common/FdoCollections.i"
%include "../Common/FdoMarshal.i"