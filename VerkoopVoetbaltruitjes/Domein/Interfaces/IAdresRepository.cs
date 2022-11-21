using Domein.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Interfaces {
    public interface IAdresRepository {
        public IEnumerable<Adres> GeefAdressen();
        public void UpdateAdres(Adres adres);
        public void VerwijderAdres(int id);
        public int VoegAdresToe(Adres adres);
    }
}
