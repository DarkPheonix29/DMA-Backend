using DMA_BLL;
using Microsoft.AspNetCore.Mvc;

namespace DMA_Backend
{
	[ApiController]
	[Route("api/tables")]
	public class TableController : ControllerBase
	{
		private readonly TableServices _tableService;

		public TableController(TableServices tableService)
		{
			_tableService = tableService ?? throw new ArgumentNullException(nameof(tableService));
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateTable([FromBody] string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return BadRequest("Table name cannot be empty.");
			}

			var table = await _tableService.CreateTableAsync(name);
			var qrCodeImage = await _tableService.GetQrCodeForTable(table.UniqueCode);

			return Ok(new
			{
				Table = table,
				QrCode = Convert.ToBase64String(qrCodeImage)
			});
		}

		[HttpGet("{code}/qrcode")]
		public async Task<IActionResult> GetQrCodeForTable(string code)
		{
			if (string.IsNullOrWhiteSpace(code))
			{
				return BadRequest("Code cannot be empty.");
			}

			var qrCodeImage = await _tableService.GetQrCodeForTable(code);
			if (qrCodeImage == null) return NotFound("Table not found.");

			return File(qrCodeImage, "image/png");
		}
	}
}