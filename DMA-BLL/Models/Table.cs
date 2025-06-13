using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMA_BLL.Models
{
	public class Table
	{
		[Key]
		public int TableId { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
		public string UniqueCode { get; set; } = Guid.NewGuid().ToString();

        //al de gekoppelde orders
        public List<Order> Orders { get; set; } = new();

        [NotMapped] // Deze eigenschap wordt niet in de database opgeslagen
        public int ToTableId { get; set; }
    }
}
