using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSC237_SportsPro_LV5_start.Models
{
    internal class RegistrationConfig : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> entity)
        {
            /**********************************************************
             * many-to-many relationship for Registrations table
             *********************************************************/

            // composite primary key
            entity.HasKey(r => new { r.CustomerID, r.ProductID });

            // one-to-many relationship between Customer and Registrations
            entity.HasOne(r => r.Customer)
                .WithMany(c => c.Registrations)
                .HasForeignKey(r => r.CustomerID);

            // one-to-many relationship between Product and Registrations
            entity.HasOne(r => r.Product)
                .WithMany(p => p.Registrations)
                .HasForeignKey(r => r.ProductID);
        }
    }

}
