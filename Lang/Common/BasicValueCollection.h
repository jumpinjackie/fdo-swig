#ifndef FDO_BASIC_VALUE_COLLECTION_H
#define FDO_BASIC_VALUE_COLLECTION_H

#include <Common/IDisposable.h>

template <class VAL> class FdoBasicValueCollection : public FdoIDisposable
{
protected:
    FdoBasicValueCollection() { }
    
    virtual ~FdoBasicValueCollection()
    {
        Clear();
    }
    
public:
    virtual FdoInt32 GetCount() const { return m_list.size(); }
    
    virtual VAL GetItem(FdoInt32 index) const
    {
        if (index >= m_list.size() || index < 0)
            throw FdoException::Create(FdoException::NLSGetMessage(FDO_NLSID(FDO_5_INDEXOUTOFBOUNDS)));

        return m_list[index];
    }
    
    virtual void Add(VAL value)
    {
        m_list.push_back(value);
    }
    
    virtual void Clear()
    {
        m_list.clear();
    }
    
private:
    std::vector<VAL> m_list;
};

#endif