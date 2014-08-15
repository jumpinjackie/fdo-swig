#ifndef FDO_DATA_TYPE_COLLECTION_H
#define FDO_DATA_TYPE_COLLECTION_H

#include "Fdo.h"
#include "BasicValueCollection.h"

class FdoDataTypeCollection : public FdoBasicValueCollection<FdoDataType>
{
protected:
    FdoDataTypeCollection() : FdoBasicValueCollection<FdoDataType>() { }
    virtual ~FdoDataTypeCollection() { }
    virtual void Dispose() { delete this; }
public:
    static FdoDataTypeCollection* Create()
    {
        return new FdoDataTypeCollection();
    }
};

#endif
