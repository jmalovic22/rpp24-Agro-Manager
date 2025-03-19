namespace EntitiesLayer.Entiteti
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
     

    public partial class Silos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Silos()
        {
            Izdatnica = new HashSet<Izdatnica>();
            Predatnica = new HashSet<Predatnica>();
        }

        [Key]
        public int Silos_id { get; set; }

        public int Popunjenost { get; set; }

        public int Kapacitet { get; set; }

        public int Dostupnost { get; set; }

        public int Farma_id { get; set; }

        public int Uzgojna_kultura_id { get; set; }

        public virtual Farma Farma { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Izdatnica> Izdatnica { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Predatnica> Predatnica { get; set; }

        public virtual Uzgojna_kultura Uzgojna_kultura { get; set; }
    }
}
