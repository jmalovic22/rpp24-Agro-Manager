namespace EntitiesLayer.Entiteti
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
     

    public partial class Vrsta_stoke
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vrsta_stoke()
        {
            Vrsta_stoke_farma = new HashSet<Vrsta_stoke_farma>();
        }
        
        [Key]
        public int Vrsta_stoke_id { get; set; }

        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Vrsta_stoke_farma> Vrsta_stoke_farma { get; set; }
    }
}
