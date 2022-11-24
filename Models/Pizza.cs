
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(25)]
        public string Name { get; set; }


        [Column(TypeName="text")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(1000)]
        //[descriptionMinimumWords]
        public string Description { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(200)]
        public string Image { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public double Price { get; set; }

        //FK

        public int? CategoryID { get; set; }
        public Category? Category { get; set; }

        public List<Ingredient>? Ingredients { get; set; }
        public Pizza()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ",";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        }
        public Pizza (string name, string description, string image, double price)
        {

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ",";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;


            Name = name;
            Description = description;
            Image = image;
            Price = price;
        }
    }
}
