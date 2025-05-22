using DMA_BLL.Interfaces;
using DMA_BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DMA_Backend.Controllers
{
	[ApiController]
	[Route("api/allergens")]
	public class AllergenController : ControllerBase
	{
		private readonly IAllergenRepos _allergenRepo;

		public AllergenController(IAllergenRepos allergenRepo)
		{
			_allergenRepo = allergenRepo;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var allergens = await _allergenRepo.GetAllAsync();
			return Ok(allergens);
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] Allergen allergen)
		{
			if (string.IsNullOrWhiteSpace(allergen.Name))
				return BadRequest("Allergen name is required");

			var added = await _allergenRepo.AddAsync(allergen);
			return CreatedAtAction(nameof(GetAll), new { id = added.AllergenId }, added);
		}
	}
}
