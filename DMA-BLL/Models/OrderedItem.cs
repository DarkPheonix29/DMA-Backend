using DMA_BLL.Models;
using System.ComponentModel.DataAnnotations;

public class OrderedItem
{
	[Key]
	public int OrderItemId { get; set; }

	public int OrderId { get; set; }  // Foreign Key to Order

	public int DishId { get; set; }  // Foreign Key to Dish

	public int Quantity { get; set; }
	// Navigation property to Order
	public virtual Order Order { get; set; }

	// Navigation property to Dish
	public virtual Dish Dish { get; set; }
}
