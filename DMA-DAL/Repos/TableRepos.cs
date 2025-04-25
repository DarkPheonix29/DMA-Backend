using DMA_BLL.Interfaces;
using DMA_BLL.Models;

namespace DMA_DAL.Repos
{
	public class TableRepos : ITableRepos
	{
		private static readonly List<Table> _tables = new();
		private static int _nextId = 1;

		public Task<Table> CreateTableAsync(string name)
		{
			var table = new Table
			{
				TableId = _nextId++,
				Name = name
			};
			_tables.Add(table);
			return Task.FromResult(table);
		}

		public Task<Table?> GetTableByCodeAsync(string code)
		{
			var table = _tables.FirstOrDefault(t => t.UniqueCode == code);
			return Task.FromResult(table);
		}
	}
}
