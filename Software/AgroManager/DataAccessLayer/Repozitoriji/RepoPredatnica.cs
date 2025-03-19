using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repozitoriji
{
    public class RepoPredatnica : Repo<Predatnica>
    {

        public RepoPredatnica() : base(new AgroManagerModel())
        {
        }

        public override IQueryable<Predatnica> DohvatiSve()
        {
            var upit = from e in Entiteti.Include("Silos").Include("Vozilo").Include("Korisnik")
                       select e;
            return upit;
        }

        public IQueryable<Predatnica> DohvatiPredatniceSilosa(int idSilosa)
        {
            var upit = from e in Entiteti.Include("Vozilo").Include("Korisnik")
                       where e.Silos_id == idSilosa
                       select e;
            return upit;
        }

        public IQueryable<Predatnica> DohvatiPredatniceKorisnika(string oibKorisnika)
        {
            var upit = from e in Entiteti.Include("Silos").Include("Vozilo")
                       where e.Korisnik_OIB == oibKorisnika
                       select e;
            return upit;
        }

        public override int Dodaj(Predatnica predatnica, bool spremiPromjene = true)
        {
            var silosPredatnice = Kontekst
                .Silos
                .SingleOrDefault(silos => silos.Silos_id == predatnica.Silos_id);

            var registracijaVozilaPredatnice = Kontekst
                .Vozilo
                .SingleOrDefault(vozilo => vozilo.Registracija == predatnica.Registracija_vozila);

            var korisnikNaPredatnici = Kontekst
                .Korisnik
                .SingleOrDefault(korisnik => korisnik.Korisnik_OIB == predatnica.Korisnik_OIB);

            var novaPredatnica = new Predatnica
            {
                Datum = predatnica.Datum,
                Kolicina_u_kg = predatnica.Kolicina_u_kg,
                Biljeske = predatnica.Biljeske,
                Silos = silosPredatnice,
                Vozilo = registracijaVozilaPredatnice,
                Korisnik = korisnikNaPredatnici
            };

            Entiteti.Add(novaPredatnica);

            if (spremiPromjene)
            {
                return SpremiPromjene();
            }
            else
            {
                return 0;
            }
        }

        public override int Azuriraj(Predatnica predatnica, bool spremiPromjene = true)
        {
            var silosPredatnice = Kontekst
                .Silos
                .SingleOrDefault(silos => silos.Silos_id == predatnica.Silos_id);

            var registracijaVozilaPredatnice = Kontekst
                .Vozilo
                .SingleOrDefault(vozilo => vozilo.Registracija == predatnica.Registracija_vozila);

            var korisnikNaPredatnici = Kontekst
                .Korisnik
                .SingleOrDefault(korisnik => korisnik.Korisnik_OIB == predatnica.Korisnik_OIB);

            var predatnicaZaAzuriranje = Kontekst
                .Predatnica
                .SingleOrDefault(_Predatnica => _Predatnica.Predatnica_id == predatnica.Predatnica_id);

            predatnicaZaAzuriranje.Datum = predatnica.Datum;
            predatnicaZaAzuriranje.Kolicina_u_kg = predatnica.Kolicina_u_kg;
            predatnicaZaAzuriranje.Biljeske = predatnica.Biljeske;
            predatnicaZaAzuriranje.Silos = silosPredatnice;
            predatnicaZaAzuriranje.Vozilo = registracijaVozilaPredatnice;
            predatnicaZaAzuriranje.Korisnik = korisnikNaPredatnici;

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