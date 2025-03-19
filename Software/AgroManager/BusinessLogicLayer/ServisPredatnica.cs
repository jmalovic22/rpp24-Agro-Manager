using DataAccessLayer.Repozitoriji;
using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ServisPredatnica
    {
        private RepoPredatnica repo;

        public ServisPredatnica()
        {
            repo = new RepoPredatnica();
        }

        public List<Predatnica> DohvatiSvePredatniceSilosa(int idSilosa)
        {
            var svePredatniceSilosa = repo.DohvatiPredatniceSilosa(idSilosa).ToList();
            return svePredatniceSilosa;
        }

        public List<Predatnica> DohvatiSvePredatniceKorisnika(string oibKorisnika)
        {
            var svePredatniceKorisnika = repo.DohvatiPredatniceKorisnika(oibKorisnika).ToList();
            return svePredatniceKorisnika;
        }

        public async Task<bool> DodajPredatnicu(Predatnica predatnicaZaDodavanje)
        {
            bool uspjesnoDodavanjePredatnice = false;

            if (predatnicaZaDodavanje == null)
            {
                throw new ArgumentException("Predatnica za dodavanje nije unesena");
            }

            if (predatnicaZaDodavanje.Silos == null)
            {
                var taskSilos = new ServisSilos().DohvatiSilosPremaIdAsync(predatnicaZaDodavanje.Silos_id);
                var silosZaDodavanje = await taskSilos;
                if (silosZaDodavanje == null)
                {
                    throw new ArgumentException("Predatnica ne sadrži vezu na silos");
                }
                predatnicaZaDodavanje.Silos = silosZaDodavanje;
            }

            PredatnicaProvjeraUnesenihPodataka(predatnicaZaDodavanje);

            int predatnicaDodana = repo.Dodaj(predatnicaZaDodavanje);
            uspjesnoDodavanjePredatnice = predatnicaDodana > 0;

            //upitan dio koda jer vec postoji trigger u bazi podataka koji obavlja ovaj dio
            /*
            if (uspjesnoDodavanjePredatnice)
            {
                using (var repoSilos = new RepoSilos())
                {
                    var idSilosaZaAzuriranjePopunjenosti = predatnicaZaDodavanje.Silos.Silos_id.ToString();
                    var silosZaAzuriranjePopunjenosti = repoSilos.DohvatiSilosPoId(idSilosaZaAzuriranjePopunjenosti);

                    if (silosZaAzuriranjePopunjenosti != null)
                    {
                        silosZaAzuriranjePopunjenosti.Popunjenost -= predatnicaZaDodavanje.Kolicina_u_kg;
                        repoSilos.Azuriraj(silosZaAzuriranjePopunjenosti);
                    }
                    else
                    {
                        throw new Exception("Silos s tim Id-ijem ne postoji unutar baze podataka");
                    }
                }
            }*/

            return uspjesnoDodavanjePredatnice;
        }

        private void PredatnicaProvjeraUnesenihPodataka(Predatnica predatnicaZaProvjeru, string porukaOGresci = "")
        {
            if (predatnicaZaProvjeru.Datum == null)
            {
                porukaOGresci += "Datum nije unesen" + Environment.NewLine;
            }

            if (predatnicaZaProvjeru.Kolicina_u_kg <= 0)
            {
                porukaOGresci += "Količina nije ispravno unesena" + Environment.NewLine;
            }

            if (predatnicaZaProvjeru.Silos.Dostupnost == 0)
            {
                porukaOGresci += "Silos nije dostupan za punjenje" + Environment.NewLine;
            }
            else if ((predatnicaZaProvjeru.Kolicina_u_kg + predatnicaZaProvjeru.Silos.Popunjenost) > predatnicaZaProvjeru.Silos.Kapacitet)
            {
                porukaOGresci += "Količina je veća od kapaciteta silosa" + Environment.NewLine;
            }

            if (predatnicaZaProvjeru.Silos_id <= 0)
            {
                porukaOGresci += "Silos nije ispravno unesen" + Environment.NewLine;
            }

            if (predatnicaZaProvjeru.Registracija_vozila == null)
            {
                porukaOGresci += "Registracija vozila nije unesena" + Environment.NewLine;
            }

            if (predatnicaZaProvjeru.Korisnik_OIB == null)
            {
                porukaOGresci += "OIB korisnika nije unesen" + Environment.NewLine;
            }

            if (predatnicaZaProvjeru.Registracija_vozila == null)
            {
                porukaOGresci += "Registracija vozila nije unesena" + Environment.NewLine;
            }

            if (porukaOGresci != "")
            {
                throw new ArgumentException(porukaOGresci);
            }


            //return true;
            /*
             mozda metodu treba vratiti u bool kako bi se true ili false vratio u metodu za dodavanje
             predatnice i onda se bi/ne bi ulazilo u dio gdje se stvara kontekst tj veza na entitetni model
             */
        }
    }
}
