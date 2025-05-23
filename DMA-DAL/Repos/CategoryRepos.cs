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
	public class CategoryRepos : ICategoryRepos
	{
		private readonly ApplicationDbContext _context;
		public CategoryRepos(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Category>> GetAllAsync()
		{
			return await _context.Categories.ToListAsync();
		}

		public async Task<Category> AddAsync(Category category)
		{
			_context.Categories.Add(category);
			await _context.SaveChangesAsync();
			return category;
		}
	}
}
