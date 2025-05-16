using DMA_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMA_BLL.Interfaces
{
    public interface IDishRepos
    {
		Task AddDishAsync(Dish dish);
		Task<IEnumerable<Dish>> GetAllDishesAsync();
		Task<Dish?> GetDishByIdAsync(int id); // Nullable return type
		Task<bool> UpdateDishAsync(Dish dish);
		Task<bool> DeleteDishAsync(int id);
		Task<List<Category>> GetCategoriesByIdsAsync(List<int> ids);
		Task<List<Allergen>> GetAllergensByIdsAsync(List<int> ids);

	}
}
