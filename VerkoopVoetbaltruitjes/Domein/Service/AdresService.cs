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
    }
}
