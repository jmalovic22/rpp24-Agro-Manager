using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repozitoriji
{
    public class RepoSilos : Repo<Silos>
    {
        public RepoSilos() : base(new AgroManagerModel())
        {

        }
        
        public override async Task<List<Silos>> DohvatiSveAsync()
        {
            var upit = from silosi in Entiteti.Include("Farma").Include("Uzgojna_kultura")
                       select silosi;
            return await upit.ToListAsync();
        }

        public async Task<List<Silos>> DohvatiSiloseFarmeAsync(int idFarme)
        {
            var upit = from silosi in Entiteti.Include("Farma").Include("Uzgojna_kultura")
                       where silosi.Farma_id == idFarme
                       select silosi;
            return await upit.ToListAsync();
        }

        public async Task<List<Silos>> DohvatiSilosePoduzecaAsync(int idPoduzeca)
        {
            var upit = from silosi in Entiteti.Include("Farma").Include("Uzgojna_kultura")
                       where silosi.Farma.Poduzece_id == idPoduzeca
                       select silosi;
            return await upit.ToListAsync();
        }

        public async Task<Silos> DohvatiSilosPoIdAsync(int idSilosa)
        {
            var dohvaceniSilos = Task.Run(() => Entiteti.SingleOrDefault(silos => silos.Silos_id == idSilosa));
            return await dohvaceniSilos;
        }

        public override int Dodaj(Silos silos, bool spremiPromjene = true)
        {
            //var farmaSilosa = Kontekst
            //    .Farma
            //    .SingleOrDefault(farma => farma.Farma_id == silos.Farma_id);

            //var uzgojnaKulturaSilosa = Kontekst
            //    .Uzgojna_kultura
            //    .SingleOrDefault(uzgojnaKultura => uzgojnaKultura.Uzgojna_kultura_id == silos.Uzgojna_kultura_id);

            var noviSilos = new Silos
            {
                Popunjenost = silos.Popunjenost,
                Kapacitet = silos.Kapacitet,
                Dostupnost = 1,
                Farma_id = silos.Farma_id,
                Uzgojna_kultura_id = silos.Uzgojna_kultura_id
            };

            Entiteti.Add(noviSilos);

            if (spremiPromjene)
            {
                return SpremiPromjene();
            }
            else
            {
                return 0;
            }
        }
        
        public override int Azuriraj(Silos silos, bool spremiPromjene = true)
        {
            //var farmaSilosa = Kontekst
            //    .Farma
            //    .SingleOrDefault(farma => farma.Farma_id == silos.Farma_id);

            //var uzgojnaKulturaSilosa = Kontekst
            //    .Uzgojna_kultura
            //    .SingleOrDefault(uzgojnaKultura => uzgojnaKultura.Uzgojna_kultura_id == silos.Uzgojna_kultura_id);

            var silosZaAzuriranje = Kontekst
                .Silos
                .SingleOrDefault(_silos => _silos.Silos_id == silos.Silos_id);

            silosZaAzuriranje.Farma_id = silos.Farma_id;
            silosZaAzuriranje.Uzgojna_kultura_id = silos.Uzgojna_kultura_id;
            silosZaAzuriranje.Kapacitet = silos.Kapacitet;
            silosZaAzuriranje.Popunjenost = silos.Popunjenost;
            silosZaAzuriranje.Dostupnost = silos.Dostupnost;

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
