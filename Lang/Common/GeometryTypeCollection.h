#ifndef FDO_GEOMETRY_TYPE_COLLECTION_H
#define FDO_GEOMETRY_TYPE_COLLECTION_H

#include "Fdo.h"
#include "BasicValueCollection.h"

class FdoGeometryTypeCollection : public FdoBasicValueCollection<FdoGeometryType>
{
protected:
    FdoGeometryTypeCollection() : FdoBasicValueCollection<FdoGeometryType>() { }
    virtual ~FdoGeometryTypeCollection() { }
    virtual void Dispose() { delete this; }
public:
    static FdoGeometryTypeCollection* Create()
    {
        return new FdoGeometryTypeCollection();
    }
};

#endif
