namespace EntitiesLayer.Entiteti
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
     

    [Table("Vozilo")]
    public partial class Vozilo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vozilo()
        {
            Izdatnica = new HashSet<Izdatnica>();
            Predatnica = new HashSet<Predatnica>();
            Prikljucak = new HashSet<Prikljucak>();
        }

        [Key]
        [StringLength(20)]
        public string Registracija { get; set; }

        [Required]
        [StringLength(50)]
        public string Vrsta { get; set; }

        [Required]
        [StringLength(50)]
        public string Marka { get; set; }

        public int Jacina_motora_u_KS { get; set; }

        [Required]
        [StringLength(1000)]
        public string Opis { get; set; }

        [Required]
        [StringLength(15)]
        public string Dostupnost { get; set; }

        public int Poduzece_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Izdatnica> Izdatnica { get; set; }

        [JsonIgnore]
        public virtual Poduzece Poduzece { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Predatnica> Predatnica { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<Prikljucak> Prikljucak { get; set; }
    }
}
