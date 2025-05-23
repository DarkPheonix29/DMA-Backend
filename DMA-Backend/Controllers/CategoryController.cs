using DMA_BLL.Interfaces;
using DMA_BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DMA_Backend.Controllers
{
	[ApiController]
	[Route("api/categories")]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryRepos _categoryRepo;

		public CategoryController(ICategoryRepos categoryRepo)
		{
			_categoryRepo = categoryRepo;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var categories = await _categoryRepo.GetAllAsync();
			return Ok(categories);
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] Category category)
		{
			if (string.IsNullOrWhiteSpace(category.Name))
				return BadRequest("Category name is required");

			var added = await _categoryRepo.AddAsync(category);
			return CreatedAtAction(nameof(GetAll), new { id = added.CategoryId }, added);
		}
	}

}
