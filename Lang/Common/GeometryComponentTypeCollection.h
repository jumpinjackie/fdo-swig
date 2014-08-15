#ifndef FDO_GEOMETRY_COMPONENT_TYPE_COLLECTION_H
#define FDO_GEOMETRY_COMPONENT_TYPE_COLLECTION_H

#include "Fdo.h"
#include "BasicValueCollection.h"

class FdoGeometryComponentTypeCollection : public FdoBasicValueCollection<FdoGeometryComponentType>
{
protected:
    FdoGeometryComponentTypeCollection() : FdoBasicValueCollection<FdoGeometryComponentType>() { }
    virtual ~FdoGeometryComponentTypeCollection() { }
    virtual void Dispose() { delete this; }
public:
    static FdoGeometryComponentTypeCollection* Create()
    {
        return new FdoGeometryComponentTypeCollection();
    }
};

#endif
