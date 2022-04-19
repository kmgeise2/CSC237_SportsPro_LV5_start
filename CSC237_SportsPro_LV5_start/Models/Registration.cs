using System.ComponentModel.DataAnnotations;

namespace CSC237_SportsPro_LV5_start.Models
{
    public class Registration
    {
        // composite primary key and foreign keys
        [Required]
        public int CustomerID { get; set; }

        [Required]
        public int ProductID { get; set; }

        // navigation properties
        public Customer Customer { get; set; }
        public Product Product { get; set; }
    }
}
