using DMA_BLL;
using DMA_BLL.Interfaces;
using DMA_BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DMA_Backend
{
	[ApiController]
	[Route("api/tables")]
	public class TableController : ControllerBase
	{
		private readonly TableServices _tableService;
		private readonly ITableRepos _tableRepos;

		public TableController(TableServices tableService,ITableRepos tableRepos)
		{
			_tableService = tableService ?? throw new ArgumentNullException(nameof(tableService));
			_tableRepos = tableRepos;
		}

		[HttpGet("{uniqueCode}")]
		public async Task<IActionResult> GetTableByCode(string uniqueCode)
		{
			var table = await _tableRepos.GetTableByCodeAsync(uniqueCode);
			if (table == null) return NotFound();
			return Ok(table);
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

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Table>>> GetAllTables()
		{
			var tables = await _tableRepos.GetAllTablesAsync();
			return Ok(tables);
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

        [HttpPost("move-orders")]
        public async Task<IActionResult> MoveOrders([FromBody] MoveOrdersRequest request)
        {
            //Hier wordt er gechekt of de request geldig is 
            if (request.FromTableId == request.ToTableId)
            {
                return BadRequest("FromTableId and ToTableId cannot be the same.");
            }

            try
            {
                //Hier roep ik de service aan om de orders te verplaatsen
                await _tableService.MoveOrdersToTableAsync(request.FromTableId, request.ToTableId);
                return Ok(new { message = "Orders moved successfully." });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}