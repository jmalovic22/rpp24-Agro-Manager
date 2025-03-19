using EntitiesLayer.Enkodiranje;
using EntitiesLayer.Entiteti;
using EntitiesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repozitoriji
{
    public class RepoKorisnik : Repo<Korisnik>
    {
        public RepoKorisnik() : base(new AgroManagerModel())
        {
        }

        public ResursiPoduzeca DohvatiResurse(int idPoduzeca)
        {
            var resursi = new ResursiPoduzeca
            {
                brojFarmi = Kontekst.Farma.Where(farma => farma.Poduzece_id == idPoduzeca).Count(),
                brojSilosa = Kontekst.Silos.Where(silos => silos.Farma.Poduzece_id == idPoduzeca).Count(),
                brojZaposlenih = Kontekst.Korisnik.Where(korisnik => korisnik.Poduzece_id == idPoduzeca).Count(),
                brojVozila = Kontekst.Vozilo.Where(vozilo => vozilo.Poduzece_id == idPoduzeca).Count(),
                brojPrikljucaka = Kontekst.Prikljucak.Where(prikljucak => prikljucak.Poduzece_id == idPoduzeca).Count(),
                brojPolja = Kontekst.Polje.Where(polje => polje.Poduzece_id == idPoduzeca).Count()
            };

            return resursi;
        }

        public override IQueryable<Korisnik> DohvatiSve()
        {
            var upit = from e in Entiteti.Include("Tip_korisnika").Include("Poduzece")
                       select e;
            return upit;
        }

        public Korisnik DohvatiKorisnika(string korime, string lozinka)
        {
            //var upit = Entiteti.SingleOrDefault(korisnik => korisnik.Korisnicko_ime == korima && korisnik.Lozinka == lozinka);
            var upit = Entiteti.SingleOrDefault(korisnik => korisnik.Korisnicko_ime == korime);
            return upit;
        }

        public IQueryable<Korisnik> DohvatiKorisnikePoduzeca(int idPoduzeca)
        {
            var upit = from korisnik in Entiteti
                       where korisnik.Poduzece_id == idPoduzeca
                       select korisnik;
            return upit;
        }

        public override int Dodaj(Korisnik korisnik, bool spremiPromjene = true)
        {
            //var tipKorisnika = Kontekst
            //    .Tip_korisnika
            //    .SingleOrDefault(_tipKorisnika => _tipKorisnika.Tip_korisnika_id == korisnik.Tip_korisnika_id);

            //var poduzece = Kontekst
            //    .Poduzece
            //    .SingleOrDefault(_poduzece => _poduzece.Poduzece_id == korisnik.Poduzece_id);

            var noviKorisnik = new Korisnik
            {
                Korisnik_OIB = korisnik.Korisnik_OIB,
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime,
                Korisnicko_ime = korisnik.Korisnicko_ime,
                Lozinka = Enkodiranje.ComputeSha256Hash(korisnik.Lozinka + Enkodiranje.sol),
                Email = korisnik.Email,
                Polozene_kategorije = korisnik.Polozene_kategorije,
                Tip_korisnika_id = korisnik.Tip_korisnika_id,
                Poduzece_id = korisnik.Poduzece_id,
                Secret = korisnik.Secret
            };

            Entiteti.Add(noviKorisnik);

            if (spremiPromjene)
            {
                return SpremiPromjene();
            }
            else
            {
                return 0;
            }
        }

        public override int Azuriraj(Korisnik korisnik, bool spremiPromjene = true)
        {
            //var tipKorisnika = Kontekst
            //    .Tip_korisnika
            //    .SingleOrDefault(_tipKorisnika => _tipKorisnika.Tip_korisnika_id == korisnik.Tip_korisnika_id);

            //var poduzece = Kontekst
            //    .Poduzece
            //    .SingleOrDefault(_poduzece => _poduzece.Poduzece_id == korisnik.Poduzece_id);

            var korisnikZaAzuriranje = Entiteti.Find(korisnik.Korisnik_OIB);

            korisnikZaAzuriranje.Ime = korisnik.Ime;
            korisnikZaAzuriranje.Prezime = korisnik.Prezime;
            korisnikZaAzuriranje.Korisnicko_ime = korisnik.Korisnicko_ime;
            //korisnikZaAzuriranje.Lozinka = korisnik.Lozinka;
            korisnikZaAzuriranje.Email = korisnik.Email;
            korisnikZaAzuriranje.Polozene_kategorije = korisnik.Polozene_kategorije;
            korisnikZaAzuriranje.Tip_korisnika_id = korisnik.Tip_korisnika_id;
            korisnikZaAzuriranje.Poduzece_id = korisnik.Poduzece_id;

            if (spremiPromjene)
            {
                return SpremiPromjene();
            }
            else
            {
                return 0;
            }
        }
    }
}
