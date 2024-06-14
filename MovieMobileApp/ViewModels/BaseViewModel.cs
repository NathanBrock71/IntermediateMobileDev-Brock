using MovieMobileApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CoffeeAppSpring2024inclass.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected readonly APICommunicationService _apiServices;

        public BaseViewModel()
        {
            _apiServices = new APICommunicationService();
        }

        bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                SetProperty(ref _isBusy, value);
            }
        }

       
        protected bool SetProperty<T>(ref T backingStore, T value, 
            [CallerMemberName] string propertyName = "", 
            Action? onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


    }
}
