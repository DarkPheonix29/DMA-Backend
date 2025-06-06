using DMA_BLL.Interfaces;
using DMA_BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
				Categories = d.Categories.Select(c => new CategoryDto
				{
					CategoryId = c.CategoryId,
					Name = c.Name
				}).ToList(),
				Allergens = d.Allergens.Select(a => new AllergenDto
				{
					AllergenId = a.AllergenId,
					Name = a.Name
				}).ToList()
			});

			return Ok(dtoList);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<DishResponseDto>> GetDishById(int id)
		{
			var dish = await _dishRepository.GetDishByIdAsync(id);
			if (dish == null)
				return NotFound(new { message = "Dish not found" });

			var dto = new DishResponseDto
			{
				DishID = dish.DishID,
				Name = dish.Name,
				Description = dish.Description,
				Price = dish.Price,
				IsAvailable = dish.IsAvailable,
				Categories = dish.Categories.Select(c => new CategoryDto
				{
					CategoryId = c.CategoryId,
					Name = c.Name
				}).ToList(),
				Allergens = dish.Allergens.Select(a => new AllergenDto
				{
					AllergenId = a.AllergenId,
					Name = a.Name
				}).ToList()
			};

			return Ok(dto);
		}

		[HttpPost]
		public async Task<ActionResult<DishResponseDto>> CreateDish([FromBody] CreateOrUpdateDishDto dto)
		{
			var categories = await _dishRepository.GetCategoriesByIdsAsync(dto.CategoryIds ?? new List<int>());
			var allergens = await _dishRepository.GetAllergensByIdsAsync(dto.AllergenIds ?? new List<int>());

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
				Categories = categories.Select(c => new CategoryDto
				{
					CategoryId = c.CategoryId,
					Name = c.Name
				}).ToList(),
				Allergens = allergens.Select(a => new AllergenDto
				{
					AllergenId = a.AllergenId,
					Name = a.Name
				}).ToList()
			};

			return CreatedAtAction(nameof(GetDishById), new { id = dish.DishID }, response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateDish(int id, [FromBody] CreateOrUpdateDishDto dto)
		{
			if (dto.DishID != null && id != dto.DishID)
				return BadRequest(new { message = "Dish ID mismatch" });

			var dish = await _dishRepository.GetDishByIdAsync(id);
			if (dish == null)
				return NotFound(new { message = "Dish not found" });

			var categories = await _dishRepository.GetCategoriesByIdsAsync(dto.CategoryIds ?? new List<int>());
			var allergens = await _dishRepository.GetAllergensByIdsAsync(dto.AllergenIds ?? new List<int>());

			dish.Name = dto.Name;
			dish.Description = dto.Description;
			dish.Price = dto.Price;
			dish.IsAvailable = dto.IsAvailable;
			dish.Categories = categories;
			dish.Allergens = allergens;

			await _dishRepository.UpdateDishAsync(dish);

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

public class CategoryDto
{
	public int CategoryId { get; set; }
	public string Name { get; set; }
}

public class AllergenDto
{
	public int AllergenId { get; set; }
	public string Name { get; set; }
}

public class DishResponseDto
{
	public int DishID { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public bool IsAvailable { get; set; }
	public List<CategoryDto> Categories { get; set; }
	public List<AllergenDto> Allergens { get; set; }
}

public class CreateOrUpdateDishDto
{
	public int? DishID { get; set; } // for PUT
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public bool IsAvailable { get; set; }
	public List<int> CategoryIds { get; set; }
	public List<int> AllergenIds { get; set; }
}