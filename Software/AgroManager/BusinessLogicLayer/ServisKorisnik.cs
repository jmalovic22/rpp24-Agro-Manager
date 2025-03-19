using DataAccessLayer.Repozitoriji;
using EntitiesLayer.Enkodiranje;
using EntitiesLayer.Entiteti;
using EntitiesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ServisKorisnik
    {
        private RepoKorisnik repo;

        public ServisKorisnik()
        {
            repo = new RepoKorisnik();
        }

        public ResursiPoduzeca DohvatiResursePoduzeca(int idPoduzeca)
        {
            ResursiPoduzeca resursi = repo.DohvatiResurse(idPoduzeca);
            return resursi;
        }

        public List<Korisnik> DohvatiSveKorisnike()
        {
            List<Korisnik> korisnici = repo.DohvatiSve().ToList();
            return korisnici;
        }

        public List<Korisnik> DohvatiKorisnikePoduzeca(int idPoduzeca)
        {
            List<Korisnik> korisnici = repo.DohvatiKorisnikePoduzeca(idPoduzeca).ToList();
            return korisnici;
        }

        public Korisnik PrijavaKorisnika(string korime, string lozinka)
        {
            var sifriranaLozinka = Enkodiranje.ComputeSha256Hash(lozinka + Enkodiranje.sol);

            Korisnik korisnik = repo.DohvatiSve()
                .SingleOrDefault(k => k.Korisnicko_ime == korime && k.Lozinka == sifriranaLozinka);

            return korisnik;
        }

        public bool DodajKorisnika(Korisnik korisnikZaDodavanje)
        {
            bool uspjesnoDodavanjeKorisnika = false;

            if (korisnikZaDodavanje == null)
            {
                throw new ArgumentException("Nije proslijeden korisnik za dodavanje");
            }

            ProvjeraPodatakaKorisnika(korisnikZaDodavanje);

            int korisnikDodan = repo.Dodaj(korisnikZaDodavanje);
            uspjesnoDodavanjeKorisnika = korisnikDodan > 0;

            if (uspjesnoDodavanjeKorisnika == false)
            {
                throw new ArgumentException("Dodavanje korisnika nije uspjelo");
            }

            return uspjesnoDodavanjeKorisnika;
        }

        public bool AzurirajKorisnika(Korisnik korisnikZaAzuriranje)
        {
            bool uspjesnoAzuriranjeKorisnika = false;

            if (korisnikZaAzuriranje == null)
            {
                throw new ArgumentException("Nije proslijeden korisnik za ažuriranje");
            }

            ProvjeraPodatakaKorisnika(korisnikZaAzuriranje);

            int korisnikAzuriran = repo.Azuriraj(korisnikZaAzuriranje);
            uspjesnoAzuriranjeKorisnika = korisnikAzuriran > 0;

            if (uspjesnoAzuriranjeKorisnika == false)
            {
                throw new ArgumentException("Ažuriranje korisnika nije uspjelo");
            }

            return uspjesnoAzuriranjeKorisnika;
        }

        public bool IzbrisiKorisnika(string korisnikaOIB)
        {
            bool uspjesnoBrisanjeKorisnika = false;

            var korisnikZaBrisanje = repo.DohvatiSve().SingleOrDefault(k => k.Korisnik_OIB == korisnikaOIB);

            if (korisnikZaBrisanje == null)
            {
                throw new ArgumentException("Korisnik nije pronađen");
            }

            int korisnikIzbrisan = repo.Izbrisi(korisnikZaBrisanje);
            uspjesnoBrisanjeKorisnika = korisnikIzbrisan > 0;

            if (uspjesnoBrisanjeKorisnika == false)
            {
                throw new ArgumentException("Brisanje korisnika nije uspjelo");
            }

            return uspjesnoBrisanjeKorisnika;
        }

        private void ProvjeraPodatakaKorisnika(Korisnik korisnikZaProvjeru, string porukaOGresci = "")
        {
            var sviKorisnici = DohvatiSveKorisnike();
            var oib = sviKorisnici.Exists(korisnik => korisnik.Korisnik_OIB == korisnikZaProvjeru.Korisnik_OIB && korisnik.Korisnik_OIB != korisnikZaProvjeru.Korisnik_OIB);
            var korime = sviKorisnici.Exists(korisnik => korisnik.Korisnicko_ime == korisnikZaProvjeru.Korisnicko_ime && korisnik.Korisnik_OIB != korisnikZaProvjeru.Korisnik_OIB);

            if (oib == true)
            {
                porukaOGresci += "U bazi već postoji korisnik s tim OIB-om" + Environment.NewLine;
            };
            if (korime == true)
            {
                porukaOGresci += "U bazi već postoji korisnik s tim korisničkim imenom" + Environment.NewLine;
            };
            if (korisnikZaProvjeru.Korisnik_OIB.Length != 11)
            {
                porukaOGresci += "OIB korisnika mora imati 11 znamenki" + Environment.NewLine;
            }
            if (string.IsNullOrWhiteSpace(korisnikZaProvjeru.Ime))
            {
                porukaOGresci += "Ime korisnika nije uneseno" + Environment.NewLine;
            }
            if (string.IsNullOrWhiteSpace(korisnikZaProvjeru.Prezime))
            {
                porukaOGresci += "Prezime korisnika nije uneseno" + Environment.NewLine;
            }
            if (string.IsNullOrWhiteSpace(korisnikZaProvjeru.Korisnicko_ime))
            {
                porukaOGresci += "Korisničko ime korisnika nije uneseno" + Environment.NewLine;
            }
            if (string.IsNullOrWhiteSpace(korisnikZaProvjeru.Lozinka))
            {
                porukaOGresci += "Lozinka korisnika nije unesena" + Environment.NewLine;
            }
            if (string.IsNullOrWhiteSpace(korisnikZaProvjeru.Email))
            {
                porukaOGresci += "Email korisnika nije unesen" + Environment.NewLine;
            }
            //if (string.IsNullOrWhiteSpace(korisnikZaProvjeru.Polozene_kategorije))
            //{
            //    porukaOGresci += "Polozene kategorije korisnika nisu unesene" + Environment.NewLine;
            //}
            if (porukaOGresci != "")
            {
                throw new ArgumentException(porukaOGresci);
            }
        }
    }
}