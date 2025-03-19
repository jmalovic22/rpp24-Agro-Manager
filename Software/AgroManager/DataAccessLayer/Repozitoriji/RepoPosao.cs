using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repozitoriji
{
    public class RepoPosao : Repo<Posao>
    {
        public RepoPosao() : base(new AgroManagerModel())
        { }

        public IQueryable<Posao> DohvatiPoslove()
        {
            var upit = from posao in Entiteti
                       select posao;
            return upit;
        }

        public Posao DohvatiPosaoPoId(int idPosla)
        {
            var upit = Entiteti.SingleOrDefault(posao => posao.Posao_id == idPosla);
            return upit;
        }

        public override int Dodaj(Posao posao, bool spremiPromjene = true)
        {
            var noviPosao = new Posao
            {
                Naziv = posao.Naziv,
                Opis = posao.Opis,
            };

            Entiteti.Add(noviPosao);

            if (spremiPromjene)
            {
                return SpremiPromjene();
            }
            else
            {
                return 0;
            }
        }

        public override int Azuriraj(Posao posao, bool spremiPromjene = true)
        {
            var posaoZaAzuriranje = Entiteti.Find(posao.Posao_id);

            //var poduzecePrikljucka = Kontekst
            //    .Poduzece
            //    .SingleOrDefault(poduzece => poduzece.Poduzece_id == prikljucak.Poduzece_id);

            //var vrstePrikljucka = Kontekst
            //    .Vrsta_prikljucka
            //    .SingleOrDefault(vrstaPrikljucka => vrstaPrikljucka.Vrsta_prikljucka_id == prikljucak.Vrsta_prikljucka_id);

            posaoZaAzuriranje.Naziv = posao.Naziv;
            posaoZaAzuriranje.Opis = posao.Opis;

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
