using DMA_BLL.Interfaces;
using DMA_BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DMA_DAL.Repos
{
	public class TableRepos : ITableRepos
	{
		private static readonly List<Table> _tables = new();
		private static int _nextId = 1;
		private readonly ApplicationDbContext _context;

		public TableRepos(ApplicationDbContext context)
		{
			_context = context;
		}

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
		public async Task<IEnumerable<Table>> GetAllTablesAsync()
		{
			return await _context.Tables.ToListAsync();
		}
		public Task<Table?> GetTableByCodeAsync(string code)
		{
			var table = _tables.FirstOrDefault(t => t.UniqueCode == code);
			return Task.FromResult(table);
		}
	}
}
