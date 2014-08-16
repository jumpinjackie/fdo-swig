#ifndef FDO_DOUBLE_COLLECTION_H
#define FDO_DOUBLE_COLLECTION_H

#include "Fdo.h"
#include "BasicValueCollection.h"

class FdoDoubleCollection : public FdoBasicValueCollection<double>
{
protected:
    FdoDoubleCollection() : FdoBasicValueCollection<double>() { }
    virtual ~FdoDoubleCollection() { }
    virtual void Dispose() { delete this; }
public:
    static FdoDoubleCollection* Create()
    {
        return new FdoDoubleCollection();
    }
};

#endif
