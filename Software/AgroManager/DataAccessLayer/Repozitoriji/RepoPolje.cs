using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repozitoriji
{
    public class RepoPolje : Repo<Polje>
    {
        public RepoPolje() : base(new AgroManagerModel())
        { }
        public IQueryable<Polje> DohvatiPolja()
        {
            var upit = from polje in Entiteti
                       select polje;
            return upit;
        }
        public Polje DohvatiPoljePoId(int idPolja)
        {
            var upit = Entiteti.SingleOrDefault(polje => polje.Polje_id == idPolja);
            return upit;
        }

        public IQueryable<Polje> DohvatiPoljaPoduzeca(int idPoduzeca)
        {
            var upit = from polje in Entiteti
                       where polje.Poduzece_id == idPoduzeca
                       select polje;
            return upit;
        }

        public override int Dodaj(Polje polje, bool spremiPromjene = true)
        {
            var novoPolje = new Polje
            {
                Ime_polja = polje.Ime_polja,
                Lokacija = polje.Lokacija,
                Velicina = polje.Velicina,
                Tip_tla = polje.Tip_tla,
                Poduzece_id = polje.Poduzece_id,
                Uzgojna_kultura_id = polje.Uzgojna_kultura_id
            };

            Entiteti.Add(novoPolje);

            if (spremiPromjene)
            {
                return SpremiPromjene();
            }
            else
            {
                return 0;
            }
        }

        public override int Azuriraj(Polje polje, bool spremiPromjene = true)
        {
            var poljeZaAzuriranje = Entiteti.Find(polje.Polje_id);

            poljeZaAzuriranje.Ime_polja = polje.Ime_polja;
            poljeZaAzuriranje.Lokacija = polje.Lokacija;
            poljeZaAzuriranje.Velicina = polje.Velicina;
            poljeZaAzuriranje.Tip_tla = polje.Tip_tla;
            poljeZaAzuriranje.Poduzece_id = polje.Poduzece_id;
            poljeZaAzuriranje.Uzgojna_kultura_id = polje.Uzgojna_kultura_id;

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
