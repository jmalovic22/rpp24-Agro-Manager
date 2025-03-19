namespace EntitiesLayer.Entiteti
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
     

    [Table("Posao")]
    public partial class Posao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Posao()
        {
            Stavka_radnog_naloga = new HashSet<Stavka_radnog_naloga>();
        }

        [Key]
        public int Posao_id { get; set; }

        [StringLength(1000)]
        public string Opis { get; set; }

        [StringLength(50)]
        public string Naziv { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Stavka_radnog_naloga> Stavka_radnog_naloga { get; set; }
    }
}
