using la_mia_pizzeria.Data;
using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using Microsoft.CodeAnalysis;

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
       
           List<Pizza> pizzaList = db.Pizzas.Include("Category").ToList();

            return View(pizzaList);
        }
        public IActionResult Details(int id)
        {
            Pizza singlePizza = db.Pizzas.Where(p => p.Id == id).Include("Category").FirstOrDefault();

            return View(singlePizza);
        }
        public IActionResult Create()
        {
            PizzaForm formData = new PizzaForm();

            formData.Pizza = new Pizza();
            formData.Categories = db.Categories.ToList();

            return View(formData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaForm formData)
        {
            
            if (!ModelState.IsValid)
            {
                formData.Categories = db.Categories.ToList();
                return View(formData);
            }

            db.Pizzas.Add(formData.Pizza);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            Pizza pizzaToUpdate = db.Pizzas.Where(pizzaToUpdate => pizzaToUpdate.Id == id).FirstOrDefault();

            if (pizzaToUpdate == null)
                return NotFound();
            PizzaForm formData = new PizzaForm();

            formData.Pizza = pizzaToUpdate;
            formData.Categories = db.Categories.ToList();

            return View(formData);
        }
        [HttpPost]
        public IActionResult Update(int id, PizzaForm formData)
        {

            formData.Pizza.Id = id;
            if (!ModelState.IsValid)
            {
                formData.Categories = db.Categories.ToList();
                return View(formData);
            }

            db.Pizzas.Update(formData.Pizza);
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
