using DMA_BLL;
using DMA_BLL.Interfaces;

namespace DMA_DAL
{
	public class TableRepos : ITableRepository
	{
		private static readonly List<Table> _tables = new();
		private static int _nextId = 1;

		public Task<Table> CreateTableAsync(string name)
		{
			var table = new Table
			{
				Id = _nextId++,
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
