using Domein.Interfaces;
using Domein.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Service {
    public class AdresService {
        private IAdresRepository _repository;

        public AdresService(IAdresRepository repository) {
            _repository = repository;
        }

        public IEnumerable<Adres> GeefAdressen() {
            return _repository.GeefAdressen();
        }

        public void UpdateAdres(Adres adres) {
            _repository.UpdateAdres(adres);
        }

        public void VerwijderAdres(Adres adres) {
            _repository.VerwijderAdres(adres.Id.Value);
        }

        public int VoegAdresToe(Adres adres) {
            return _repository.VoegAdresToe(adres);
        }
    }
}
