namespace EntitiesLayer.Entiteti
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;

    public partial class Evidencija_stoke_farma
    {
        [Key]
        public int Evidencija_stoke_farma_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Datum_promjene { get; set; }

        public int Kolicina_promjene { get; set; }

        [StringLength(1000)]
        public string Napomena { get; set; }

        public int Vrsta_stoke_farma_id { get; set; }

        public virtual Vrsta_stoke_farma Vrsta_stoke_farma { get; set; }
    }
}
