#ifndef FDO_EXPRESSION_TYPE_COLLECTION_H
#define FDO_EXPRESSION_TYPE_COLLECTION_H

#include "Fdo.h"
#include "BasicValueCollection.h"

class FdoExpressionTypeCollection : public FdoBasicValueCollection<FdoExpressionType>
{
protected:
    FdoExpressionTypeCollection() : FdoBasicValueCollection<FdoExpressionType>() { }
    virtual ~FdoExpressionTypeCollection() { }
    virtual void Dispose() { delete this; }
public:
    static FdoExpressionTypeCollection* Create()
    {
        return new FdoExpressionTypeCollection();
    }
};

#endif
