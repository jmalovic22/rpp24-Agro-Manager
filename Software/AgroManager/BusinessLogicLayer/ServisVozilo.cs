using DataAccessLayer.Repozitoriji;
using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ServisVozilo
    {
        private RepoVozilo repo;

        public ServisVozilo()
        {
            repo = new RepoVozilo();
        }

        public List<Vozilo> DohvatiSvaVozila()
        {
            return repo.DohvatiSve().ToList();
        }

        public List<Vozilo> DohvatiVozilaTrazenogPoduzeca(int idPoduzeca)
        {
            return repo.DohvatiVozilaPoduzeca(idPoduzeca).ToList();
        }

        public Vozilo DohvatiVoziloPremaRegistraciji(string registracijaVozila)
        {
            return repo.DohvatiVoziloPoRegistraciji(registracijaVozila);
        }

        public bool DodajVozilo(Vozilo voziloZaDodavanje)
        {
            bool uspjesnoDodavanjeVozila = false;

            if (voziloZaDodavanje == null)
            {
                throw new ArgumentException("Vozilo za dodavanje nije pronađeno");
            }

            VoziloIspravniPodaciDodaj(voziloZaDodavanje);

            int voziloDodano = repo.Dodaj(voziloZaDodavanje);
            uspjesnoDodavanjeVozila = voziloDodano > 0;

            if (uspjesnoDodavanjeVozila == false)
            {
                throw new ArgumentException("Dodavanje vozila nije uspjelo");
            }

            return uspjesnoDodavanjeVozila;
        }

        public bool AzurirajVozilo(Vozilo voziloZaAzuriranje)
        {
            bool uspjesnoAzuriranjeVozila = false;

            if (voziloZaAzuriranje == null)
            {
                throw new ArgumentException("Vozilo za ažuriranje nije pronađeno");
            }

            VoziloIspravniPodaciAzuriraj(voziloZaAzuriranje);

            int voziloAzurirano = repo.Azuriraj(voziloZaAzuriranje);
            uspjesnoAzuriranjeVozila = voziloAzurirano > 0;

            if (uspjesnoAzuriranjeVozila == false)
            {
                throw new ArgumentException("Ažuriranje vozila nije uspjelo");
            }

            return uspjesnoAzuriranjeVozila;
        }

        public bool IzbrisiVozilo(string registracijaVozila)
        {
            bool uspjesnoBrisanjeVozila = false;

            var voziloZaBrisanje = repo.DohvatiVoziloPoRegistraciji(registracijaVozila);

            if (voziloZaBrisanje == null)
            {
                throw new ArgumentException("Vozilo za brisanje nije pronađeno");
            }

            int voziloIzbrisano = repo.Izbrisi(voziloZaBrisanje);
            uspjesnoBrisanjeVozila = voziloIzbrisano > 0;

            if (uspjesnoBrisanjeVozila == false)
            {
                throw new ArgumentException("Brisanje vozila nije uspjelo");
            }

            return uspjesnoBrisanjeVozila;
        }

        private void VoziloIspravniPodaciDodaj(Vozilo voziloZaProvjeru, string porukaOGresci = "")
        {
            var registracija = DohvatiSvaVozila().Exists(vozilo => vozilo.Registracija == voziloZaProvjeru.Registracija);

            if (registracija == true)
            {
                porukaOGresci += "U bazi već postoji vozilo s tom registracijom" + Environment.NewLine;
            };

            VoziloIspravniPodaciAzuriraj(voziloZaProvjeru, porukaOGresci);
        }

        private void VoziloIspravniPodaciAzuriraj(Vozilo voziloZaProvjeru, string porukaOGresci = "")
        {
            if (string.IsNullOrWhiteSpace(voziloZaProvjeru.Registracija))
            {
                porukaOGresci += "Registracija vozila ne smije biti prazna" + Environment.NewLine;
            }

            if (string.IsNullOrWhiteSpace(voziloZaProvjeru.Vrsta))
            {
                porukaOGresci += "Vrsta vozila ne smije biti prazna" + Environment.NewLine;
            }

            if (string.IsNullOrWhiteSpace(voziloZaProvjeru.Marka))
            {
                porukaOGresci += "Marka vozila ne smije biti prazna" + Environment.NewLine;
            }

            if (voziloZaProvjeru.Jacina_motora_u_KS <= 0)
            {
                porukaOGresci += "Jačina motora mora biti veća od 0" + Environment.NewLine;
            }

            if (string.IsNullOrWhiteSpace(voziloZaProvjeru.Opis))
            {
                porukaOGresci += "Opis vozila ne smije biti prazan" + Environment.NewLine;
            }

            if (porukaOGresci != "")
            {
                throw new ArgumentException(porukaOGresci);
            }
        }
    }
}
