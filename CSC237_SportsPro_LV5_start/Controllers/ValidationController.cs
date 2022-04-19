using CSC237_SportsPro_LV5_start.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSC237_SportsPro_LV5_start.Controllers
{
    public class ValidationController : Controller
    {

        public JsonResult CheckEmail(string email, int customerID, [FromServices] IRepository<Customer> data)
        {
            if (customerID == 0)  // only check for new customers - don't check on edit
            {
                string msg = Check.EmailExists(data, email);
                if (!string.IsNullOrEmpty(msg))
                {
                    return Json(msg);
                }
            }

            TempData["okEmail"] = true;
            return Json(true);
        }
    }
}