using DataAccessLayer.Repozitoriji;
using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ServisSilos
    {
        private RepoSilos RepoSilos;

        public ServisSilos()
        {
            RepoSilos = new RepoSilos();
        }

        public async Task<List<Silos>> DohvatiSveSiloseAsync()
        {
            var silosi = await RepoSilos.DohvatiSveAsync();
            return silosi;
        }

        public async Task<List<Silos>> DohvatiSiloseNekeFarmeAsync(int idFarme)
        {
            var silosi = await RepoSilos.DohvatiSiloseFarmeAsync(idFarme);
            return silosi;
        }

        public async Task<List<Silos>> DohvatiSiloseNekogPoduzecaAsync(int idPoduzeca)
        {
                var silosi = await RepoSilos.DohvatiSilosePoduzecaAsync(idPoduzeca);
                return silosi;
        }

        //trenutno beskorisna metoda ostaviti cu ju ako bude potrebna u buducnosti zbog neke druge funkcionalnosti
        public async Task<Silos> DohvatiSilosPremaIdAsync(int idSilosa)
        {
            var silos = await RepoSilos.DohvatiSilosPoIdAsync(idSilosa);
            return silos;
        }

        public bool DodajSilosFarmi(Silos silosZaDodavanje)
        {
            bool uspjesnoDodavanjeSilosa = false;

            if (silosZaDodavanje == null)
            {
                throw new ArgumentException("Niste unijeli silos za dodavanje");
            }

            SilosDodavanjeIspravniPodaci(silosZaDodavanje);

            int silosDodan = RepoSilos.Dodaj(silosZaDodavanje);
            uspjesnoDodavanjeSilosa = silosDodan > 0;

            if (uspjesnoDodavanjeSilosa == false)
            {
                throw new ArgumentException("Nastala je greska pri dodavanju silosa");
            }

            return uspjesnoDodavanjeSilosa;
        }

        public bool AzurirajPodatkeSilosa(Silos silosZaAzuriranje)
        {
            bool uspjesnoAzuriranje = false;

            if (silosZaAzuriranje == null)
            {
                throw new ArgumentException("Niste unijeli silos za ažuriranje");
            }

            SilosAzuriranjeIspravniPodaci(silosZaAzuriranje);

            int silosAzuriran = RepoSilos.Azuriraj(silosZaAzuriranje);
            uspjesnoAzuriranje = silosAzuriran > 0;

            if (uspjesnoAzuriranje == false)
            {
                throw new ArgumentException("Nastala je greska pri ažuriranju silosa");
            }

            return uspjesnoAzuriranje;
        }

        public async Task<bool> IzbrisiSilosAsync(int idSilosa)
        {
            bool uspjesnoBrisanjeSilosa = false;

                var silosZaBrisanje = await RepoSilos.DohvatiSilosPoIdAsync(idSilosa);

                if (silosZaBrisanje != null)
                {
                    int silosIzbrisan = RepoSilos.Izbrisi(silosZaBrisanje);
                    uspjesnoBrisanjeSilosa = silosIzbrisan > 0;
                }
                else
                {
                    throw new ArgumentException("Odabrani silos ne postoji");
                }

            if (uspjesnoBrisanjeSilosa == false)
            {
                throw new ArgumentException("Nastala je greska pri brisanju silosa");
            }

            return uspjesnoBrisanjeSilosa;
        }

        private bool SilosDodavanjeIspravniPodaci(Silos silosZaProvjeru, string porukaOGresci = "")
        {
            if (silosZaProvjeru.Popunjenost != 0)
            {
                porukaOGresci += "Popunjenost silosa pri kreiranju moze biti samo 0" + Environment.NewLine;
            };
            return SilosAzuriranjeIspravniPodaci(silosZaProvjeru, porukaOGresci);
        }

        private bool SilosAzuriranjeIspravniPodaci(Silos silosZaProvjeru, string porukaOGresci = "")
        {
            if (silosZaProvjeru.Kapacitet <= 0)
            {
                porukaOGresci += "Niste unijeli kapacitet silosa" + Environment.NewLine;
            };

            if (porukaOGresci != "")
            {
                throw new ArgumentException(porukaOGresci);
            }

            return true;
        }
    }
}
