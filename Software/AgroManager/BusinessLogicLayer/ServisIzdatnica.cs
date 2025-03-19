using DataAccessLayer.Repozitoriji;
using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ServisIzdatnica
    {
        private RepoIzdatnica repo;
        public ServisIzdatnica()
        {
            repo = new RepoIzdatnica();
        }

        public List<Izdatnica> DohvatiSveIzdatniceSilosa(int idSilosa)
        {
            var sveIzdatniceSilosa = repo.DohvatiIzdatniceSilosa(idSilosa).ToList();
            return sveIzdatniceSilosa;
        }

        public List<Izdatnica> DohvatiSveIzdatniceKorisnika(string oibKorisnika)
        {
            var sveIzdatniceKorisnika = repo.DohvatiIzdatniceKorisnika(oibKorisnika).ToList();
            return sveIzdatniceKorisnika;
        }

        public async Task<bool> DodajIzdatnicu(Izdatnica izdatnicaZaDodavanje)
        {
            bool uspjesnoDodavanjeIzdatnice = false;

            if (izdatnicaZaDodavanje == null)
            {
                throw new ArgumentException("Izdatnica za dodavanje nije unesena");
            }

            if (izdatnicaZaDodavanje.Silos == null)
            {
                var taskSilos = new ServisSilos().DohvatiSilosPremaIdAsync(izdatnicaZaDodavanje.Silos_id);
                var silosZaDodavanje = await taskSilos;
                if (silosZaDodavanje == null)
                {
                    throw new ArgumentException("Izdatnica ne sadrži vezu na silos");
                }
                izdatnicaZaDodavanje.Silos = silosZaDodavanje;
            }

            IzdatnicaProvjeraUnesenihPodataka(izdatnicaZaDodavanje);

            int izdatnicaDodana = repo.Dodaj(izdatnicaZaDodavanje);
            uspjesnoDodavanjeIzdatnice = izdatnicaDodana > 0;

            //upitan dio koda jer vec postoji trigger u bazi podataka koji obavlja ovaj dio
            /*
            if (uspjesnoDodavanjeIzdatnice)
            {
                using (var repoSilos = new RepoSilos())
                {
                    var idSilosaZaAzuriranjePopunjenosti = izdatnicaZaDodavanje.Silos.Silos_id.ToString();
                    var silosZaAzuriranjePopunjenosti = repoSilos.DohvatiSilosPoId(idSilosaZaAzuriranjePopunjenosti);

                    if (silosZaAzuriranjePopunjenosti != null)
                    {
                        silosZaAzuriranjePopunjenosti.Popunjenost -= izdatnicaZaDodavanje.Kolicina_u_kg;
                        repoSilos.Azuriraj(silosZaAzuriranjePopunjenosti);
                    }
                    else
                    {
                        throw new Exception("Silos s tim Id-ijem ne postoji unutar baze podataka");
                    }
                }
            }*/

            return uspjesnoDodavanjeIzdatnice;
        }

        private void IzdatnicaProvjeraUnesenihPodataka(Izdatnica izdatnicaZaProvjeru, string porukaOGresci = "")
        {
            if (izdatnicaZaProvjeru.Datum == null)
            {
                porukaOGresci += "Datum nije unesen" + Environment.NewLine;
            }

            if (izdatnicaZaProvjeru.Kolicina_u_kg <= 0)
            {
                porukaOGresci += "Količina nije ispravno unesena" + Environment.NewLine;
            }

            if (izdatnicaZaProvjeru.Silos.Dostupnost == 0)
            {
                porukaOGresci += "Silos nije dostupan za izdavanje" + Environment.NewLine;
            }
            else if (izdatnicaZaProvjeru.Kolicina_u_kg > izdatnicaZaProvjeru.Silos.Popunjenost)
            {
                porukaOGresci += "Količina je veća od popunjenosti silosa" + Environment.NewLine;
            }

            if (izdatnicaZaProvjeru.Silos_id <= 0)
            {
                porukaOGresci += "Silos nije ispravno unesen" + Environment.NewLine;
            }

            if (izdatnicaZaProvjeru.Registracija_vozila == null)
            {
                porukaOGresci += "Registracija vozila nije unesena" + Environment.NewLine;
            }

            if (izdatnicaZaProvjeru.Korisnik_OIB == null)
            {
                porukaOGresci += "OIB korisnika nije unesen" + Environment.NewLine;
            }

            if (izdatnicaZaProvjeru.Registracija_vozila == null)
            {
                porukaOGresci += "Registracija vozila nije unesena" + Environment.NewLine;
            }

            if (porukaOGresci != "")
            {
                throw new ArgumentException(porukaOGresci);
            }
        }
    }
}
