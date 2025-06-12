using DMA_BLL.Interfaces;
using DMA_BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using DMA_Backend.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace DMA_Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderRepos _orderRepos;
		private readonly IDishRepos _dishRepos;
		private readonly IHubContext<OrderHub> _hubContext;

		public OrderController(IOrderRepos orderRepos, IDishRepos dishRepos, IHubContext<OrderHub> hubContext)
		{
			_orderRepos = orderRepos;
			_dishRepos = dishRepos;
			_hubContext = hubContext;
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateOrder([FromBody] OrderRequest orderRequest)
		{
			var orderItems = new List<OrderedItem>();
			decimal totalAmount = 0;

			foreach (var item in orderRequest.Items)
			{
				var dish = await _dishRepos.GetDishByIdAsync(item.DishId);
				if (dish == null)
					return NotFound($"Dish with ID {item.DishId} not found.");

				totalAmount += dish.Price * item.Quantity;

				orderItems.Add(new OrderedItem
				{
					DishId = dish.DishID,
					Quantity = item.Quantity,
					DishName = dish.Name // Store only the name of the dish
				});
			}

			var order = await _orderRepos.CreateOrderAsync(orderRequest.TableId, orderItems, totalAmount);
			
			await _hubContext.Clients.All.SendAsync("ReceiveOrder", order);

			return Ok(order);
		}

		[HttpGet("all-orders")]
		public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
		{
			var orders = await _orderRepos.GetAllOrdersAsync();
			return Ok(orders);
		}

		[HttpGet("all-ordered-items")]
		public async Task<ActionResult<IEnumerable<OrderedItem>>> GetAllOrderedItems()
		{
			var orderedItems = await _orderRepos.GetAllOrderedItemsAsync();
			return Ok(orderedItems);
		}

		[HttpGet("{orderId}")]
		public IActionResult GetOrder(int orderId)
		{
			var order = _orderRepos.GetOrderById(orderId);
			if (order == null)
				return NotFound();

			return Ok(order);
		}

		[HttpPut("{orderId}/status")]
		public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] string status)
		{
			// Update de status
			_orderRepos.UpdateOrderStatus(orderId, status);

			// Haal de order op, zodat we die kunnen doorsturen
			var order = _orderRepos.GetOrderById(orderId);
			if (order == null)
				return NotFound();

			// Als de order is gewijzigd, dan sturen we de order naar de client.
			if (status.Equals("Done", StringComparison.OrdinalIgnoreCase) ||
			    status.Equals("Bar Done", StringComparison.OrdinalIgnoreCase) ||
			    status.Equals("Keuken Done", StringComparison.OrdinalIgnoreCase) ||
				status.Equals("Bar PreDone", StringComparison.OrdinalIgnoreCase) ||
				status.Equals("Keuken PreDone", StringComparison.OrdinalIgnoreCase)||
				status.Equals("Waiting", StringComparison.OrdinalIgnoreCase) ||
				status.Equals("Bar Finished", StringComparison.OrdinalIgnoreCase) ||
				status.Equals("Keuken Finished", StringComparison.OrdinalIgnoreCase))
			{
				await _hubContext.Clients.All.SendAsync("ReceiveOrder", order);
			}

			return Ok(order);
		}
	}

	public class OrderRequest
	{
		public int TableId { get; set; }
		public List<OrderItemRequest> Items { get; set; }
	}

	public class OrderItemRequest
	{
		public int DishId { get; set; }
		public int Quantity { get; set; }
	}
}
