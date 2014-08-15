#ifndef FDO_SPATIAL_OPERATIONS_COLLECTION_H
#define FDO_SPATIAL_OPERATIONS_COLLECTION_H

#include "Fdo.h"
#include "BasicValueCollection.h"

class FdoSpatialOperationsCollection : public FdoBasicValueCollection<FdoSpatialOperations>
{
protected:
    FdoSpatialOperationsCollection() : FdoBasicValueCollection<FdoSpatialOperations>() { }
    virtual ~FdoSpatialOperationsCollection() { }
    virtual void Dispose() { delete this; }
public:
    static FdoSpatialOperationsCollection* Create()
    {
        return new FdoSpatialOperationsCollection();
    }
};

#endif
