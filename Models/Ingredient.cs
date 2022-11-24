namespace la_mia_pizzeria.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<Pizza>? Pizzas { get; set; }
    }
}
