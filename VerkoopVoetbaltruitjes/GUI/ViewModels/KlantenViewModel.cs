using Domein;
using Domein.Interfaces;
using Domein.Model;
using Domein.Service;
using GUI.Commands;
using SQLserver.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI.ViewModels {
    public class KlantenViewModel : INotifyPropertyChanged {

        private ObservableCollection<KlantViewModel> _klanten;

        public IEnumerable<KlantViewModel> Klanten => _klanten;

        public KlantenViewModel() {
            var adressen = AdressenViewModel.GeefAdresViewModels();
            _klanten = GeefKlantViewModels(adressen);
            _klanten.CollectionChanged += KlantenCollectionChanged;
        }

        private void KlantenCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:
                    foreach (KlantViewModel klantVM in e.NewItems) {
                        Adres adres = new Adres(klantVM.Adres.Adres);
                        Klant klant = new Klant(klantVM.Id, klantVM.Naam, adres);
                        int id = ServiceProvider.klantService.VoegKlantToe(klant);
                        klantVM.Id = id;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (KlantViewModel klantVM in e.OldItems) {
                        Adres adres = new Adres(klantVM.Adres.Adres);
                        Klant klant = new Klant(klantVM.Id, klantVM.Naam, adres);
                        ServiceProvider.klantService.VerwijderKlant(klant);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    // Doe niets??
                    break;
            }
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
