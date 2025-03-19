namespace EntitiesLayer.Entiteti
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
     

    [Table("Korisnik")]
    public partial class Korisnik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Korisnik()
        {
            Izdatnica = new HashSet<Izdatnica>();
            Predatnica = new HashSet<Predatnica>();
            Radni_nalog = new HashSet<Radni_nalog>();
        }

        [Key]
        [StringLength(11)]
        public string Korisnik_OIB { get; set; }

        [Required]
        [StringLength(30)]
        public string Ime { get; set; }

        [Required]
        [StringLength(30)]
        public string Prezime { get; set; }

        [Required]
        [StringLength(50)]
        public string Korisnicko_ime { get; set; }

        [Required]
        [StringLength(255)]
        public string Lozinka { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(30)]
        public string Polozene_kategorije { get; set; }

        public int Poduzece_id { get; set; }

        public int Tip_korisnika_id { get; set; }

        [StringLength(100)]
        public string Secret { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Izdatnica> Izdatnica { get; set; }

        public virtual Poduzece Poduzece { get; set; }

        public virtual Tip_korisnika Tip_korisnika { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Predatnica> Predatnica { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Radni_nalog> Radni_nalog { get; set; }
    }
}
