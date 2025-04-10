namespace DMA_BLL.Models
{
	public class Table
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string UniqueCode { get; set; } = Guid.NewGuid().ToString();
	}
}
