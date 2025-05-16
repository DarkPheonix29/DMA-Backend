using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMA_BLL.Models
{
	public class Allergen
	{
		public int AllergenId { get; set; }
		public string Name { get; set; }

		public ICollection<Dish> Dishes { get; set; }
	}
}
