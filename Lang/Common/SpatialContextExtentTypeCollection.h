#ifndef FDO_SPATIAL_CONTEXT_EXTENT_TYPE_COLLECTION_H
#define FDO_SPATIAL_CONTEXT_EXTENT_TYPE_COLLECTION_H

#include "Fdo.h"
#include "BasicValueCollection.h"

class FdoSpatialContextExtentTypeCollection : public FdoBasicValueCollection<FdoSpatialContextExtentType>
{
protected:
    FdoSpatialContextExtentTypeCollection() : FdoBasicValueCollection<FdoSpatialContextExtentType>() { }
    virtual ~FdoSpatialContextExtentTypeCollection() { }
    virtual void Dispose() { delete this; }
public:
    static FdoSpatialContextExtentTypeCollection* Create()
    {
        return new FdoSpatialContextExtentTypeCollection();
    }
};

#endif
