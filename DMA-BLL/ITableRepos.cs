namespace DMA_BLL.Interfaces
{
	public interface ITableRepository
	{
		Task<Table> CreateTableAsync(string name);
		Task<Table?> GetTableByCodeAsync(string code);
	}
}
