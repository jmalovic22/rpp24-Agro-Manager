using DataAccessLayer.Repozitoriji;
using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ServisPrikljucak
    {
        private RepoPrikljucak repo;

        public ServisPrikljucak()
        {
            repo = new RepoPrikljucak();
        }

        public List<Prikljucak> DohvatiSvePrikljucke()
        {
            return repo.DohvatiSve().ToList();
        }

        public List<Prikljucak> DohvatiPrikljuckePoduzeca(int idPoduzeca)
        {
            return repo.DohvatiPrikljuckePoduzeca(idPoduzeca).ToList();
        }

        public Prikljucak DohvatiPrikljucakPoId(int idPrikljucka)
        {
            return repo.DohvatiPrikljucakPoId(idPrikljucka);
        }

        public bool DodajPrikljucak(Prikljucak prikljucakZaDodavnaje)
        {
            bool uspjesnoDodavanjePrikljucka = false;
            
            if (prikljucakZaDodavnaje == null)
            {
                throw new ArgumentException("Priključak za dodavanje nije unesen");
            }

            PrikljucakIspravniPodaciDodaj(prikljucakZaDodavnaje);

            int farmaDodana = repo.Dodaj(prikljucakZaDodavnaje);
            uspjesnoDodavanjePrikljucka = farmaDodana > 0;

            if (uspjesnoDodavanjePrikljucka == false)
            {
                throw new ArgumentException("Dodavanje priključka nije uspjelo");
            }

            return uspjesnoDodavanjePrikljucka;
        }

        public bool AzurirajPrikljucak(Prikljucak prikljucakZaAzuriranje)
        {
            bool usjesnoAzuriranjePrikljucka = false;

            if (prikljucakZaAzuriranje == null)
            {
                throw new ArgumentException("Priključak za ažuriranje ne postoji");
            }

            PrikljucakIspravniPodaciAzuriraj(prikljucakZaAzuriranje);

            int prikljucakAzuriran = repo.Azuriraj(prikljucakZaAzuriranje);
            usjesnoAzuriranjePrikljucka = prikljucakAzuriran > 0;

            if (usjesnoAzuriranjePrikljucka == false)
            {
                throw new ArgumentException("Ažuriranje priključka nije uspjelo");
            }

            return usjesnoAzuriranjePrikljucka;
        }

        public bool IzbrisiPrikljucak(int idPrikljucka)
        {
            bool uspjesnoBrisanjePrikljucka = false;

            var prikljucakZaBrisanje = repo.DohvatiPrikljucakPoId(idPrikljucka);

            if (prikljucakZaBrisanje == null)
            {
                throw new ArgumentException("Priključak za brisanje nije pronađen");
            }

            int prikljucakIzbrisan = repo.Izbrisi(prikljucakZaBrisanje);
            uspjesnoBrisanjePrikljucka = prikljucakIzbrisan > 0;

            if (uspjesnoBrisanjePrikljucka == false)
            {
                throw new ArgumentException("Brisanje priključka nije uspjelo");
            }

            return uspjesnoBrisanjePrikljucka;
        }

        private void PrikljucakIspravniPodaciDodaj(Prikljucak prikljucakZaProvjeru, string porukaOGresci = "")
        {
            var registracija = DohvatiSvePrikljucke().Exists(prikljucak => prikljucak.Registracija == prikljucakZaProvjeru.Registracija);

            if (registracija == true)
            {
                porukaOGresci += "U bazi već postoji priključak s tom registracijom" + Environment.NewLine;
            }

            PrikljucakIspravniPodaciAzuriraj(prikljucakZaProvjeru);
        }

        private void PrikljucakIspravniPodaciAzuriraj(Prikljucak prikljucakZaProvjeru, string porukaOGresci = "")
        {
            if (string.IsNullOrWhiteSpace(prikljucakZaProvjeru.Marka))
            {
                porukaOGresci += "Marka priključka ne smije biti prazna" + Environment.NewLine;
            }

            if (porukaOGresci != "")
            {
                throw new ArgumentException(porukaOGresci);
            }
        }
    }
}
