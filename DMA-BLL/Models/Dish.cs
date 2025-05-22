using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMA_BLL.Models
{
	public class Dish
	{
		[Key]
		public int DishID { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public bool IsAvailable { get; set; }

		// Many-to-many navigation properties
		public ICollection<Category> Categories { get; set; }
		public ICollection<Allergen> Allergens { get; set; }
	}
}
