using Domein.Interfaces;
using Domein.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Service {
    public class KlantService {
        private IKlantRepository _repository;

        public KlantService(IKlantRepository repository) {
            _repository = repository;
        }

        public IEnumerable<Klant> GeefKlanten() {
            return _repository.GeefKlanten();
        }

        public int VoegKlantToe(Klant klant) {
            return _repository.VoegKlantToe(klant);
        }

        public void VerwijderKlant(Klant klant) {
            _repository.VerwijderKlant(klant);
        }

    }
}
