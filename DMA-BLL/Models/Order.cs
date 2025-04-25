using DMA_BLL.Models;
using System.ComponentModel.DataAnnotations;

public class Order
{
	[Key]
	public int OrderId { get; set; }

	[Required]
	public string CustomerName { get; set; }

	[Required]
	public TimeOnly OrderTime { get; set; }

	public string Status { get; set; } = "Pending";  // Possible values: Pending, Completed, Cancelled

	public decimal TotalAmount { get; set; }

	// Navigation property for related OrderItems
	public List<OrderedItem> OrderItems { get; set; } = new List<OrderedItem>();
}
