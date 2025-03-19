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
    public class RepoFarma : Repo<Farma>
    {
        public RepoFarma() : base(new AgroManagerModel())
        {
        }

        public override async Task<List<Farma>> DohvatiSveAsync()
        {
            var upit = from e in Entiteti.Include("Poduzece")
                       select e;
            return await upit.ToListAsync();
        }

        public async Task<Farma> DohvatiFarmuPremaIdAsync(int idFarme)
        {
            var dohvacenaFarma = Task.Run(() => Entiteti.SingleOrDefault(farma => farma.Farma_id == idFarme));
            return await dohvacenaFarma;
        }

        public async Task<List<Farma>> DohvatiFarmePoduzecaAsync(int idPoduzeca)
        {

            var upit = from e in Entiteti.Include("Poduzece")
                       where e.Poduzece_id == idPoduzeca
                       select e;
            return await upit.ToListAsync();
        }
        
        public override int Dodaj(Farma farma, bool spremiPromjene = true)
        {
            var poduzeceFarme = Kontekst
                .Poduzece
                .SingleOrDefault(poduzece => poduzece.Poduzece_id == farma.Poduzece_id);

            var novaFarma = new Farma
            {
                Lokacija = farma.Lokacija,
                Povrsina = farma.Povrsina,
                Broj_zaposlenih = farma.Broj_zaposlenih,
                Status = "Aktivna",    
                Poduzece_id = poduzeceFarme.Poduzece_id,
            };

            Entiteti.Add(novaFarma);

            if (spremiPromjene)
            {
                return SpremiPromjene();
            }
            else
            {
                return 0;
            }
        }
        
        public override int Azuriraj(Farma farma, bool spremiPromjene = true)
        {
            //var poduzeceFarme = Kontekst
            //    .Poduzece
            //    .SingleOrDefault(poduzece => poduzece.Poduzece_id == farma.Poduzece_id);

            var farmaZaAzuriranje = Kontekst
                .Farma
                .SingleOrDefault(_farma => _farma.Farma_id == farma.Farma_id);

            farmaZaAzuriranje.Lokacija = farma.Lokacija;
            farmaZaAzuriranje.Povrsina = farma.Povrsina;
            farmaZaAzuriranje.Broj_zaposlenih = farma.Broj_zaposlenih;
            farmaZaAzuriranje.Status = farma.Status;
            farmaZaAzuriranje.Poduzece_id = farma.Poduzece_id;

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
