using DMA_BLL.Models;
using DMA_BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMA_DAL.Repos
{
	public class DishRepos : IDishRepos
	{
		private readonly ApplicationDbContext _context;

		public DishRepos(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task AddDishAsync(Dish dish)
		{
			await _context.Dishes.AddAsync(dish);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Dish>> GetAllDishesAsync()
		{
			return await _context.Dishes.ToListAsync();
		}

		public async Task<Dish?> GetDishByIdAsync(int id) // Nullable return type
		{
			return await _context.Dishes.FirstOrDefaultAsync(d => d.DishID == id);
		}

		public async Task<bool> UpdateDishAsync(Dish dish)
		{
			var existingDish = await _context.Dishes.FindAsync(dish.DishID);
			if (existingDish == null) return false;

			_context.Entry(existingDish).CurrentValues.SetValues(dish);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteDishAsync(int id)
		{
			var dish = await _context.Dishes.FirstOrDefaultAsync(d => d.DishID == id);
			if (dish == null) return false;

			_context.Dishes.Remove(dish);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
