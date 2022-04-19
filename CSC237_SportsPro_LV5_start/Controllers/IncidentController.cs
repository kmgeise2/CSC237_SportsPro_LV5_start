using CSC237_SportsPro_LV5_start.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CSC237_SportsPro_LV5_start.Controllers
{
    public class IncidentController : Controller
    {
        private IRepository<Incident> data { get; set; }

        public IncidentController(IRepository<Incident> rep)
        {
            data = rep;
        }

        [Route("[controller]s")]
        public IActionResult List(string filter = "all")
        {
            IncidentListViewModel model = new IncidentListViewModel
            {
                Filter = filter
            };

            var options = new QueryOptions<Incident>
            {
                Includes = "Customer, Product",
                OrderBy = i => i.DateOpened
            };

            if (filter == "unassigned")
            {
                options.Where = i => i.TechnicianID == null;
            }

            if (filter == "open")
            {
                options.Where = i => i.DateClosed == null;
            }

            IEnumerable<Incident> incidents = data.List(options);
            model.Incidents = incidents;

            return View(model);
        }

        public IActionResult Filter(string id)
        {
            return RedirectToAction("List", new { Filter = id });
        }

        [HttpGet]
        public IActionResult Add()
        {
            IncidentViewModel model = new IncidentViewModel
            {
                Incident = new Incident(),
                Action = "Add"
            };

            return View("AddEdit", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            IncidentViewModel model = new IncidentViewModel
            {
                Incident = data.Get(id),
                Action = "Edit"
            };

            return View("AddEdit", model);
        }

        [HttpPost]
        public IActionResult Save(Incident incident)
        {
            if (ModelState.IsValid)
            {
                if (incident.IncidentID == 0)
                {
                    data.Insert(incident);
                }
                else
                {
                    data.Update(incident);
                }
                data.Save();
                return RedirectToAction("List");
            }
            else
            {
                IncidentViewModel model = new IncidentViewModel
                {
                    Incident = incident
                };

                if (incident.IncidentID == 0)
                {
                    model.Action = "Add";
                }
                else
                {
                    model.Action = "Edit";
                }
                return View("AddEdit", model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var incident = data.Get(id);
            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            data.Delete(incident);
            data.Save();
            return RedirectToAction("List");
        }

    }
}