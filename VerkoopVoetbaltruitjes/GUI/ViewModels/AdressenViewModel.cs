using Domein.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace GUI.ViewModels {
    public class AdressenViewModel : INotifyPropertyChanged {
        private ObservableCollection<AdresViewModel> _adressen;
        public IEnumerable<AdresViewModel> Adressen {
            get { return _adressen; }
            set {
                _adressen = new ObservableCollection<AdresViewModel>(value);
                _adressen.CollectionChanged += AdressenCollectionChanged;
                OnPropertyChanged(nameof(Adressen));
            }
        }
        private ObservableCollection<AdresViewModel> _adressenCopy;

        private string _idQuery;
        public string IdQuery {
            get { return _idQuery; }
            set { _idQuery = value; OnPropertyChanged(nameof(IdQuery)); }
        }
        private string _adresQuery;
        public string AdresQuery {
            get { return _adresQuery; }
            set { _adresQuery = value; OnPropertyChanged(nameof(AdresQuery)); }
        }


        public AdressenViewModel() {
            VernieuwAdresViewModels();
        }

        public static ObservableCollection<AdresViewModel> GeefAdresViewModels() {
            ObservableCollection<AdresViewModel> adressen = new ObservableCollection<AdresViewModel>();
            foreach (Adres adres in ServiceProvider.adresService.GeefAdressen()) {
                var adresVM = new AdresViewModel(adres);
                adressen.Add(adresVM);
            }
            return adressen;
        }

        private void VernieuwAdresViewModels() {
            Adressen = GeefAdresViewModels();
            _adressenCopy = new(_adressen);
        }

        private void AdressenCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:
                    foreach (AdresViewModel adresVM in e.NewItems) {
                        Adres adres = new Adres(adres: adresVM.Adres);
                        int id = ServiceProvider.adresService.VoegAdresToe(adres);
                        adresVM.Id = id;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (AdresViewModel adresVM in e.OldItems) {
                        Adres adres = new Adres(adresVM.Id, adresVM.Adres);
                        try {
                            ServiceProvider.adresService.VerwijderAdres(adres);
                        } catch {
                            MessageBox.Show("Kan dit adres niet verwijderen want er is nog een klant die dit adres gebruikt!");
                        }
                    }
                    break;
            }
        }

        private void OnPropertyChanged(string propertyName) {
            switch (propertyName) {
                case (nameof(IdQuery)):
                case (nameof(AdresQuery)):
                    Adressen = _adressenCopy;
                    Func<AdresViewModel, bool> ZoekAdressenOpId = a => true;
                    Func<AdresViewModel, bool> ZoekAdressenOpAdres = a => true;
                    if (!string.IsNullOrWhiteSpace(IdQuery)) ZoekAdressenOpId = a => a.Id.ToString().ToLower().Contains(IdQuery.ToLower());
                    if (!string.IsNullOrWhiteSpace(AdresQuery)) ZoekAdressenOpAdres = a => a.Adres.ToLower().Contains(AdresQuery.ToLower());

                    Adressen = _adressen.Where(ZoekAdressenOpId)
                                      .Where(ZoekAdressenOpAdres)
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
