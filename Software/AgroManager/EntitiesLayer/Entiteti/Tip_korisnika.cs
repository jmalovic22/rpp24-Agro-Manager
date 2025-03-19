namespace EntitiesLayer.Entiteti
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
     

    public partial class Tip_korisnika
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tip_korisnika()
        {
            Korisnik = new HashSet<Korisnik>();
        }

        [Key]
        public int Tip_korisnika_id { get; set; }

        [Required]
        [StringLength(50)]
        public string Pozicija { get; set; }

        public int Razina_prava { get; set; }

        [Required]
        [StringLength(100)]
        public string Opis_pozicije { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Korisnik> Korisnik { get; set; }
    }
}
