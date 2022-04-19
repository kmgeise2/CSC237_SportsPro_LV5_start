using System.Linq;

namespace CSC237_SportsPro_LV5_start.Models
{
    public static class Check
    {
        public static string EmailExists(IRepository<Customer> data, string email)
        {
            string msg = "";
            if (!string.IsNullOrEmpty(email))
            {
                var customer = data.Get(new QueryOptions<Customer>
                {
                    Where = c => c.Email.ToLower() == email.ToLower()
                });
                if (customer != null)
                    msg = "Email address already in use.";
            }
            return msg;
        }
    }
}