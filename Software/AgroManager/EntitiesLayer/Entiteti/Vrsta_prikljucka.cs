namespace EntitiesLayer.Entiteti
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
     

    public partial class Vrsta_prikljucka
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vrsta_prikljucka()
        {
            Prikljucak = new HashSet<Prikljucak>();
        }

        [Key]
        public int Vrsta_prikljucka_id { get; set; }

        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }

        [Required]
        [StringLength(1000)]
        public string Opis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Prikljucak> Prikljucak { get; set; }
    }
}
