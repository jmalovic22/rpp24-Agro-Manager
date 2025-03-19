using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repozitoriji
{
    public class RepoPrikljucak : Repo<Prikljucak>
    {
        public RepoPrikljucak() : base(new AgroManagerModel())
        { }

        public override IQueryable<Prikljucak> DohvatiSve()
        {
            var upit = from prikljucak in Entiteti.Include("Poduzece")
                       select prikljucak;
            return upit;
        }

        public IQueryable<Prikljucak> DohvatiPrikljuckePoduzeca(int idPoduzeca)
        {
            var upit = from prikljucak in Entiteti.Include("Poduzece")
                       where prikljucak.Poduzece_id == idPoduzeca
                       select prikljucak;
            return upit;
        }

        public Prikljucak DohvatiPrikljucakPoId(int idPrikljucka)
        {
            var upit = Entiteti.SingleOrDefault(prikljucak => prikljucak.Prikljucak_id == idPrikljucka);
            return upit;
        }

        public override int Dodaj(Prikljucak prikljucak, bool spremiPromjene = true)
        {
            //var poduzecePrikljucka = Kontekst
            //    .Poduzece
            //    .SingleOrDefault(poduzece => poduzece.Poduzece_id == prikljucak.Poduzece_id);

            //var vrstePrikljucka = Kontekst
            //    .Vrsta_prikljucka
            //    .SingleOrDefault(vrstaPrikljucka => vrstaPrikljucka.Vrsta_prikljucka_id == prikljucak.Vrsta_prikljucka_id);

            var noviPrikljucak = new Prikljucak
            {
                Registracija = prikljucak.Registracija,
                Marka = prikljucak.Marka,
                Dostupnost = "Dostupno",
                Poduzece_id = prikljucak.Poduzece_id,
                Vrsta_prikljucka_id = prikljucak.Vrsta_prikljucka_id,
                Registracija_vozila = prikljucak.Registracija_vozila
            };

            Entiteti.Add(noviPrikljucak);

            if (spremiPromjene)
            {
                return SpremiPromjene();
            }
            else
            {
                return 0;
            }
        }

        public override int Azuriraj(Prikljucak prikljucak, bool spremiPromjene = true)
        {
            var prikljucakZaAzuriranje = Entiteti.Find(prikljucak.Prikljucak_id);

            //var poduzecePrikljucka = Kontekst
            //    .Poduzece
            //    .SingleOrDefault(poduzece => poduzece.Poduzece_id == prikljucak.Poduzece_id);

            //var vrstePrikljucka = Kontekst
            //    .Vrsta_prikljucka
            //    .SingleOrDefault(vrstaPrikljucka => vrstaPrikljucka.Vrsta_prikljucka_id == prikljucak.Vrsta_prikljucka_id);

            prikljucakZaAzuriranje.Marka = prikljucak.Marka;
            prikljucakZaAzuriranje.Registracija = prikljucak.Registracija;
            prikljucakZaAzuriranje.Dostupnost = prikljucak.Dostupnost;
            prikljucakZaAzuriranje.Poduzece_id = prikljucak.Poduzece_id;
            prikljucakZaAzuriranje.Registracija_vozila = prikljucak.Registracija_vozila;
            prikljucakZaAzuriranje.Vrsta_prikljucka_id = prikljucak.Vrsta_prikljucka_id;

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
