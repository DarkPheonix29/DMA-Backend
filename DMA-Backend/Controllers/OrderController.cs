using DMA_BLL.Interfaces;
using DMA_BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DMA_Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderRepos _orderRepos;

		public OrderController(IOrderRepos orderRepos)
		{
			_orderRepos = orderRepos;
		}

		// Create a new order
		[HttpPost("create")]
		public IActionResult CreateOrder([FromBody] OrderRequest orderRequest)
		{
			var orderItems = new List<OrderedItem>();
			foreach (var item in orderRequest.Items)
			{
				var dish = new Dish { DishID = item.DishId, Price = item.Price }; // In a real scenario, fetch the Dish from DB
				orderItems.Add(new OrderedItem
				{
					DishId = dish.DishID,
					Quantity = item.Quantity,
				});
			}

			var order = _orderRepos.CreateOrder(orderRequest.CustomerName, orderItems);

			return Ok(order);
		}

		// Get an order by ID
		[HttpGet("{orderId}")]
		public IActionResult GetOrder(int orderId)
		{
			var order = _orderRepos.GetOrderById(orderId);
			if (order == null)
				return NotFound();

			return Ok(order);
		}

		// Update order status
		[HttpPut("{orderId}/status")]
		public IActionResult UpdateOrderStatus(int orderId, [FromBody] string status)
		{
			_orderRepos.UpdateOrderStatus(orderId, status);
			return Ok();
		}
	}

	public class OrderRequest
	{
		public string CustomerName { get; set; }
		public List<OrderItemRequest> Items { get; set; }
	}

	public class OrderItemRequest
	{
		public int DishId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}
}
