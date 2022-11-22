using la_mia_pizzeria.Data;
using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria.Controllers
{
    public class PizzaController : Controller
    {
        PizzaDbContext db;

        public PizzaController() : base()
        {
            db = new PizzaDbContext();
        }
        public IActionResult Index()
        {
       
           List<Pizza> pizzaList = db.Pizzas.ToList();

            return View(pizzaList);
        }
        public IActionResult Details(int id)
        {
            Pizza singlePizza = db.Pizzas.Where(p => p.Id == id).FirstOrDefault();

            return View(singlePizza);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza newPizza)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            db.Pizzas.Add(newPizza);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            Pizza pizzaToSearch = db.Pizzas.Where(pizzaToSearch => pizzaToSearch.Id == id).FirstOrDefault();

            if (pizzaToSearch == null)
                return NotFound();

            //return View() --> non funziona perchè non ha la memoria della post
            return View(pizzaToSearch);
        }
        [HttpPost]
        public IActionResult Update(Pizza pizzaToUpdate)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            db.Pizzas.Update(pizzaToUpdate);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza pizzaToDelete = db.Pizzas.Where(pizzaToDelete => pizzaToDelete.Id == id).FirstOrDefault();

            if (pizzaToDelete == null)
            {
                return NotFound();
            }

            db.Pizzas.Remove(pizzaToDelete);
            db.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}
