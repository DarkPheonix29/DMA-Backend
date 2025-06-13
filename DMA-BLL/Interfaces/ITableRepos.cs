using DMA_BLL.Models;

namespace DMA_BLL.Interfaces
{
	public interface ITableRepos
	{
		Task<Table> CreateTableAsync(string name);
		Task<Table?> GetTableByCodeAsync(string code);
		Task<IEnumerable<Table>> GetAllTablesAsync();
        Task MoveOrdersToAnotherTableAsync(int fromTableId, int toTableId);
        Task<Table?> GetTableByNameAsync(string name);

    }
}
