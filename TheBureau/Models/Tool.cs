namespace TheBureau
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tool")]
    public partial class Tool
    {
        public int id { get; set; }

        [Required]
        [StringLength(30)]
        public string name { get; set; }

        public int stage { get; set; }
    }
}
