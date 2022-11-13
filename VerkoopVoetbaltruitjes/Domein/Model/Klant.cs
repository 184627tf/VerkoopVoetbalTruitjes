using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Model {
    public class Klant {
        private int? _klantNummer;
        public int? KlantNummer { 
            get { return _klantNummer; }
            set { 
                if (value.HasValue) {
                    if (value <= 0) throw new Exception($"Set ID: ID mag niet kleiner of gelijk aan 0 zijn, maar was {value.Value}.");
                    _klantNummer = value.Value;
                } else {
                    _klantNummer = null;
                }
            }
        }

        private string _naam;
        public string Naam {
            get { return _naam; }
            set {
                if (string.IsNullOrWhiteSpace(value)) throw new Exception($"Set Naam: naam mag niet leeg of whitespace zijn.");
                _naam = value;
            }
        }

        public Adres Adres { get; set; }

        public Klant(int? klantenNummer = null, string naam = "naam", Adres adres = null) {
            KlantNummer = klantenNummer;
            Naam = naam;
            Adres = adres;
        }
    }
}
