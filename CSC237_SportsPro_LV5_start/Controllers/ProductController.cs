using CSC237_SportsPro_LV5_start.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSC237_SportsPro_LV5_start.Controllers
{
    public class ProductController : Controller
    {
        private IRepository<Product> data { get; set; }

        public ProductController(IRepository<Product> rep)
        {
            data = rep;
        }

        [Route("[controller]s")]
        public ViewResult List()
        {
            var products = this.data.List(new QueryOptions<Product>
            {
                OrderBy = p => p.ReleaseDate
            });
            return View(products);
        }

        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddEdit", new Product());
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var product = data.Get(id);
            return View("AddEdit", product);
        }

        [HttpPost]
        public IActionResult Save(Product product)
        {
            string message;
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {
                    data.Insert(product);
                    message = product.Name + " was added.";
                }
                else
                {
                    data.Update(product);
                    message = product.Name + " was updated.";
                }
                data.Save();
                TempData["message"] = message;
                return RedirectToAction("List");
            }
            else
            {
                if (product.ProductID == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Action = "Edit";
                }
                return View(product);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var product = data.Get(id);
            return View(product);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Product product)
        {
            var p = data.Get(product.ProductID);  // read from DB to get product name
            data.Delete(p);
            data.Save();
            TempData["message"] = p.Name + " was deleted.";
            return RedirectToAction("List");
        }
    }
}