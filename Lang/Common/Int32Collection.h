#ifndef FDO_INT32_COLLECTION_H
#define FDO_INT32_COLLECTION_H

#include "Fdo.h"
#include "BasicValueCollection.h"

class FdoInt32Collection : public FdoBasicValueCollection<FdoInt32>
{
protected:
    FdoInt32Collection() : FdoBasicValueCollection<FdoInt32>() { }
    virtual ~FdoInt32Collection() { }
    virtual void Dispose() { delete this; }
public:
    static FdoInt32Collection* Create()
    {
        return new FdoInt32Collection();
    }
};

#endif
