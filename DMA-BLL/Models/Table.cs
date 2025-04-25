using System.ComponentModel.DataAnnotations;

namespace DMA_BLL.Models
{
	public class Table
	{
		[Key]
		public int TableId { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
		public string UniqueCode { get; set; } = Guid.NewGuid().ToString();
	}
}
