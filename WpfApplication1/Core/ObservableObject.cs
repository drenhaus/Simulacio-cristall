using System;
using System.ComponentModel;

namespace WpfApplication1.Core
{
    class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName])
    }
}
