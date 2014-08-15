#ifndef FDO_DISTANCE_OPERATIONS_COLLECTION_H
#define FDO_DISTANCE_OPERATIONS_COLLECTION_H

#include "Fdo.h"
#include "BasicValueCollection.h"

class FdoDistanceOperationsCollection : public FdoBasicValueCollection<FdoDistanceOperations>
{
protected:
    FdoDistanceOperationsCollection() : FdoBasicValueCollection<FdoDistanceOperations>() { }
    virtual ~FdoDistanceOperationsCollection() { }
    virtual void Dispose() { delete this; }
public:
    static FdoDistanceOperationsCollection* Create()
    {
        return new FdoDistanceOperationsCollection();
    }
};

#endif
