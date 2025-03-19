using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repozitoriji;
using EntitiesLayer.Entiteti;

namespace BusinessLogicLayer
{
    public class ServisPolje
    {
        private RepoPolje repo;

        public ServisPolje() 
        {
            repo = new RepoPolje();
        }

        public List<Polje> DohvatiPolja()
        {
            return repo.DohvatiPolja().ToList();
        }

        public Polje DohvatiPolje(int idPolje)
        {
            return repo.DohvatiPoljePoId(idPolje);
        }

        public List<Polje> DohvatiPoljaPoduzeca(int idPoduzeca)
        {
            return repo.DohvatiPoljaPoduzeca(idPoduzeca).ToList();
        }

        public bool DodajPolje(Polje poljeZaDodavanje)
        {
            bool uspjesnoDodavanjePolja = false;

            if (poljeZaDodavanje == null)
            {
                throw new ArgumentException("Polje za dodavanje nije pronađeno");
            }

            //PosaoIspravniPodaciDodavanja(posaoZaDodavanje);

            int poljeDodano = repo.Dodaj(poljeZaDodavanje);
            uspjesnoDodavanjePolja = poljeDodano > 0;

            if (uspjesnoDodavanjePolja == false)
            {
                throw new ArgumentException("Dodavanje polja nije uspjelo");
            }

            return uspjesnoDodavanjePolja;
        }

        public bool AzurirajPolje(Polje poljeZaAzuriranje)
        {
            bool uspjesnoAzuriranjePolja = false;

            if (poljeZaAzuriranje == null)
            {
                throw new ArgumentException("Polje za ažuriranje nije pronađeno");
            }

            //PosaoIspravniPodaciDodavanja(poljeZaAzuriranje);

            int poljeAzurirano = repo.Azuriraj(poljeZaAzuriranje);
            uspjesnoAzuriranjePolja = poljeAzurirano > 0;

            if (uspjesnoAzuriranjePolja == false)
            {
                throw new ArgumentException("Ažuriranje polja nije uspjelo");
            }

            return uspjesnoAzuriranjePolja;
        }

        public bool IzbrisiPolje(int idPolja)
        {
            bool uspjesnoBrisanjePolja = false;

            var poljeZaBrisanje = repo.DohvatiPoljePoId(idPolja);

            if (poljeZaBrisanje == null)
            {
                throw new ArgumentException("Polje za brisanje nije pronađeno");
            }

            int poljeIzbrisano = repo.Izbrisi(poljeZaBrisanje);
            uspjesnoBrisanjePolja = poljeIzbrisano > 0;

            if (uspjesnoBrisanjePolja == false)
            {
                throw new ArgumentException("Brisanje polja nije uspjelo");
            }

            return uspjesnoBrisanjePolja;
        }
    }
}
