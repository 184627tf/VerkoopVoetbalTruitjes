using Domein.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace GUI.ViewModels {
    public class KlantenViewModel : INotifyPropertyChanged {

        private ObservableCollection<KlantViewModel> _klanten;
        public IEnumerable<KlantViewModel> Klanten {
            get { return _klanten; }
            set { 
                _klanten = new ObservableCollection<KlantViewModel>(value); 
                _klanten.CollectionChanged += KlantenCollectionChanged;
                OnPropertyChanged(nameof(Klanten));
            }
        }
        private ObservableCollection<KlantViewModel> _klantenCopy;

        private string _idQuery;
        public string IdQuery {
            get { return _idQuery; }
            set { _idQuery = value; OnPropertyChanged(nameof(IdQuery)); }
        }

        private string _naamQuery;
        public string NaamQuery {
            get { return _naamQuery; }
            set { _naamQuery = value; OnPropertyChanged(nameof(NaamQuery)); }
        }

        private string _adresQuery;
        public string AdresQuery {
            get { return _adresQuery; }
            set { _adresQuery = value; OnPropertyChanged(nameof(AdresQuery)); }
        }

        public KlantenViewModel() {
            VernieuwKlantViewModels();
        }

        public void VernieuwKlantViewModels() {
            var adressen = AdressenViewModel.GeefAdresViewModels();
            ObservableCollection<KlantViewModel> klanten = new();
            foreach (Klant klant in ServiceProvider.klantService.GeefKlanten()) {
                var klantVM = new KlantViewModel(klant, adressen);
                klanten.Add(klantVM);
            }
            Klanten = klanten;
            _klantenCopy = new(_klanten);
        }

        private void KlantenCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:
                    foreach (KlantViewModel klantVM in e.NewItems) {
                        Adres adres = new Adres(adres: klantVM.Adres.Adres);
                        Klant klant = new Klant(klantVM.Id, klantVM.Naam, adres);
                        int id = ServiceProvider.klantService.VoegKlantToe(klant);
                        klantVM.Id = id;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (KlantViewModel klantVM in e.OldItems) {
                        Adres adres = new Adres(adres: klantVM.Adres.Adres);
                        Klant klant = new Klant(klantVM.Id, klantVM.Naam, adres);
                        ServiceProvider.klantService.VerwijderKlant(klant);
                    }
                    break;
            }
        }

        private void OnPropertyChanged(string propertyName) {
            switch (propertyName) {
                case (nameof(IdQuery)):
                case (nameof(NaamQuery)):
                case (nameof(AdresQuery)):
                    Klanten = _klantenCopy;
                    Func<KlantViewModel, bool> ZoekKlantenOpId = k => true;
                    Func<KlantViewModel, bool> ZoekKlantenOpNaam = k => true;
                    Func<KlantViewModel, bool> ZoekKlantenOpAdres = k => true;
                    if (!string.IsNullOrWhiteSpace(IdQuery)) ZoekKlantenOpId = k => k.Id.ToString().ToLower().Contains(IdQuery.ToLower());
                    if (!string.IsNullOrWhiteSpace(NaamQuery)) ZoekKlantenOpNaam = k => k.Naam.ToLower().Contains(NaamQuery.ToLower());
                    if (!string.IsNullOrWhiteSpace(AdresQuery)) ZoekKlantenOpAdres = k => k.Adres.Adres.ToLower().Contains(AdresQuery.ToLower());

                    Klanten = _klanten.Where(ZoekKlantenOpId)
                                      .Where(ZoekKlantenOpNaam)
                                      .Where(ZoekKlantenOpAdres)
                                      .ToList();
                    break;
            }
            NotifyPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(String info) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }
}
