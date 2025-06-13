using DMA_BLL.Interfaces;
using DMA_BLL.Models;

namespace DMA_BLL
{
	public class TableServices
	{
		private readonly ITableRepos _tableRepository;
		private readonly QrCodeService _qrCodeService;

		public TableServices(ITableRepos tableRepository, QrCodeService qrCodeService)
		{
			_tableRepository = tableRepository;
			_qrCodeService = qrCodeService;
		}

		public async Task<Table> CreateTableAsync(string name)
		{
			return await _tableRepository.CreateTableAsync(name);
		}

		public async Task<byte[]> GetQrCodeForTable(string code)
		{
			var table = await _tableRepository.GetTableByCodeAsync(code);
			if (table == null) return null!;

			string tableUrl = $"http://localhost:3000/menu/table/{table.UniqueCode}";
			return _qrCodeService.GenerateQrCode(tableUrl);
		}

        //delete table by Id
        public async Task<bool> DeleteTableAsync(int id)
        {
            return await _tableRepository.DeleteTableAsync(id);
        }
    }
}
