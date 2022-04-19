using System;
using System.ComponentModel.DataAnnotations;

namespace CSC237_SportsPro_LV5_start.Models
{
    public class Technician
    {
        public int TechnicianID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
