using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repozitoriji
{
    public class RepoIzdatnica : Repo<Izdatnica>
    {

        public RepoIzdatnica() : base(new AgroManagerModel())
        {
        }

        public override IQueryable<Izdatnica> DohvatiSve()
        {
            var upit = from e in Entiteti.Include("Silos").Include("Vozilo").Include("Korisnik")
                       select e;
            return upit;
        }

        public IQueryable<Izdatnica> DohvatiIzdatniceSilosa(int idSilosa)
        {
            var upit = from e in Entiteti.Include("Vozilo").Include("Korisnik")
                       where e.Silos_id == idSilosa
                       select e;
            return upit;
        }

        public IQueryable<Izdatnica> DohvatiIzdatniceKorisnika(string oibKorisnika)
        {

            var upit = from e in Entiteti.Include("Silos").Include("Vozilo")
                       where e.Korisnik_OIB == oibKorisnika
                       select e;
            return upit;
        }

        public override int Dodaj(Izdatnica Izdatnica, bool spremiPromjene = true)
        {
            var silosIzdatnice = Kontekst
                .Silos
                .SingleOrDefault(silos => silos.Silos_id == Izdatnica.Silos_id);

            var registracijaVozilaIzdatnice = Kontekst
                .Vozilo
                .SingleOrDefault(vozilo => vozilo.Registracija == Izdatnica.Registracija_vozila);

            var korisnikNaIzdatnici = Kontekst
                .Korisnik
                .SingleOrDefault(korisnik => korisnik.Korisnik_OIB == Izdatnica.Korisnik_OIB);

            var novaIzdatnica = new Izdatnica
            {
                Datum = Izdatnica.Datum,
                Kolicina_u_kg = Izdatnica.Kolicina_u_kg,
                Biljeske = Izdatnica.Biljeske,
                Silos = silosIzdatnice,
                Vozilo = registracijaVozilaIzdatnice,
                Korisnik = korisnikNaIzdatnici
            };

            Entiteti.Add(novaIzdatnica);

            if (spremiPromjene)
            {
                return SpremiPromjene();
            }
            else
            {
                return 0;
            }
        }

        public override int Azuriraj(Izdatnica Izdatnica, bool spremiPromjene = true)
        {
            var silosIzdatnice = Kontekst
                .Silos
                .SingleOrDefault(silos => silos.Silos_id == Izdatnica.Silos_id);

            var registracijaVozilaIzdatnice = Kontekst
                .Vozilo
                .SingleOrDefault(vozilo => vozilo.Registracija == Izdatnica.Registracija_vozila);

            var korisnikNaIzdatnici = Kontekst
                .Korisnik
                .SingleOrDefault(korisnik => korisnik.Korisnik_OIB == Izdatnica.Korisnik_OIB);

            var IzdatnicaZaAzuriranje = Kontekst
                .Izdatnica
                .SingleOrDefault(_Izdatnica => _Izdatnica.Izdatnica_id == Izdatnica.Izdatnica_id);

            IzdatnicaZaAzuriranje.Datum = Izdatnica.Datum;
            IzdatnicaZaAzuriranje.Kolicina_u_kg = Izdatnica.Kolicina_u_kg;
            IzdatnicaZaAzuriranje.Biljeske = Izdatnica.Biljeske;
            IzdatnicaZaAzuriranje.Silos = silosIzdatnice;
            IzdatnicaZaAzuriranje.Vozilo = registracijaVozilaIzdatnice;
            IzdatnicaZaAzuriranje.Korisnik = korisnikNaIzdatnici;

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
