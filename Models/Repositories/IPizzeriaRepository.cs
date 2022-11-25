namespace la_mia_pizzeria.Models.Repositories
{
    public interface IPizzeriaRepository
    {
        List<Pizza> All();
        Pizza GetById(int id);
        void Create(Pizza pizzaToCreate, List<int> SelectedIngredients);
        void Update(Pizza pizzaToUpdate, Pizza formData, List<int>? SelectedIngredients);
        void Delete(Pizza pizzaToDelete);
    }
}
