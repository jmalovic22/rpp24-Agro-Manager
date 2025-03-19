namespace EntitiesLayer.Entiteti
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
     

    [Table("Polje")]
    public partial class Polje
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Polje()
        {
            Radni_nalog = new HashSet<Radni_nalog>();
        }

        [Key]
        public int Polje_id { get; set; }

        [Required]
        [StringLength(50)]
        public string Ime_polja { get; set; }

        [Required]
        [StringLength(60)]
        public string Lokacija { get; set; }

        public int Velicina { get; set; }

        [Required]
        [StringLength(50)]
        public string Tip_tla { get; set; }

        public int Poduzece_id { get; set; }

        public int Uzgojna_kultura_id { get; set; }

        [JsonIgnore]
        public virtual Poduzece Poduzece { get; set; }

        public virtual Uzgojna_kultura Uzgojna_kultura { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Radni_nalog> Radni_nalog { get; set; }
    }
}
