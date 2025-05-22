using DMA_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMA_BLL.Interfaces
{
	public interface ICategoryRepos
	{
		Task<IEnumerable<Category>> GetAllAsync();
		Task<Category> AddAsync(Category category);
	}
}
