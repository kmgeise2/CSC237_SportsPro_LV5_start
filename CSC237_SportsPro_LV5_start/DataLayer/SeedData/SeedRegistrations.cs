using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSC237_SportsPro_LV5_start.Models
{
    internal class SeedRegistrations : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> entity)
        {
            entity.HasData(
                 new Registration
                 {
                     CustomerID = 1002,
                     ProductID = 1
                 },
                 new Registration
                 {
                     CustomerID = 1002,
                     ProductID = 3
                 },
                 new Registration
                 {
                     CustomerID = 1010,
                     ProductID = 2
                 }
            );
        }
    }
}