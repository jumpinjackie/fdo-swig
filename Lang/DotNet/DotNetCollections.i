%define IMPLEMENT_LIST(collection_type, item_type)
//Necessary imports
%typemap(csimports) collection_type %{
using System.Collections;
using System.Collections.Generic;
%}
//Collection Interfaces implemented by the implementing proxy class
%typemap(csinterfaces_derived) collection_type "IList<item_type>"
//This is the IList<T> implementation that is injected into the implementing proxy class
%typemap(cscode) collection_type %{
    int IList<item_type>.IndexOf(item_type item)
    {
        return this.IndexOf(item);
    }
    
    void IList<item_type>.Insert(int index, item_type item)
    {
        this.Insert(index, item);
    }
    
    void IList<item_type>.RemoveAt(int index)
    {
        this.RemoveAt(index);
    }
    
    item_type IList<item_type>.this[int index]
    {
        get { return this.GetItem(index); }
        set { this.SetItem(index, value); }
    }
    
    void ICollection<item_type>.Add(item_type item)
    {
        this.Add(item);
    }
    
    void ICollection<item_type>.Clear()
    {
        this.Clear();
    }
    
    bool ICollection<item_type>.Contains(item_type item)
    {
        return this.Contains(item);
    }
    
    void ICollection<item_type>.CopyTo(item_type[] array, int arrayIndex)
    {
        throw new global::System.NotImplementedException();
    }
    
    bool ICollection<item_type>.Remove(item_type item)
    {
        int count = this.Count;
        this.Remove(item);
        return this.Count < count;
    }
    
    int ICollection<item_type>.Count
    {
        get { return this.Count; }
    }
    
    bool ICollection<item_type>.IsReadOnly
    {
        get { return false; }
    }
    
    class CollectionEnumerator : IEnumerator<item_type>
    {
        private IList<item_type> _list;
        private int _position;
        private int _count;
        
        public CollectionEnumerator(IList<item_type> list)
        {
            _list = list;
            _count = list.Count;
            _position = -1;
        }
        
        bool IEnumerator.MoveNext()
        {
            _position++;
            return _position < _count;
        }
        
        void IEnumerator.Reset()
        {
            _position = -1;
        }
        
        object IEnumerator.Current
        {
            get { return _list[_position]; }
        }
        
        item_type IEnumerator<item_type>.Current
        {
            get { return _list[_position]; }
        }
        
        public void Dispose() { }
    }
    
    IEnumerator<item_type> IEnumerable<item_type>.GetEnumerator()
    {
        return new CollectionEnumerator(this);
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return new CollectionEnumerator(this);
    }
%}
%enddef
%define IMPLEMENT_READONLY_LIST(collection_type, item_type)
//Necessary imports
%typemap(csimports) collection_type %{
using System.Collections;
using System.Collections.Generic;
%}
//Collection Interfaces implemented by the implementing proxy class
%typemap(csinterfaces_derived) collection_type "IList<item_type>"
//This is the IList<T> implementation that is injected into the implementing proxy class
%typemap(cscode) collection_type %{
    int IList<item_type>.IndexOf(item_type item)
    {
        return this.IndexOf(item);
    }
    
    void IList<item_type>.Insert(int index, item_type item)
    {
        throw new global::System.InvalidOperationException();
    }
    
    void IList<item_type>.RemoveAt(int index)
    {
        throw new global::System.InvalidOperationException();
    }
    
    item_type IList<item_type>.this[int index]
    {
        get { return this.GetItem(index); }
        set { throw new global::System.InvalidOperationException(); }
    }
    
    void ICollection<item_type>.Add(item_type item)
    {
        throw new global::System.InvalidOperationException();
    }
    
    void ICollection<item_type>.Clear()
    {
        throw new global::System.InvalidOperationException();
    }
    
    bool ICollection<item_type>.Contains(item_type item)
    {
        return this.Contains(item);
    }
    
    void ICollection<item_type>.CopyTo(item_type[] array, int arrayIndex)
    {
        throw new global::System.NotImplementedException();
    }
    
    bool ICollection<item_type>.Remove(item_type item)
    {
        throw new global::System.InvalidOperationException();
    }
    
    int ICollection<item_type>.Count
    {
        get { return this.Count; }
    }
    
    bool ICollection<item_type>.IsReadOnly
    {
        get { return true; }
    }
    
    class CollectionEnumerator : IEnumerator<item_type>
    {
        private IList<item_type> _list;
        private int _position;
        private int _count;
        
        public CollectionEnumerator(IList<item_type> list)
        {
            _list = list;
            _count = list.Count;
            _position = -1;
        }
        
        bool IEnumerator.MoveNext()
        {
            _position++;
            return _position < _count;
        }
        
        void IEnumerator.Reset()
        {
            _position = -1;
        }
        
        object IEnumerator.Current
        {
            get { return _list[_position]; }
        }
        
        item_type IEnumerator<item_type>.Current
        {
            get { return _list[_position]; }
        }
        
        public void Dispose() { }
    }
    
    IEnumerator<item_type> IEnumerable<item_type>.GetEnumerator()
    {
        return new CollectionEnumerator(this);
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return new CollectionEnumerator(this);
    }
%}
%enddef

