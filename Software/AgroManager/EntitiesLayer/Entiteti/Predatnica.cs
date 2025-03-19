namespace EntitiesLayer.Entiteti
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
     

    [Table("Predatnica")]
    public partial class Predatnica
    {
        [Key]
        public int Predatnica_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Datum { get; set; }

        public int Kolicina_u_kg { get; set; }

        [Required]
        [StringLength(1000)]
        public string Biljeske { get; set; }

        public int Silos_id { get; set; }

        [Required]
        [StringLength(20)]
        public string Registracija_vozila { get; set; }

        [Required]
        [StringLength(11)]
        public string Korisnik_OIB { get; set; }

        public virtual Korisnik Korisnik { get; set; }

        public virtual Silos Silos { get; set; }

        public virtual Vozilo Vozilo { get; set; }
    }
}
