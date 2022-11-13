using Domein.Interfaces;
using Domein.Model;
using Domein.Service;
using SQLserver.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModels {
    public class KlantenViewModel : INotifyPropertyChanged {

        private ObservableCollection<KlantViewModel> _klanten;

        public IEnumerable<KlantViewModel> Klanten => _klanten;

        public KlantenViewModel() {
            var adressen = AdressenViewModel.GeefAdresViewModels();
            _klanten = GeefKlantViewModels(adressen);
        }

        public static ObservableCollection<KlantViewModel> GeefKlantViewModels(ObservableCollection<AdresViewModel> adressen) {
            ObservableCollection<KlantViewModel> klanten = new();
            foreach (Klant klant in ServiceProvider.klantService.GeefKlanten()) {
                var klantVM = new KlantViewModel(klant, adressen);
                klanten.Add(klantVM);
            }
            return klanten;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
