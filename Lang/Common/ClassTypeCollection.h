#ifndef FDO_CLASS_TYPE_COLLECTION_H
#define FDO_CLASS_TYPE_COLLECTION_H

#include "Fdo.h"
#include "BasicValueCollection.h"

class FdoClassTypeCollection : public FdoBasicValueCollection<FdoClassType>
{
protected:
    FdoClassTypeCollection() : FdoBasicValueCollection<FdoClassType>() { }
    virtual ~FdoClassTypeCollection() { }
    virtual void Dispose() { delete this; }
public:
    static FdoClassTypeCollection* Create()
    {
        return new FdoClassTypeCollection();
    }
};

#endif
