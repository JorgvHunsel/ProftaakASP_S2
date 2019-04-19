using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProftaakASP_S2.Models
{
    public class ReviewViewModel
    {
        public int ReviewId { get; set; }
        [Required]
        public int VolunteerId { get; set; }
        [Required]
        public int CareRecipientId { get; set; }
        public string Review { get; set; }
        [Required]
        public int starAmount { get; set; }
    }
}
