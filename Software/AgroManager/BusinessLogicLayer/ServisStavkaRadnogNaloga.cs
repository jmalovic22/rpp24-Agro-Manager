using DataAccessLayer.Repozitoriji;
using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ServisStavkaRadnogNaloga
    {
        private RepoStavkaRadnogNaloga repo;

        public ServisStavkaRadnogNaloga()
        {
            repo = new RepoStavkaRadnogNaloga();
        }

        public List<Stavka_radnog_naloga> DohvatiStavkeRadnogNaloga(int idRadnogNaloga)
        {
            return repo.DohvatiStavkeRadnogNaloga(idRadnogNaloga).ToList();
        }

        public Stavka_radnog_naloga DohvatiStavkuRadnogNalogaPoId(int idStavkeRadnogNaloga)
        {
            return repo.DohvatiStavkuRadnogNalogaPoId(idStavkeRadnogNaloga);
        }

        public bool DodajStavkuRadnogNaloga(Stavka_radnog_naloga stavkaRadnogNalogaZaDodavnaje)
        {
            bool uspjesnoDodavanjeStavkeRadnogNaloga = false;

            if (stavkaRadnogNalogaZaDodavnaje == null)
            {
                throw new ArgumentException("Stavka radnog naloga nije unesena");
            }

            ProvjeraUnesenihPodatakaStavkeRadnogNaloga(stavkaRadnogNalogaZaDodavnaje);

            int stavkaRadnogNalogaDodana = repo.Dodaj(stavkaRadnogNalogaZaDodavnaje);
            uspjesnoDodavanjeStavkeRadnogNaloga = stavkaRadnogNalogaDodana > 0;

            if (uspjesnoDodavanjeStavkeRadnogNaloga == false)
            {
                throw new ArgumentException("Dodavanje stavke radnog naloga nije uspjelo");
            }

            return uspjesnoDodavanjeStavkeRadnogNaloga;
        }

        private void ProvjeraUnesenihPodatakaStavkeRadnogNaloga(Stavka_radnog_naloga stavkaRadnogNalogaZaProvjeru, string porukaOGresci = "")
        {
            
            if (stavkaRadnogNalogaZaProvjeru.Status == null)
            {
                porukaOGresci += ("Status stavke radnog naloga nije unesen");
            }

            if (stavkaRadnogNalogaZaProvjeru.Razina_prioriteta <= 0 || stavkaRadnogNalogaZaProvjeru.Razina_prioriteta > 3)
            {
                porukaOGresci += ("Razina prioriteta stavke radnog naloga ne smije biti manji od 0 ili veci 3. ");
            }

            if (stavkaRadnogNalogaZaProvjeru.Status.Length > 20)
            {
                porukaOGresci += ("Status stavke radnog naloga ne smije biti duži od 20 znakova");
            }

            if (stavkaRadnogNalogaZaProvjeru.Napomena.Length > 100)
            {
                porukaOGresci += ("Napomena stavke radnog naloga ne smije biti duža od 100 znakova");
            }

            if (porukaOGresci != "")
            {
                throw new ArgumentException(porukaOGresci);
            }

        }

        public bool AzurirajStavkuRadnogNaloga(Stavka_radnog_naloga stavkaRadnogNalogaZaAzuriranje)
        {
            bool usjesnoAzuriranjeStavkeRadnogNaloga = false;

            ProvjeraUnesenihPodatakaStavkeRadnogNaloga(stavkaRadnogNalogaZaAzuriranje);

            int stavkaRadnogNalogaAzurirana = repo.Azuriraj(stavkaRadnogNalogaZaAzuriranje);
            usjesnoAzuriranjeStavkeRadnogNaloga = stavkaRadnogNalogaAzurirana > 0;

            if (usjesnoAzuriranjeStavkeRadnogNaloga == false)
            {
                throw new ArgumentException("Ažuriranje stavke radnog naloga nije uspjelo");
            }

            return usjesnoAzuriranjeStavkeRadnogNaloga;
        }

        public bool ObrisiStavkuRadnogNaloga(int idStavkeRadnogNaloga)
        {
            bool uspjesnoBrisanjeStavkeRadnogNaloga = false;

            var stavkaRadnogNalogaZaBrisanje = repo.DohvatiStavkuRadnogNalogaPoId(idStavkeRadnogNaloga);

            if (stavkaRadnogNalogaZaBrisanje == null)
            {
                throw new ArgumentException("Stavka radnog naloga ne postoji");
            }

            int stavkaRadnogNalogaIzbrisana = repo.Izbrisi(stavkaRadnogNalogaZaBrisanje);
            uspjesnoBrisanjeStavkeRadnogNaloga = stavkaRadnogNalogaIzbrisana > 0;

            if (uspjesnoBrisanjeStavkeRadnogNaloga == false)
            {
                throw new ArgumentException("Brisanje stavke radnog naloga nije uspjelo");
            }

            return uspjesnoBrisanjeStavkeRadnogNaloga;
        }
    }
}
