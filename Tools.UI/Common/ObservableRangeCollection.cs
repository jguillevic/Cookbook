﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Tools.UI.Common
{
    public class ObservableRangeCollection<T> : ObservableCollection<T>
    {
        public ObservableRangeCollection()
            : base() { }

        public ObservableRangeCollection(IEnumerable<T> collection)
            : base(collection) { }

        public void AddRange(IEnumerable<T> collection)
        {
            foreach (var i in collection)
                Items.Add(i);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        
        public void RemoveRange(IEnumerable<T> collection)
        {
            foreach (var i in collection)
                Items.Remove(i);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        
        public void Replace(T item)
        {
            ReplaceRange(new T[] { item });
        }
        
        public void ReplaceRange(IEnumerable<T> collection)
        {
            Items.Clear();
            foreach (var i in collection)
                Items.Add(i);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
