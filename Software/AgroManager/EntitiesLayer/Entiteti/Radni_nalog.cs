namespace EntitiesLayer.Entiteti
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
     

    public partial class Radni_nalog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Radni_nalog()
        {
            Stavka_radnog_naloga = new HashSet<Stavka_radnog_naloga>();
            Korisnik = new HashSet<Korisnik>();
        }

        [Key]
        public int Radni_nalog_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Datum_kreiranja { get; set; }

        [Column(TypeName = "date")]
        public DateTime Zavrsni_rok { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Datum_zavrsetka { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        public int Polje_id { get; set; }

        public virtual Polje Polje { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<Stavka_radnog_naloga> Stavka_radnog_naloga { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Korisnik> Korisnik { get; set; }
    }
}
