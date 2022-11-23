namespace la_mia_pizzeria.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        List<Pizza> Pizza { get; set; }

        public Category()
        {

        }
    }
}
  