IMPLEMENT_LIST(FdoArgumentDefinitionCollection, FdoArgumentDefinition)
IMPLEMENT_LIST(FdoBatchParameterValueCollection, FdoParameterValueCollection)
IMPLEMENT_LIST(FdoClassCollection, FdoClassDefinition)
IMPLEMENT_LIST(FdoCurvePolygonCollection, FdoICurvePolygon)
IMPLEMENT_LIST(FdoCurveStringCollection, FdoICurveString)
IMPLEMENT_LIST(FdoDataPropertyDefinitionCollection, FdoDataPropertyDefinition)
IMPLEMENT_LIST(FdoDataValueCollection, FdoDataValue)
IMPLEMENT_LIST(FdoDirectPositionCollection, FdoIDirectPosition)
IMPLEMENT_LIST(FdoExpressionCollection, FdoExpression)
IMPLEMENT_LIST(FdoFeatureClassCollection, FdoFeatureClass)
IMPLEMENT_LIST(FdoFeatureSchemaCollection, FdoFeatureSchema)
//IMPLEMENT_LIST(FdoFunctionDefinitionCollection, FdoFunctionDefinition)
IMPLEMENT_LIST(FdoIdentifierCollection, FdoIdentifier)
IMPLEMENT_LIST(FdoJoinCriteriaCollection, FdoJoinCriteria)
IMPLEMENT_LIST(FdoLinearRingCollection, FdoILinearRing)
IMPLEMENT_LIST(FdoLineStringCollection, FdoILineString)
IMPLEMENT_LIST(FdoParameterValueCollection, FdoParameterValue)
IMPLEMENT_LIST(FdoPhysicalSchemaMappingCollection, FdoPhysicalSchemaMapping)
IMPLEMENT_LIST(FdoPropertyDefinitionCollection, FdoPropertyDefinition)
IMPLEMENT_LIST(FdoRingCollection, FdoIRing)
IMPLEMENT_LIST(FdoSignatureDefinitionCollection, FdoSignatureDefinition)

IMPLEMENT_READONLY_LIST(FdoReadOnlyArgumentDefinitionCollection, FdoArgumentDefinition)
IMPLEMENT_READONLY_LIST(FdoReadOnlyDataPropertyDefinitionCollection, FdoDataPropertyDefinition)
IMPLEMENT_READONLY_LIST(FdoReadOnlyPropertyDefinitionCollection, FdoPropertyDefinition)
IMPLEMENT_READONLY_LIST(FdoReadOnlySignatureDefinitionCollection, FdoSignatureDefinition)