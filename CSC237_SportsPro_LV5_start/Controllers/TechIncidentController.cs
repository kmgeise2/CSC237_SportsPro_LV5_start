using CSC237_SportsPro_LV5_start.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSC237_SportsPro_LV5_start.Controllers
{
    [Authorize]
    public class TechIncidentController : Controller
    {
        private ISportsProUnit data { get; set; }
        private ISession session { get; set; }

        public TechIncidentController(ISportsProUnit unit, IHttpContextAccessor accessor)
        {
            data = unit;
            session = accessor.HttpContext.Session;
        }

        [HttpGet]
        public IActionResult Get()
        {
            int techID = session.GetInt32("techID") ?? 0;
            Technician technician;
            if (techID == 0)
            {
                technician = new Technician();
            }
            else
            {
                technician = data.Technicians.Get(techID);
            }

            return View(technician);
        }

        [HttpPost]
        public IActionResult List(Technician technician)
        {
            session.SetInt32("techID", technician.TechnicianID);

            if (technician.TechnicianID == 0)
            {
                TempData["message"] = "You must select a technician.";
                return RedirectToAction("Get");
            }
            else
            {
                return RedirectToAction("List", new { id = technician.TechnicianID });
            }
        }

        [HttpGet]
        public IActionResult List(int id)
        {
            var model = new TechIncidentViewModel
            {
                Technician = data.Technicians.Get(id),

                Incidents = data.Incidents.List(new QueryOptions<Incident>
                {
                    Includes = "Customer, Product",
                    OrderBy = i => i.DateOpened,
                    WhereClauses = new WhereClauses<Incident>
                    {
                        { i => i.TechnicianID == id },
                        { i => i.DateClosed == null }
                    }
                })
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            int techID = HttpContext.Session.GetInt32("techID") ?? 0;
            var model = new TechIncidentViewModel
            {
                Technician = data.Technicians.Get(techID),

                Incident = data.Incidents.Get(new QueryOptions<Incident>
                {
                    Includes = "Customer, Product",
                    Where = i => i.IncidentID == id
                })
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(IncidentViewModel model)
        {
            Incident i = data.Incidents.Get(model.Incident.IncidentID);
            i.Description = model.Incident.Description;
            i.DateClosed = model.Incident.DateClosed;

            data.Incidents.Update(i);
            data.Save();

            int techID = HttpContext.Session.GetInt32("techID") ?? 0;
            return RedirectToAction("List", new { id = techID });
        }

    }
}