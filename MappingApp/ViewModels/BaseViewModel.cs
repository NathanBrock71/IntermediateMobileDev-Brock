using MappingApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MappingApp.ViewModels
{

    public class BaseViewModel
    {
        protected JsonDbService _dbService;
        protected APIServices _apiService;
        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel()
        {
            _dbService = new JsonDbService();
            _apiService = new APIServices();
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
