using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Models
{
    public class SilosModel
    {
        public int Id { get; set; }
        public int PopunjenostPostotak { get; set; }
        public string FarmaLokacija { get; set; }
        public string UzgojnaKultura { get; set; }
        public string Dostupnost { get; set; }

        public SilosModel(Silos silos, string farmaLokacija, string uzgojnaKultura)
        {
            Id = silos.Silos_id;
            PopunjenostPostotak = IzracunajPostotak(silos.Popunjenost, silos.Kapacitet);
            FarmaLokacija = farmaLokacija;
            UzgojnaKultura = uzgojnaKultura;
            Dostupnost = silos.Dostupnost == 1 ? "Dostupno" : "Nedostupno";
        }

        private int IzracunajPostotak(double popunjenost, double kapacitet)
        {
            if (kapacitet == 0) return 0;
            double postotak = (popunjenost / kapacitet) * 100;
            return (int)postotak;
        }
    }
}
