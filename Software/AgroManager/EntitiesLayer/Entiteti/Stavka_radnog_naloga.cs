namespace EntitiesLayer.Entiteti
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
     

    public partial class Stavka_radnog_naloga
    {
        [Key]
        public int Stavka_radnog_naloga_id { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        public int Razina_prioriteta { get; set; }

        [StringLength(100)]
        public string Napomena { get; set; }

        public int Radni_nalog_id { get; set; }

        public int Posao_id { get; set; }

        public virtual Posao Posao { get; set; }

        [JsonIgnore]
        public virtual Radni_nalog Radni_nalog { get; set; }
    }
}
