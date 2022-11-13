using Domein.Interfaces;
using Domein.Model;
using Domein.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModels {
    public class AdressenViewModel : INotifyPropertyChanged {
        private ObservableCollection<AdresViewModel> _adressen;

        public IEnumerable<AdresViewModel> Adressen => _adressen;

        public AdressenViewModel() {
            _adressen = GeefAdresViewModels();
        }

        public static ObservableCollection<AdresViewModel> GeefAdresViewModels() {
            ObservableCollection<AdresViewModel> adressen = new ObservableCollection<AdresViewModel>();
            foreach (Adres adres in ServiceProvider.adresService.GeefAdressen()) {
                var adresVM = new AdresViewModel(adres);
                adressen.Add(adresVM);
            }
            return adressen;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
