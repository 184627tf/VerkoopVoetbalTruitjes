using Domein.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Interfaces {
    public interface IKlantRepository {
        public int VoegKlantToe(Klant klant);
        public IEnumerable<Klant> GeefKlanten();
        public void UpdateKlant(Klant klant);
        public void VerwijderKlant(Klant klant);
        public bool Exists(Klant klant);
    }
}
