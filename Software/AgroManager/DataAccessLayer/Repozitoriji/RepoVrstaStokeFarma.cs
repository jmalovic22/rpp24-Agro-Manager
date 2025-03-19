using EntitiesLayer.Entiteti;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repozitoriji
{
    public class RepoVrstaStokeFarma : Repo<Vrsta_stoke_farma>
    {
        public RepoVrstaStokeFarma() : base(new AgroManagerModel())
        {
        }

        public override async Task<List<Vrsta_stoke_farma>> DohvatiSveAsync()
        {
            var upit = from e in Entiteti
                       select e;
            return await upit.ToListAsync();
        }

        public override int Dodaj(Vrsta_stoke_farma vrstaStokeFarma, bool spremiPromjene = true)
        {
            //var farmaStoke = Task.Run(() => Kontekst
            //    .Farma
            //    .SingleOrDefault(_farma => _farma.Farma_id == vrstaStokeFarma.Farma_id));

            //var vrstaStoke = Kontekst
            //    .Vrsta_stoke
            //    .SingleOrDefault(_vrstaStoke => _vrstaStoke.Vrsta_stoke_id == vrstaStokeFarma.Vrsta_stoke_id);

            var novaVrstaStokeFarma = new Vrsta_stoke_farma
            {
                Kolicina_stoke = vrstaStokeFarma.Kolicina_stoke,
                Farma_id = vrstaStokeFarma.Farma_id,
                Vrsta_stoke_id = vrstaStokeFarma.Vrsta_stoke_id
            };

            Entiteti.Add(novaVrstaStokeFarma);

            if (spremiPromjene)
            {
                return SpremiPromjene();
            }
            else
            {
                return 0;
            }
        }

        public override int Azuriraj(Vrsta_stoke_farma vrstaStokeFarma, bool spremiPromjene = true)
        {
           /* var vrstaStokeFarmaZaAzuriranje = Kontekst
                .Vrsta_stoke_farma
                .SingleOrDefault(_vrstaStokeFarma => _vrstaStokeFarma.Vrsta_stoke_farma_id == vrstaStokeFarma.Vrsta_stoke_farma_id);

            var farmaStoke = Kontekst
                .Farma
                .SingleOrDefault(_farma => _farma.Farma_id == vrstaStokeFarmaZaAzuriranje.Farma_id);

            var vrstaStoke = Kontekst
                .Vrsta_stoke
                .SingleOrDefault(_vrstaStoke => _vrstaStoke.Vrsta_stoke_id == vrstaStokeFarmaZaAzuriranje.Vrsta_stoke_id);

            vrstaStokeFarmaZaAzuriranje.Kolicina_stoke = vrstaStokeFarma.Kolicina_stoke;
            vrstaStokeFarmaZaAzuriranje.Farma = farmaStoke;
            vrstaStokeFarmaZaAzuriranje.Vrsta_stoke = vrstaStoke;

            if (spremiPromjene)
            {
                return SpremiPromjene();
            }
            else
            {
                return 0;
            }*/

            throw new NotImplementedException();
        }

        public async Task<List<Vrsta_stoke_farma>> DohvatiSveVrsteStokeFarmeAsync(int idFarme)
        {

            var upit = from e in Entiteti
                       where e.Farma_id == idFarme 
                       select e;
            
            return await upit.ToListAsync();
        }
    }
}
