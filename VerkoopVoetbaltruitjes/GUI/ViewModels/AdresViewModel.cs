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

        public string Adres {
            get { return _adres.Adresnaam; }
            set { _adres.Adresnaam = value; }
        }
        public AdresViewModel() : this(new Adres()) { }
        public AdresViewModel(Adres adres) {
            _adres = adres;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString() {
            return Adres;
        }
    }
}
