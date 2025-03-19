using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Entiteti;
using PresentationLayer;

namespace EntitiesLayer
{
    public static class AppKontekst
    {
        public static Korisnik prijavljeniKorisnik { get; set; }
        public static List<Korisnik> korisnici { get; set; }
        public static List<Uzgojna_kultura> uzgojnaKulture { get; set; }
        public static List<Vrsta_prikljucka> vrstePrikljucaka { get; set; }
        public static List<Vrsta_stoke> vrsteStoke { get; set; }

        public static Vozilo vozilo { get; set; } = null;

        public static Prikljucak prikljucak { get; set; }

        public static MainWindow mainWindow { get; set; }

        public static List<Korisnik> privremenaListaZaKorisnikeRadnogNaloga { get; set; } = new List<Korisnik>();
        public static List<Stavka_radnog_naloga> privremenaListaZaStavkeRadnogNaloga { get; set; } = new List<Stavka_radnog_naloga>();
    }
}
