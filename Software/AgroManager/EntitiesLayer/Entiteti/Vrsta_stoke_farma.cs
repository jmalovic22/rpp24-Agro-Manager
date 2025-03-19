namespace EntitiesLayer.Entiteti
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
     

    public partial class Vrsta_stoke_farma
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vrsta_stoke_farma()
        {
            Evidencija_stoke_farma = new HashSet<Evidencija_stoke_farma>();
        }
        public override string ToString()
        {
            return $"{Vrsta_stoke.Naziv}, {Kolicina_stoke}";
        }
        [Key]
        public int Vrsta_stoke_farma_id { get; set; }

        public int Kolicina_stoke { get; set; }

        public int Farma_id { get; set; }

        public int Vrsta_stoke_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        [JsonIgnore]
        public virtual ICollection<Evidencija_stoke_farma> Evidencija_stoke_farma { get; set; }

        public virtual Farma Farma { get; set; }

        public virtual Vrsta_stoke Vrsta_stoke { get; set; }
    }
}
