using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Model {
    public class Klant {
        private int? _id;
        public int? Id { 
            get { return _id; }
            set { 
                if (value.HasValue) {
                    if (value <= 0) throw new Exception($"Set ID: ID mag niet kleiner of gelijk aan 0 zijn, maar was {value.Value}.");
                    _id = value.Value;
                } else {
                    _id = null;
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

        public Klant(int? id = null, string naam = "naam", Adres adres = null) {
            Id = id;
            Naam = naam;
            Adres = adres;
        }
    }
}
