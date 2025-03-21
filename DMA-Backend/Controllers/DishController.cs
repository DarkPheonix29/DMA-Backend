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
		public async Task<ActionResult<IEnumerable<Dish>>> GetAllDishes()
		{
			var dishes = await _dishRepository.GetAllDishesAsync();
			return Ok(dishes);
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
		public async Task<ActionResult<Dish>> CreateDish(Dish dish)
		{
			if (dish == null)
				return BadRequest(new { message = "Invalid dish data" });

			await _dishRepository.AddDishAsync(dish);
			return CreatedAtAction(nameof(GetDishById), new { id = dish.DishID }, dish);
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
