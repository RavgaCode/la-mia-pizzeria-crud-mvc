using la_mia_pizzeria.Data;
using la_mia_pizzeria.Models;
using la_mia_pizzeria.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using Microsoft.CodeAnalysis;
using Azure;
using System.Diagnostics;


namespace la_mia_pizzeria.Controllers
{
    public class PizzaController : Controller
    {
        PizzaDbContext db;
        IDbPizzeriaRepository pizzeriaRepository;

        public PizzaController(IDbPizzeriaRepository _pizzeriaRepository) :base()
        {
            db = new PizzaDbContext();
            pizzeriaRepository = _pizzeriaRepository;   
        }
        public IActionResult Index()
        {

            List<Pizza> pizzaList = pizzeriaRepository.All();

            return View(pizzaList);
        }
        public IActionResult Details(int id)
        {
            Pizza singlePizza = pizzeriaRepository.GetById(id);
            return View(singlePizza);
        }
        public IActionResult Create()
        {
            PizzaForm formData = new PizzaForm();

            formData.Pizza = new Pizza();
            formData.Categories = db.Categories.ToList();
            formData.Ingredients = new List<SelectListItem>();

            List<Ingredient> ingredientsList = db.Ingredients.ToList();

            foreach (Ingredient ingredient in ingredientsList)
            {
                formData.Ingredients.Add(new SelectListItem(ingredient.Title, ingredient.Id.ToString()));
            }

            return View(formData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaForm formData)
        {
            
            if (!ModelState.IsValid)
            {
                formData.Categories = db.Categories.ToList();
                formData.Ingredients = new List<SelectListItem>();

                List<Ingredient> ingredientsList = db.Ingredients.ToList();

                foreach (Ingredient ingredient in ingredientsList)
                {
                    formData.Ingredients.Add(new SelectListItem(ingredient.Title, ingredient.Id.ToString()));
                }

                return View(formData);
            }

            //Aggiungo il sistema repository

            //formData.Pizza.Ingredients = new List<Ingredient>();

            //foreach (int ingredientId in formData.SelectedIngredients)
            //{
            //    Ingredient ingredient = db.Ingredients.Where(ingredient => ingredient.Id == ingredientId).FirstOrDefault();
            //    formData.Pizza.Ingredients.Add(ingredient);
            //}

            //db.Pizzas.Add(formData.Pizza);
            //db.SaveChanges();

            pizzeriaRepository.Create(formData.Pizza, formData.SelectedIngredients);

            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            Pizza pizzaToUpdate = pizzeriaRepository.GetById(id);

            if (pizzaToUpdate == null)
                return NotFound();
            PizzaForm formData = new PizzaForm();

            formData.Pizza = pizzaToUpdate;
            formData.Categories = db.Categories.ToList();
            formData.Ingredients = new List<SelectListItem>();

            List<Ingredient> ingredientsList = db.Ingredients.ToList();

            foreach (Ingredient ingredient in ingredientsList)
            {
                formData.Ingredients.Add(new SelectListItem(
                    ingredient.Title,
                    ingredient.Id.ToString(),
                    pizzaToUpdate.Ingredients.Any(i => i.Id == ingredient.Id)
                   ));
            }

            return View(formData);
        }
        [HttpPost]
        public IActionResult Update(int id, PizzaForm formData)
        {

            if (!ModelState.IsValid)
            {
                formData.Pizza.Id = id;
                formData.Categories = db.Categories.ToList();
                formData.Ingredients = new List<SelectListItem>();

                List<Ingredient> ingredientsList = db.Ingredients.ToList();

                foreach (Ingredient ingredient in ingredientsList)
                {
                    formData.Ingredients.Add(new SelectListItem(ingredient.Title, ingredient.Id.ToString()));
                }

                return View(formData);
            }


            Pizza pizzaToUpdate = pizzeriaRepository.GetById(id);   

            if (pizzaToUpdate == null)
            {
                return NotFound();
            }

            //Aggiungo il nuovo sistema repository

            //pizzaToUpdate.Name = formData.Pizza.Name;
            //pizzaToUpdate.Description = formData.Pizza.Description;
            //pizzaToUpdate.Image = formData.Pizza.Image;
            //pizzaToUpdate.CategoryID = formData.Pizza.CategoryID;

            //pizzaToUpdate.Ingredients.Clear();

            //if (formData.SelectedIngredients == null)
            //{
            //    formData.SelectedIngredients = new List<int>();
            //}

            //foreach (int ingredientId in formData.SelectedIngredients)
            //{
            //    Ingredient ingredient = db.Ingredients.Where(i => i.Id == ingredientId).FirstOrDefault();
            //    pizzaToUpdate.Ingredients.Add(ingredient);
            //}

            //db.SaveChanges();

            pizzeriaRepository.Update(pizzaToUpdate, formData.Pizza, formData.SelectedIngredients);

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza pizzaToDelete = pizzeriaRepository.GetById(id);

            if (pizzaToDelete == null)
            {
                return NotFound();
            }

            pizzeriaRepository.Delete(pizzaToDelete);


            return RedirectToAction("Index");
        }
    }
}
