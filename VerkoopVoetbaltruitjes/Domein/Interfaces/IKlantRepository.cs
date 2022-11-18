using Domein.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Interfaces {
    public interface IKlantRepository {
        public bool Exists(Klant klant);
        public IEnumerable<Klant> GeefKlanten();
        public void VerwijderKlant(Klant klant);
        public int VoegKlantToe(Klant klant);
    }
}
