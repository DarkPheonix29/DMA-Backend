using System.ComponentModel.DataAnnotations;

namespace DMA_BLL.Models
{
	public class Dish
	{
		[Key]
		public int DishID { get; set; }
		public string Category { get; set; }
		public string Allergens { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public bool IsAvailable { get; set; }
	}
}
