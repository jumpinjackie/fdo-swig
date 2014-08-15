#ifndef FDO_LOCK_TYPE_COLLECTION_H
#define FDO_LOCK_TYPE_COLLECTION_H

#include "Fdo.h"
#include "BasicValueCollection.h"

class FdoLockTypeCollection : public FdoBasicValueCollection<FdoLockType>
{
protected:
    FdoLockTypeCollection() : FdoBasicValueCollection<FdoLockType>() { }
    virtual ~FdoLockTypeCollection() { }
    virtual void Dispose() { delete this; }
public:
    static FdoLockTypeCollection* Create()
    {
        return new FdoLockTypeCollection();
    }
};

#endif
