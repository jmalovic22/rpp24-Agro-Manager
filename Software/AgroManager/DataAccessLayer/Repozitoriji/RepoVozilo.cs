using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repozitoriji
{
    public class RepoVozilo : Repo<Vozilo>
    {
        public RepoVozilo() : base(new AgroManagerModel())
        {
        }

        public override IQueryable<Vozilo> DohvatiSve()
        {
            var upit = from vozila in Entiteti.Include("Poduzece")
                       select vozila;
            return upit;
        }

        public IQueryable<Vozilo> DohvatiVozilaPoduzeca(int idPoduzeca)
        {
            var upit = from vozila in Entiteti.Include("Poduzece")
                       where vozila.Poduzece_id == idPoduzeca
                       select vozila;
            return upit;
        }

        public Vozilo DohvatiVoziloPoRegistraciji(string registracijaVozila)
        {
            var upit = Entiteti.SingleOrDefault(vozilo => vozilo.Registracija == registracijaVozila);
            return upit;
        }

        public override int Dodaj(Vozilo vozilo, bool spremiPromjene = true)
        {
            //var poduzeceVozila = Kontekst
            //    .Poduzece
            //    .SingleOrDefault(poduzece => poduzece.Poduzece_id == vozilo.Poduzece_id);

            var novoVozilo = new Vozilo
            {
                Registracija = vozilo.Registracija,
                Vrsta = vozilo.Vrsta,
                Marka = vozilo.Marka,
                Jacina_motora_u_KS = vozilo.Jacina_motora_u_KS,
                Opis = vozilo.Opis,
                Dostupnost = "Dostupno",
                Poduzece_id = vozilo.Poduzece_id
            };
            Entiteti.Add(novoVozilo);
            if (spremiPromjene)
            {
                return SpremiPromjene();
            }
            else
            {
                return 0;
            }
        }

        public override int Azuriraj(Vozilo vozilo, bool spremiPromjene = true)
        {
            var voziloZaAzuriraj = Entiteti.Find(vozilo.Registracija);

            //var poduzeceVozila = Kontekst
            //    .Poduzece
            //    .SingleOrDefault(poduzece => poduzece.Poduzece_id == vozilo.Poduzece_id);

            voziloZaAzuriraj.Vrsta = vozilo.Vrsta;
            voziloZaAzuriraj.Marka = vozilo.Marka;
            voziloZaAzuriraj.Jacina_motora_u_KS = vozilo.Jacina_motora_u_KS;
            voziloZaAzuriraj.Opis = vozilo.Opis;
            voziloZaAzuriraj.Dostupnost = vozilo.Dostupnost;
            voziloZaAzuriraj.Poduzece_id = vozilo.Poduzece_id;

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
