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
    public class KlantViewModel : INotifyPropertyChanged {
        private Klant _klant;

        public int? Id {
            get { return _klant.Id; }
            set { _klant.Id = value; }
        }
        public string Naam {
            get { return _klant.Naam; }
            set { _klant.Naam = value; OnPropertyChanged(nameof(Naam)); }
        }
        public AdresViewModel? Adres {
            get { return new AdresViewModel(_klant.Adres); }
            set { _klant.Adres = new Adres(value.Adres); OnPropertyChanged(nameof(Adres)); }
        }

        private ObservableCollection<AdresViewModel> _adressen;

        public IEnumerable<AdresViewModel> Adressen => _adressen;

        public KlantViewModel() : this(new Klant(adres: new Adres()), null) { }
        public KlantViewModel(Klant klant, ObservableCollection<AdresViewModel> adressen) {
            _klant = klant;

            if (adressen != null) {
                _adressen = adressen;
            } else {
                _adressen = AdressenViewModel.GeefAdresViewModels();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) {
            Adres adres = new Adres(this.Adres.Adres);
            Klant klant = new Klant(this.Id, this.Naam, adres);
            ServiceProvider.klantService.UpdateKlant(klant);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
