using DMA_BLL.Models;

namespace DMA_BLL.Interfaces
{
	public interface ITableRepos
	{
		Task<Table> CreateTableAsync(string name);
		Task<Table?> GetTableByCodeAsync(string code);
	}
}
