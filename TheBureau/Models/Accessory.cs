namespace TheBureau.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Accessory")]
    public partial class Accessory
    {
        public int id { get; set; }

        [Required]
        [StringLength(2)]
        public string equipmentId { get; set; }

        [Required]
        [StringLength(70)]
        public string name { get; set; }

        public decimal price { get; set; }

        public virtual Equipment Equipment { get; set; }
    }
}
