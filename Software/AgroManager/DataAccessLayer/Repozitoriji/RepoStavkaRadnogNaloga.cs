using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repozitoriji
{
    public class RepoStavkaRadnogNaloga : Repo<Stavka_radnog_naloga>
    {
        public RepoStavkaRadnogNaloga() : base(new AgroManagerModel())
        { }

        public IQueryable<Stavka_radnog_naloga> DohvatiStavkeRadnogNaloga(int idRadnogNaloga)
        {
            var upit = from stavka in Entiteti.Include("Radni_nalog")
                       where stavka.Radni_nalog_id == idRadnogNaloga
                       select stavka;
            return upit;
        }

        public Stavka_radnog_naloga DohvatiStavkuRadnogNalogaPoId(int idStavkeRadnogNaloga)
        {
            var upit = Entiteti.SingleOrDefault(Stavka_radnog_naloga => Stavka_radnog_naloga.Stavka_radnog_naloga_id == idStavkeRadnogNaloga);
            return upit;
        }

        public override int Dodaj(Stavka_radnog_naloga stavka, bool spremiPromjene = true)
        {
            //var radniNalog = Kontekst
            //    .Radni_nalog
            //    .SingleOrDefault(_radniNalog => _radniNalog.Radni_nalog_id == stavka.Radni_nalog_id);

            var novoStavka = new Stavka_radnog_naloga
            {
                Status = stavka.Status,
                Razina_prioriteta = stavka.Razina_prioriteta,
                Napomena = stavka.Napomena,
                Radni_nalog_id = stavka.Radni_nalog_id,
                Posao_id = stavka.Posao_id
            };

            Entiteti.Add(novoStavka);

            if (spremiPromjene)
            {
                return SpremiPromjene();
            }
            else
            {
                return 0;
            }
        }

        public override int Azuriraj(Stavka_radnog_naloga stavka, bool spremiPromjene = true)
        {
            var stavkaRradnogNalogaZaAzuriranje = Kontekst
                .Stavka_radnog_naloga
                .SingleOrDefault(_stavkaRadniNalog => _stavkaRadniNalog.Stavka_radnog_naloga_id == stavka.Stavka_radnog_naloga_id);

            stavkaRradnogNalogaZaAzuriranje.Status = stavka.Status;
            stavkaRradnogNalogaZaAzuriranje.Razina_prioriteta = stavka.Razina_prioriteta;
            stavkaRradnogNalogaZaAzuriranje.Napomena = stavka.Napomena;
            stavkaRradnogNalogaZaAzuriranje.Radni_nalog_id = stavka.Radni_nalog_id;
            stavkaRradnogNalogaZaAzuriranje.Posao = stavka.Posao;

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
