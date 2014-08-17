#ifndef FDO_MEM_CHECK_H
#define FDO_MEM_CHECK_H

#define USE_VLD

#ifdef USE_VLD
#ifdef _WIN32
#include <vld.h>
#endif

class FdoMemCheck
{
public:
    static void EnableMemoryLeakChecking(long iBlock = -1) { }
    static void DumpMemoryLeakResults() { }
};

#else

#ifdef _WIN32
#define _CRTDBG_MAP_ALLOC
#include <stdlib.h>
#include <crtdbg.h>
#endif

class FdoMemCheck
{
public:
    static void EnableMemoryLeakChecking(long iBlock = -1)
    {
#ifdef _WIN32
        _CrtSetDbgFlag( _CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF );
    #ifdef _DEBUG
        // to debug memory leaks, set a breakpoint here and set iBlock
        // to the block allocation you want to break on
        _CrtSetBreakAlloc(iBlock);
    #endif
#endif
    }

    static void DumpMemoryLeakResults()
    {
#ifdef _WIN32
        _CrtDumpMemoryLeaks();
#endif
    }
};

#endif

#endif