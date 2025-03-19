namespace EntitiesLayer.Entiteti
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;
     

    [Table("Farma")]
    public partial class Farma
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Farma()
        {
            Silos = new HashSet<Silos>();
            Vrsta_stoke_farma = new HashSet<Vrsta_stoke_farma>();
        }
        override public string ToString()
        {
            return Lokacija;
        }
        [Key]
        public int Farma_id { get; set; }

        [Required]
        [StringLength(60)]
        public string Lokacija { get; set; }

        public int Povrsina { get; set; }

        public int Broj_zaposlenih { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }

        public int? Poduzece_id { get; set; }

        [JsonIgnore]
        public virtual Poduzece Poduzece { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Silos> Silos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Vrsta_stoke_farma> Vrsta_stoke_farma { get; set; }
    }
}
