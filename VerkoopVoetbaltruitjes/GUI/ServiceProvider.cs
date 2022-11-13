using Domein.Interfaces;
using Domein.Service;
using SQLserver.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI {
    public static class ServiceProvider {
        private static IKlantRepository klantRepository = new KlantRepository();
        public static KlantService klantService = new KlantService(klantRepository);
        private static IAdresRepository adresRepository = new AdresRepository();
        public static AdresService adresService = new AdresService(adresRepository);
    }
}
