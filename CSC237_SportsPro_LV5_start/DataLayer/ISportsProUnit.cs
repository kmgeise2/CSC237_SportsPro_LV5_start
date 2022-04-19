
namespace CSC237_SportsPro_LV5_start.Models
{
    public interface ISportsProUnit
    {
        IRepository<Product> Products { get; }
        IRepository<Technician> Technicians { get; }
        IRepository<Customer> Customers { get; }
        IRepository<Country> Countries { get; }
        IRepository<Registration> Registrations { get; }
        IRepository<Incident> Incidents { get; }

        void Save();
    }
}
