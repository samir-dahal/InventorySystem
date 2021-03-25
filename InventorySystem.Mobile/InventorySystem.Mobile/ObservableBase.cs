using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace InventorySystem.Mobile
{
    public class ObservableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }
        private bool _isValidationError;

        public bool IsValidationError
        {
            get { return _isValidationError; }
            set { _isValidationError = value; OnPropertyChanged(nameof(IsValidationError)); }
        }
        private string _validationErrors;

        public string ValidationErrors
        {
            get { return _validationErrors; }
            set { _validationErrors = value; OnPropertyChanged(nameof(ValidationErrors)); }
        }
        public ICommand InputChangedCommand => new Command(() => IsValidationError = false);
    }
}
