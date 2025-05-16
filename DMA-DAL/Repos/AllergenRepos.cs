using DMA_BLL.Interfaces;
using DMA_BLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMA_DAL.Repos
{
	public class AllergenRepos : IAllergenRepos
	{
		private readonly ApplicationDbContext _context;
		public AllergenRepos(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Allergen>> GetAllAsync()
		{
			return await _context.Allergens.ToListAsync();
		}

		public async Task<Allergen> AddAsync(Allergen allergen)
		{
			_context.Allergens.Add(allergen);
			await _context.SaveChangesAsync();
			return allergen;
		}
	}
}
