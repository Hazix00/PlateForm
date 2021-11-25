using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlateForm.Core.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public IList<Ticket> Tickets { get; set; }
    }
}
