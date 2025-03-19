namespace EntitiesLayer.Entiteti
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
     

    [Table("Poduzece")]
    public partial class Poduzece
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Poduzece()
        {
            Farma = new HashSet<Farma>();
            Korisnik = new HashSet<Korisnik>();
            Polje = new HashSet<Polje>();
            Vozilo = new HashSet<Vozilo>();
        }

        [Key]
        public int Poduzece_id { get; set; }

        [StringLength(50)]
        public string Naziv { get; set; }

        [StringLength(60)]
        public string Vlasnik { get; set; }

        [StringLength(15)]
        public string Telefon { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Farma> Farma { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Korisnik> Korisnik { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Polje> Polje { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Vozilo> Vozilo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Prikljucak> Prikljucak { get; set; }
    }
}
