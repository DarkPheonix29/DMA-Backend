using DMA_BLL.Interfaces;
using DMA_BLL.Models;
using DMA_DAL.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMA_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DishController : ControllerBase
	{
		private readonly IDishRepos _dishRepository;

		public DishController(IDishRepos dishRepository)
		{
			_dishRepository = dishRepository;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<DishResponseDto>>> GetAllDishes()
		{
			var dishes = await _dishRepository.GetAllDishesAsync();

			var dtoList = dishes.Select(d => new DishResponseDto
			{
				DishID = d.DishID,
				Name = d.Name,
				Description = d.Description,
				Price = d.Price,
				IsAvailable = d.IsAvailable,
				Categories = d.Categories.Select(c => c.Name).ToList(),
				Allergens = d.Allergens.Select(a => a.Name).ToList()
			});

			return Ok(dtoList);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<Dish>> GetDishById(int id)
		{
			var dish = await _dishRepository.GetDishByIdAsync(id);
			if (dish == null)
				return NotFound(new { message = "Dish not found" });

			return Ok(dish);
		}

		[HttpPost]
		public async Task<ActionResult<DishResponseDto>> CreateDish([FromBody] CreateDishDto dto)
		{
			// Get related entities
			var categories = await _dishRepository.GetCategoriesByIdsAsync(dto.CategoryIds);
			var allergens = await _dishRepository.GetAllergensByIdsAsync(dto.AllergenIds);

			var dish = new Dish
			{
				Name = dto.Name,
				Description = dto.Description,
				Price = dto.Price,
				IsAvailable = dto.IsAvailable,
				Categories = categories,
				Allergens = allergens
			};

			await _dishRepository.AddDishAsync(dish);

			var response = new DishResponseDto
			{
				DishID = dish.DishID,
				Name = dish.Name,
				Description = dish.Description,
				Price = dish.Price,
				IsAvailable = dish.IsAvailable,
				Categories = categories.Select(c => c.Name).ToList(),
				Allergens = allergens.Select(a => a.Name).ToList()
			};

			return CreatedAtAction(nameof(GetDishById), new { id = dish.DishID }, response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateDish(int id, Dish dish)
		{
			if (id != dish.DishID)
				return BadRequest(new { message = "Dish ID mismatch" });

			var updated = await _dishRepository.UpdateDishAsync(dish);
			if (!updated)
				return NotFound(new { message = "Dish not found" });

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDish(int id)
		{
			var deleted = await _dishRepository.DeleteDishAsync(id);
			if (!deleted)
				return NotFound(new { message = "Dish not found" });

			return NoContent();
		}
	}
}
public class CreateDishDto
{
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public bool IsAvailable { get; set; }
	public List<int> CategoryIds { get; set; }
	public List<int> AllergenIds { get; set; }
}
public class DishResponseDto
{
	public int DishID { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public bool IsAvailable { get; set; }

	public List<string> Categories { get; set; }
	public List<string> Allergens { get; set; }
}