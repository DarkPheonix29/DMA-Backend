using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DMA_BLL.Models
{
	public class Category
	{
		public int CategoryId { get; set; }
		public string Name { get; set; }

		[JsonIgnore]
		public ICollection<Dish> Dishes { get; set; } = new List<Dish>();
	}
}
