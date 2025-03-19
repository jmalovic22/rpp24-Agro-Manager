using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repozitoriji
{
    public class RepoEvidencijaStokeFarma : Repo<Evidencija_stoke_farma>
    {
        public RepoEvidencijaStokeFarma() : base(new AgroManagerModel())
        {
        }

        public override async Task<List<Evidencija_stoke_farma>> DohvatiSveAsync()
        {
            var upit = from e in Entiteti.Include("Vrsta_stoke_farma")
                       select e;
            return await upit.ToListAsync();
        }

        public override int Dodaj(Evidencija_stoke_farma evidencijaStokeFarma, bool spremiPromjene = true)
        {
            //var vrstaStokeZaEvidenciju = Kontekst
            //    .Vrsta_stoke_farma
            //    .SingleOrDefault(_vrstaStokeZaEvidenciju => _vrstaStokeZaEvidenciju.Vrsta_stoke_farma_id == evidencijaStokeFarma.Vrsta_stoke_farma_id);

            var novaEvidencijaStokeFarma = new Evidencija_stoke_farma
            {
                Datum_promjene = evidencijaStokeFarma.Datum_promjene,
                Kolicina_promjene = evidencijaStokeFarma.Kolicina_promjene,
                Napomena = evidencijaStokeFarma.Napomena,
                Vrsta_stoke_farma_id = evidencijaStokeFarma.Vrsta_stoke_farma_id,
            };

            Entiteti.Add(novaEvidencijaStokeFarma);

            if (spremiPromjene)
            {
                return SpremiPromjene();
            }
            else
            {
                return 0;
            }
        }

        public override int Azuriraj(Evidencija_stoke_farma evidencijaStokeFarma, bool spremiPromjene = true)
        {
            var evidencijaStokeFarmaZaAzuriranje = Kontekst
                .Evidencija_stoke_farma
                .SingleOrDefault(_evidencijaStokeFarma => _evidencijaStokeFarma.Evidencija_stoke_farma_id == evidencijaStokeFarma.Evidencija_stoke_farma_id);

            //var vrstaStokeZaEvidenciju = Kontekst
            //    .Vrsta_stoke_farma
            //    .SingleOrDefault(_vrstaStokeZaEvidenciju => _vrstaStokeZaEvidenciju.Vrsta_stoke_farma_id == evidencijaStokeFarma.Vrsta_stoke_farma_id);

            evidencijaStokeFarmaZaAzuriranje.Datum_promjene = evidencijaStokeFarma.Datum_promjene;
            evidencijaStokeFarmaZaAzuriranje.Kolicina_promjene = evidencijaStokeFarma.Kolicina_promjene;
            evidencijaStokeFarmaZaAzuriranje.Napomena = evidencijaStokeFarma.Napomena;
            evidencijaStokeFarmaZaAzuriranje.Vrsta_stoke_farma_id = evidencijaStokeFarma.Vrsta_stoke_farma_id;

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
