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

        //delete table by Id
        public async Task<bool> DeleteTableAsync(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null) return false;
            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
