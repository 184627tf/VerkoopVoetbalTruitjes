using Domein.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModels {
    public class AdresViewModel : INotifyPropertyChanged {
        private Adres _adres;

        public int? Id {
            get { return _adres.Id; }
            set { _adres.Id = value; }
        }

        public string Adres {
            get { return _adres.Adresnaam; }
            set { _adres.Adresnaam = value; OnPropertyChanged(nameof(Adres)); }
        }
        public AdresViewModel() : this(new Adres()) { }
        public AdresViewModel(Adres adres) {
            _adres = adres;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(String info) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        protected void OnPropertyChanged(string propertyName) {
            switch (propertyName) {
                case (nameof(Id)):
                case (nameof(Adres)):
                    Adres adres = new Adres(this.Id, this.Adres);
                    ServiceProvider.adresService.UpdateAdres(adres);
                    break;
            }
            NotifyPropertyChanged(propertyName);
        }

        public override string ToString() {
            return Adres;
        }
    }
}
