using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repozitoriji
{
    public abstract class Repo<T> : IDisposable where T : class
    {
        protected AgroManagerModel Kontekst { get; set; }
        public DbSet<T> Entiteti { get; set; }
        
        public Repo(AgroManagerModel kontekst)
        {
            Kontekst = kontekst;
            Entiteti = Kontekst.Set<T>();
        }

        public virtual async Task<List<T>> DohvatiSveAsync()
        {
            var upit = from e in Entiteti
                       select e;
            return await upit.ToListAsync();
        }

        public virtual IQueryable<T> DohvatiSve()
        {
            var upit = from e in Entiteti
                       select e;
            return upit;
        }

        public int SpremiPromjene()
        { 
            return Kontekst.SaveChanges();
        }

        public virtual int Dodaj(T entitet, bool spremiPromjene = true)
        {
            Entiteti.Add(entitet);
            if (spremiPromjene)
            {
                return SpremiPromjene();
            }
            else
            {
                return 0;
            }
        }

        public abstract int Azuriraj(T entitet, bool spremiPromjene = true);

        public virtual int Izbrisi(T entitet, bool spremiPromjene = true)
        {
            Entiteti.Attach(entitet);
            Entiteti.Remove(entitet);
            if (spremiPromjene)
            {
                return SpremiPromjene();
            }
            else
            {
                return 0;
            }
        }

        public void Dispose()
        {
            Kontekst.Dispose();
        }
    }
}
