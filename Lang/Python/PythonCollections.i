// PythonCollections.i
//
// Magic methods to allow our FdoCollection-derived proxy classes to act more like python lists
//
// TODO: string key support for FdoNamedCollection. How to make __getitem__ and __setitem__ accept string or int keys?
// TODO: __iter__ support

%extend FdoCollection {
    size_t __len__() { return $self->GetCount(); }
    OBJ* __getitem__(FdoInt32 i) { return $self->GetItem(i); }
    void __setitem__(FdoInt32 i, OBJ* item)
    {
        if (i < $self->GetCount())
            $self->SetItem(i, item);
        else
            $self->Insert(i, item);
    }
    void __delitem__(FdoInt32 i) { $self->RemoveAt(i); }
    bool __contains__(OBJ* item) { return $self->Contains(item); }
};