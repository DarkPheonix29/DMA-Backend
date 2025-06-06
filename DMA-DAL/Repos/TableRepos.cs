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

        public async Task MoveOrdersToTableAsync(int fromTableId, int toTableId)
        {
            if (fromTableId == toTableId)
                return;
            //Hier haal ik de tafels op om te controleren of ze bestaan
            var fromTable = await _context.Tables.FindAsync(fromTableId);
            var toTable = await _context.Tables.FindAsync(toTableId);

            //Dit gooit een fout als de tafels niet bestaan
            if (fromTable == null || toTable == null)
                throw new ArgumentException("Eén of beide tafels bestaan niet.");

            //Hier haal ik de orders op die verplaatst moeten worden
            var ordersToMove = await _context.Orders
                .Where(o => o.TableId == fromTableId)
                .ToListAsync();

            //Hier pas ik de TableId van de orders aan naar de nieuwe tafel
            foreach (var order in ordersToMove)
            {
                order.TableId = toTableId;
            }

            await _context.SaveChangesAsync();
        }
    }
}
