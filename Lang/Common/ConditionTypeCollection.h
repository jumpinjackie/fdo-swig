#ifndef FDO_CONDITION_TYPE_COLLECTION_H
#define FDO_CONDITION_TYPE_COLLECTION_H

#include "Fdo.h"
#include "BasicValueCollection.h"

class FdoConditionTypeCollection : public FdoBasicValueCollection<FdoConditionType>
{
protected:
    FdoConditionTypeCollection() : FdoBasicValueCollection<FdoConditionType>() { }
    virtual ~FdoConditionTypeCollection() { }
    virtual void Dispose() { delete this; }
public:
    static FdoConditionTypeCollection* Create()
    {
        return new FdoConditionTypeCollection();
    }
};

#endif
