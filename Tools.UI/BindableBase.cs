using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Tools.UI
{
    public abstract class BindableBase : INotifyPropertyChanged, IDisposable
    {
        private SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (_synchronizationContext != null)
            {
                _synchronizationContext.Post((s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)), null);               
            }
            else
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual void Dispose() { }
    }
}
