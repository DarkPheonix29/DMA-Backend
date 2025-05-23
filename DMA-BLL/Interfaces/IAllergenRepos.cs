using DMA_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMA_BLL.Interfaces
{
	public interface IAllergenRepos
	{
		Task<IEnumerable<Allergen>> GetAllAsync();
		Task<Allergen> AddAsync(Allergen allergen);
	}
}
