using TheBureau.ViewModels;

namespace TheBureau
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            Requests = new HashSet<Request>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(20)]
        public string firstname { get; set; }

        [Required]
        [StringLength(20)]
        public string patronymic { get; set; }

        [Required]
        [StringLength(20)]
        public string surname { get; set; }

        [Required]
        [StringLength(255)]
        public string email { get; set; }

        public decimal contactNumber { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
