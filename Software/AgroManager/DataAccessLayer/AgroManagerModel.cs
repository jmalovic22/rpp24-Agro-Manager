using EntitiesLayer.Entiteti;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer
{
    public partial class AgroManagerModel : DbContext
    {
        public AgroManagerModel()
            : base("name=AgroManagerModel")
        {
        }

        public virtual DbSet<Evidencija_stoke_farma> Evidencija_stoke_farma { get; set; }
        public virtual DbSet<Farma> Farma { get; set; }
        public virtual DbSet<Izdatnica> Izdatnica { get; set; }
        public virtual DbSet<Korisnik> Korisnik { get; set; }
        public virtual DbSet<Poduzece> Poduzece { get; set; }
        public virtual DbSet<Polje> Polje { get; set; }
        public virtual DbSet<Posao> Posao { get; set; }
        public virtual DbSet<Predatnica> Predatnica { get; set; }
        public virtual DbSet<Prikljucak> Prikljucak { get; set; }
        public virtual DbSet<Radni_nalog> Radni_nalog { get; set; }
        public virtual DbSet<Silos> Silos { get; set; }
        public virtual DbSet<Stavka_radnog_naloga> Stavka_radnog_naloga { get; set; }
        public virtual DbSet<Tip_korisnika> Tip_korisnika { get; set; }
        public virtual DbSet<Uzgojna_kultura> Uzgojna_kultura { get; set; }
        public virtual DbSet<Vozilo> Vozilo { get; set; }
        public virtual DbSet<Vrsta_prikljucka> Vrsta_prikljucka { get; set; }
        public virtual DbSet<Vrsta_stoke> Vrsta_stoke { get; set; }
        public virtual DbSet<Vrsta_stoke_farma> Vrsta_stoke_farma { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evidencija_stoke_farma>()
                .Property(e => e.Napomena)
                .IsUnicode(false);

            modelBuilder.Entity<Farma>()
                .Property(e => e.Lokacija)
                .IsUnicode(false);

            modelBuilder.Entity<Farma>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Farma>()
                .HasMany(e => e.Silos)
                .WithRequired(e => e.Farma)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Farma>()
                .HasMany(e => e.Vrsta_stoke_farma)
                .WithRequired(e => e.Farma)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Izdatnica>()
                .Property(e => e.Biljeske)
                .IsUnicode(false);

            modelBuilder.Entity<Izdatnica>()
                .Property(e => e.Registracija_vozila)
                .IsUnicode(false);

            modelBuilder.Entity<Izdatnica>()
                .Property(e => e.Korisnik_OIB)
                .IsUnicode(false);

            modelBuilder.Entity<Korisnik>()
                .Property(e => e.Korisnik_OIB)
                .IsUnicode(false);

            modelBuilder.Entity<Korisnik>()
                .Property(e => e.Ime)
                .IsUnicode(false);

            modelBuilder.Entity<Korisnik>()
                .Property(e => e.Prezime)
                .IsUnicode(false);

            modelBuilder.Entity<Korisnik>()
                .Property(e => e.Korisnicko_ime)
                .IsUnicode(false);

            modelBuilder.Entity<Korisnik>()
                .Property(e => e.Lozinka)
                .IsUnicode(false);

            modelBuilder.Entity<Korisnik>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Korisnik>()
                .Property(e => e.Polozene_kategorije)
                .IsUnicode(false);

            modelBuilder.Entity<Korisnik>()
                .HasMany(e => e.Izdatnica)
                .WithRequired(e => e.Korisnik)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Korisnik>()
                .HasMany(e => e.Predatnica)
                .WithRequired(e => e.Korisnik)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Korisnik>()
                .HasMany(e => e.Radni_nalog)
                .WithMany(e => e.Korisnik)
                .Map(m => m.ToTable("Korisnici_radni_nalog").MapLeftKey("Korisnik_OIB").MapRightKey("Radni_nalog_id"));

            modelBuilder.Entity<Poduzece>()
                .Property(e => e.Naziv)
                .IsUnicode(false);

            modelBuilder.Entity<Poduzece>()
                .Property(e => e.Vlasnik)
                .IsUnicode(false);

            modelBuilder.Entity<Poduzece>()
                .Property(e => e.Telefon)
                .IsUnicode(false);

            modelBuilder.Entity<Poduzece>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Poduzece>()
                .HasMany(e => e.Korisnik)
                .WithRequired(e => e.Poduzece)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Poduzece>()
                .HasMany(e => e.Polje)
                .WithRequired(e => e.Poduzece)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Poduzece>()
                .HasMany(e => e.Vozilo)
                .WithRequired(e => e.Poduzece)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Poduzece>()
                .HasMany(e => e.Prikljucak)
                .WithRequired(e => e.Poduzece)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Polje>()
                .Property(e => e.Ime_polja)
                .IsUnicode(false);

            modelBuilder.Entity<Polje>()
                .Property(e => e.Lokacija)
                .IsUnicode(false);

            modelBuilder.Entity<Polje>()
                .Property(e => e.Tip_tla)
                .IsUnicode(false);

            modelBuilder.Entity<Polje>()
                .HasMany(e => e.Radni_nalog)
                .WithRequired(e => e.Polje)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Posao>()
                .Property(e => e.Opis)
                .IsUnicode(false);

            modelBuilder.Entity<Posao>()
                .Property(e => e.Naziv)
                .IsUnicode(false);

            modelBuilder.Entity<Posao>()
                .HasMany(e => e.Stavka_radnog_naloga)
                .WithRequired(e => e.Posao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Predatnica>()
                .Property(e => e.Biljeske)
                .IsUnicode(false);

            modelBuilder.Entity<Predatnica>()
                .Property(e => e.Registracija_vozila)
                .IsUnicode(false);

            modelBuilder.Entity<Predatnica>()
                .Property(e => e.Korisnik_OIB)
                .IsUnicode(false);

            modelBuilder.Entity<Prikljucak>()
                .Property(e => e.Dostupnost)
                .IsUnicode(false);

            modelBuilder.Entity<Prikljucak>()
                .Property(e => e.Registracija)
                .IsUnicode(false);

            modelBuilder.Entity<Prikljucak>()
                .Property(e => e.Marka)
                .IsUnicode(false);

            modelBuilder.Entity<Prikljucak>()
                .Property(e => e.Registracija_vozila)
                .IsUnicode(false);

            modelBuilder.Entity<Radni_nalog>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Radni_nalog>()
                .HasMany(e => e.Stavka_radnog_naloga)
                .WithRequired(e => e.Radni_nalog)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Silos>()
                .HasMany(e => e.Izdatnica)
                .WithRequired(e => e.Silos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Silos>()
                .HasMany(e => e.Predatnica)
                .WithRequired(e => e.Silos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stavka_radnog_naloga>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Stavka_radnog_naloga>()
                .Property(e => e.Napomena)
                .IsUnicode(false);

            modelBuilder.Entity<Tip_korisnika>()
                .Property(e => e.Pozicija)
                .IsUnicode(false);

            modelBuilder.Entity<Tip_korisnika>()
                .Property(e => e.Opis_pozicije)
                .IsUnicode(false);

            modelBuilder.Entity<Tip_korisnika>()
                .HasMany(e => e.Korisnik)
                .WithRequired(e => e.Tip_korisnika)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Uzgojna_kultura>()
                .Property(e => e.Naziv)
                .IsUnicode(false);

            modelBuilder.Entity<Uzgojna_kultura>()
                .HasMany(e => e.Polje)
                .WithRequired(e => e.Uzgojna_kultura)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Uzgojna_kultura>()
                .HasMany(e => e.Silos)
                .WithRequired(e => e.Uzgojna_kultura)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vozilo>()
                .Property(e => e.Registracija)
                .IsUnicode(false);

            modelBuilder.Entity<Vozilo>()
                .Property(e => e.Vrsta)
                .IsUnicode(false);

            modelBuilder.Entity<Vozilo>()
                .Property(e => e.Marka)
                .IsUnicode(false);

            modelBuilder.Entity<Vozilo>()
                .Property(e => e.Opis)
                .IsUnicode(false);

            modelBuilder.Entity<Vozilo>()
                .Property(e => e.Dostupnost)
                .IsUnicode(false);

            modelBuilder.Entity<Vozilo>()
                .HasMany(e => e.Izdatnica)
                .WithRequired(e => e.Vozilo)
                .HasForeignKey(e => e.Registracija_vozila)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vozilo>()
                .HasMany(e => e.Predatnica)
                .WithRequired(e => e.Vozilo)
                .HasForeignKey(e => e.Registracija_vozila)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vozilo>()
                .HasMany(e => e.Prikljucak)
                .WithOptional(e => e.Vozilo)
                .HasForeignKey(e => e.Registracija_vozila);

            modelBuilder.Entity<Vrsta_prikljucka>()
                .Property(e => e.Naziv)
                .IsUnicode(false);

            modelBuilder.Entity<Vrsta_prikljucka>()
                .Property(e => e.Opis)
                .IsUnicode(false);

            modelBuilder.Entity<Vrsta_prikljucka>()
                .HasMany(e => e.Prikljucak)
                .WithRequired(e => e.Vrsta_prikljucka)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vrsta_stoke>()
                .Property(e => e.Naziv)
                .IsUnicode(false);

            modelBuilder.Entity<Vrsta_stoke>()
                .HasMany(e => e.Vrsta_stoke_farma)
                .WithRequired(e => e.Vrsta_stoke)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vrsta_stoke_farma>()
                .HasMany(e => e.Evidencija_stoke_farma)
                .WithRequired(e => e.Vrsta_stoke_farma)
                .WillCascadeOnDelete(false);
        }
    }
}
