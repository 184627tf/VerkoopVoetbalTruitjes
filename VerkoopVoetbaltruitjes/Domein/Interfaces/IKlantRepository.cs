using Domein.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Interfaces {
    public interface IKlantRepository {
        public IEnumerable<Klant> GeefKlanten();
    }
}
