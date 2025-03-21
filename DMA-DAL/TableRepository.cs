
namespace DMA_DAL
{
    public class TableRepository
    {
        private static List<Table> _tables = new List<Table>();
        private static int _nextId = 1;

        public Task<Table> CreateTableAsync(string name)  
        {
            var table = new Table { Id = _nextId++, Name = name };
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