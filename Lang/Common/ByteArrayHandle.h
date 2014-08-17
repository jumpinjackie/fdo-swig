#ifndef FDO_BYTE_ARRAY_HANDLE_H
#define FDO_BYTE_ARRAY_HANDLE_H

#include "Fdo.h"

//
// FdoByteArrayHandle is a specialized box type around FdoArray<FdoByte> mainly
// used to simplify SWIG code generation
//
class FdoByteArrayHandle : public FdoIDisposable
{
public:
    static FdoByteArrayHandle* Create(FdoByteArray* array)
    {
        return new FdoByteArrayHandle(array);
    }
    
    FdoInt32 GetLength() { return m_array->GetCount(); }
    
    FdoByte At(FdoInt32 index) { return m_data[index]; }
    
    FdoByteArray* GetInternalArray() { return FDO_SAFE_ADDREF(m_array.p); }
    
protected:
    FdoByteArrayHandle(FdoByteArray* array)
    {
        m_array = FDO_SAFE_ADDREF(array);
        m_data = m_array->GetData();
    }

    virtual ~FdoByteArrayHandle()
    {
        m_data = NULL;
        m_array = NULL;
    }
    
    virtual void Dispose() { delete this; }
    
private:
    FdoByte* m_data;
    FdoPtr<FdoByteArray> m_array;
};

#endif