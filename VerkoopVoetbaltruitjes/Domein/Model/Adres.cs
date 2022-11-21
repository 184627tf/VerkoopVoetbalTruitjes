using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Model {
    public class Adres {
        private int? _id;
        public int? Id {
            get { return _id; }
            set { _id = value; }
        }

        private string _adres;
        public string Adresnaam { 
            get { return _adres; }
            set {
                if (value.Length < 5) throw new ArgumentException($"Set Adresnaam: het adres moet minstens 5 karakters lang zijn, maar was {value.Length} karakters lang.");
                _adres = value; 
            }
        }

        public Adres(int? id = null, string adres = "adres") {
            Id = id;
            Adresnaam = adres;
        }

        public override string ToString() {
            return Adresnaam;
        }
    }
}
