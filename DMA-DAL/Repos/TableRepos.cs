using DMA_BLL.Interfaces;
using DMA_BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DMA_DAL.Repos
{
	public class TableRepos : ITableRepos
	{
		private readonly ApplicationDbContext _context;

		public TableRepos(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Table> CreateTableAsync(string name)
		{
			var table = new Table
			{
				Name = name,
				UniqueCode = Guid.NewGuid().ToString() // assuming you want a unique code
			};

			_context.Tables.Add(table);
			await _context.SaveChangesAsync();

			return table;
		}

		public async Task<IEnumerable<Table>> GetAllTablesAsync()
		{
			return await _context.Tables.ToListAsync();
		}
		public async Task<Table?> GetTableByCodeAsync(string code)
		{
			return await _context.Tables.FirstOrDefaultAsync(t => t.UniqueCode == code);
		}

        public async Task MoveOrdersToAnotherTableAsync(int fromTableId, int toTableId)
        {
            var fromTable = await _context.Tables.FindAsync(fromTableId);
            var toTable = await _context.Tables.FindAsync(toTableId);

            if (fromTable == null || toTable == null)
                throw new ArgumentException("Een van de tafels bestaat niet.");

            var ordersToMove = await _context.Orders
                .Where(o => o.TableId == fromTableId)
                .ToListAsync();

            foreach (var order in ordersToMove)
            {
                order.TableId = toTableId;
            }

            await _context.SaveChangesAsync();
        }
        public async Task<Table?> GetTableByNameAsync(string name)
        {
            return await _context.Tables
                .FirstOrDefaultAsync(t => t.Name == name);
        }


    }
}
