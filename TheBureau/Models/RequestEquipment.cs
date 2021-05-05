namespace TheBureau.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequestEquipment")]
    public partial class RequestEquipment
    {
        public int id { get; set; }

        public int requestId { get; set; }

        [Required]
        [StringLength(2)]
        public string equipmentId { get; set; }

        public virtual Equipment Equipment { get; set; }

        public virtual Request Request { get; set; }
    }
}
