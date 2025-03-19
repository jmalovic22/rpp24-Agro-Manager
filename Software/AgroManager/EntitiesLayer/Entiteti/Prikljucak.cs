namespace EntitiesLayer.Entiteti
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
     

    [Table("Prikljucak")]
    public partial class Prikljucak
    {
        [Key]
        public int Prikljucak_id { get; set; }

        [Required]
        [StringLength(15)]
        public string Dostupnost { get; set; }

        [Required]
        [StringLength(15)]
        public string Registracija { get; set; }

        [Required]
        [StringLength(50)]
        public string Marka { get; set; }

        [StringLength(20)]
        public string Registracija_vozila { get; set; }

        public int Vrsta_prikljucka_id { get; set; }

        public int Poduzece_id { get; set; }

        [JsonIgnore]
        public virtual Poduzece Poduzece { get; set; }

        [JsonIgnore]
        public virtual Vozilo Vozilo { get; set; }

        public virtual Vrsta_prikljucka Vrsta_prikljucka { get; set; }
    }
}
