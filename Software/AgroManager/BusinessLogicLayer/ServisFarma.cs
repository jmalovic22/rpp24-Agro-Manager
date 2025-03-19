using DataAccessLayer.Repozitoriji;
using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ServisFarma
    {
        private RepoFarma repo;

        public ServisFarma()
        {
            repo = new RepoFarma();
        }

        //sto se tice funkcionalnosti same aplikacije najvjerojatnije nepotrebna metoda
        public async Task<List<Farma>> DohvatiSveFarmeAsync()
        {
            var farme = repo.DohvatiSveAsync();
            return await farme;
        }

        public async Task<List<Farma>> DohvatiFarmeTrazenogPoduzecaAsync(int idPoduzeca)
        {
            var farmePoduzeca = await repo.DohvatiFarmePoduzecaAsync(idPoduzeca);
            return farmePoduzeca;
        }

        public async Task<Farma> DohvatiFarmuPoIdAsync(int idFarme)
        {
            var farmaPoduzeca = await repo.DohvatiFarmuPremaIdAsync(idFarme);
            return farmaPoduzeca;
        }

        public async Task<bool> DodajFarmuAsync(Farma farmaZaDodavanje)
        {
            bool uspjesnoDodavanjeFarme = false;

            if (farmaZaDodavanje == null)
            {
                throw new ArgumentException("Niste unijeli farma za dodavanje");
            }

            await FarmaIspravniPodaciAsync(farmaZaDodavanje);

            int farmaDodana = repo.Dodaj(farmaZaDodavanje);
            uspjesnoDodavanjeFarme = farmaDodana > 0;

            if (uspjesnoDodavanjeFarme == false)
            {
                throw new ArgumentException("Nastala je greska pri dodavanju farme");
            }

            return uspjesnoDodavanjeFarme;
        }

        
        public async Task<bool> AzurirajFarmuAsync(Farma farmaZaAzuriranje)
        {
            bool uspjesnoAzuriranjeFarme = false;

            if (farmaZaAzuriranje == null)
            {
                throw new ArgumentException("Niste unijeli farma za ažuriranje");
            }

            await FarmaIspravniPodaciAsync(farmaZaAzuriranje);

            int farmaAzurirana = repo.Azuriraj(farmaZaAzuriranje);
            uspjesnoAzuriranjeFarme = farmaAzurirana > 0;

            if (uspjesnoAzuriranjeFarme == false)
            {
                throw new ArgumentException("Nastala je greska pri ažuriranju farme");
            }

            return uspjesnoAzuriranjeFarme;
        }

        public async Task<bool> IzbrisiFarmuAsync(int idFarme)
        {
            bool uspjesnoBrisanjeFarme = false;
            
            var farmaZaBrisanje = await repo.DohvatiFarmuPremaIdAsync(idFarme);
             
            if (farmaZaBrisanje != null)
            {
                int farmaIzbrisana = repo.Izbrisi(farmaZaBrisanje);
                uspjesnoBrisanjeFarme = farmaIzbrisana > 0;
            }
            else
            {
                throw new ArgumentException("Odabrana farma ne postoji");
            }

            if (uspjesnoBrisanjeFarme == false)
            {
                throw new ArgumentException("Nastala je greska pri brisanju farme");
            }

            return uspjesnoBrisanjeFarme;
        }

        private async Task FarmaIspravniPodaciAsync(Farma farmaZaProvjeru, string porukaOGresci = "")
        {

            var sveFarme = await DohvatiSveFarmeAsync();
            var lokacija = sveFarme.Exists(farma => farma.Lokacija == farmaZaProvjeru.Lokacija && farma.Farma_id != farmaZaProvjeru.Farma_id);

            if (lokacija == true)
            {
                porukaOGresci += "Na toj lokaciji već postoji farma" + Environment.NewLine;
            };

            if (string.IsNullOrWhiteSpace(farmaZaProvjeru.Lokacija))
            {
                porukaOGresci += "Niste unijeli lokaciju farme" + Environment.NewLine;
            };
            
            if (farmaZaProvjeru.Povrsina <= 0)
            {
                porukaOGresci += "Površina farme ne smije biti manja od 0" + Environment.NewLine;
            };

            if (farmaZaProvjeru.Broj_zaposlenih <= 0)
            {
                porukaOGresci += "Broj zaposlenih ne smije biti 0 ili manje" + Environment.NewLine;
            };

            if (porukaOGresci != "")
            {
                throw new ArgumentException(porukaOGresci);
            }

        }

    }
}
