
namespace TheBureau
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public partial class User
    {
        public int id { get; set; }

        [Required]
        [StringLength(20)]
        public string login { get; set; }

        [Required]
        [StringLength(70)]
        public string password { get; set; }

        public int role { get; set; }

        public virtual Role Role1 { get; set; }
    }
}
