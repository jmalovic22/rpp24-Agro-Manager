using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repozitoriji
{
    public class RepoRadniNalog : Repo<Radni_nalog>
    {
        public RepoRadniNalog() : base(new AgroManagerModel())
        {
        }

        public IQueryable<Radni_nalog> DohvatiRadneNalogePolja(int idPolja)
        {
            var upit = from radniNalog in Entiteti
                       where radniNalog.Polje_id == idPolja
                       select radniNalog;
            return upit;
        }

        public IQueryable<Radni_nalog> DohvatiRadneNalogePoduzeca(int idPoduzeca)
        {
            var upit = from radniNalog in Entiteti
                       where radniNalog.Polje.Poduzece_id == idPoduzeca
                       select radniNalog;
            return upit;
        }

        public Radni_nalog DohvatiRadniNalogPoId(int idRadnogNaloga)
        {
            var upit = Entiteti.SingleOrDefault(Radni_nalog => Radni_nalog.Radni_nalog_id == idRadnogNaloga);
            return upit;
        }

        public override int Dodaj(Radni_nalog radniNalog, bool spremiPromjene = true)
        {
            //var polje = Kontekst
            //    .Polje
            //    .SingleOrDefault(_polje => _polje.Polje_id == radniNalog.Polje_id);

            // korisnici na radnom nalogu
            List<Korisnik> korisnici = new List<Korisnik>();
            foreach (var kor in radniNalog.Korisnik)
            {
                var korisnik = Kontekst
                    .Korisnik
                    .SingleOrDefault(_korisnik => _korisnik.Korisnik_OIB == kor.Korisnik_OIB);
                if (korisnik == null)
                {
                    throw new ArgumentException("Korisnik s OIB-om " + kor.Korisnik_OIB + " ne postoji u bazi");
                }
                korisnici.Add(korisnik);
            }

            // stavke radnog naloga
            List<Stavka_radnog_naloga> stavke = new List<Stavka_radnog_naloga>();
            foreach (var stav in radniNalog.Stavka_radnog_naloga)
            {
                var stavka = new Stavka_radnog_naloga
                {
                    Status = stav.Status,
                    Razina_prioriteta = stav.Razina_prioriteta,
                    Napomena = stav.Napomena,
                    Posao_id = stav.Posao_id
                };

                stavke.Add(stavka);
            }

            var noviRadniNalog = new Radni_nalog
            {
                Datum_kreiranja = radniNalog.Datum_kreiranja,
                Zavrsni_rok = radniNalog.Zavrsni_rok,
                Datum_zavrsetka = radniNalog.Datum_zavrsetka,
                Status = radniNalog.Status, //mislim da ovo treba hardkodirat da uvijek bude string "kreiran" / "izvrsava se"
                Polje_id = radniNalog.Polje_id,
                Korisnik = korisnici,
                Stavka_radnog_naloga = stavke
            };

            Entiteti.Add(noviRadniNalog);

            if (spremiPromjene)
            {
                return SpremiPromjene();
            }
            else
            {
                return 0;
            }
        }

        public override int Azuriraj(Radni_nalog radniNalog, bool spremiPromjene = true)
        {
            //var polje = Kontekst
            //    .Polje
            //    .SingleOrDefault(_polje => _polje.Polje_id == radniNalog.Polje_id);

            var radniNalogZaAzuriranje = Kontekst
                .Radni_nalog
                .SingleOrDefault(_radniNalog => _radniNalog.Radni_nalog_id == radniNalog.Radni_nalog_id);

            radniNalogZaAzuriranje.Datum_kreiranja = radniNalog.Datum_kreiranja;
            radniNalogZaAzuriranje.Zavrsni_rok = radniNalog.Zavrsni_rok;
            radniNalogZaAzuriranje.Datum_zavrsetka = radniNalog.Datum_zavrsetka;
            radniNalogZaAzuriranje.Status = radniNalog.Status;
            radniNalogZaAzuriranje.Polje_id = radniNalog.Polje_id;

            // makni sve korisnike s liste i dodaj nove
            radniNalogZaAzuriranje.Korisnik.Clear();
            foreach (var kor in radniNalog.Korisnik)
            {
                var korisnik = Kontekst
                    .Korisnik
                    .SingleOrDefault(_korisnik => _korisnik.Korisnik_OIB == kor.Korisnik_OIB);
                if (korisnik == null)
                {
                    throw new ArgumentException("Korisnik s OIB-om " + kor.Korisnik_OIB + " ne postoji u bazi");
                }
                radniNalogZaAzuriranje.Korisnik.Add(korisnik);
            }

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