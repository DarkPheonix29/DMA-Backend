using DMA_BLL.Interfaces;
using DMA_DAL;
using Microsoft.EntityFrameworkCore;
using DMA_BLL.Models;

public class OrderRepos : IOrderRepos
{
	private readonly ApplicationDbContext _context;

	public OrderRepos(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Order> CreateOrderAsync(int tableId, List<OrderedItem> orderItems, decimal totalAmount)
	{
		var order = new Order
		{
			TableId = tableId,
			OrderTime = TimeOnly.FromDateTime(DateTime.Now),
			TotalAmount = totalAmount,
			OrderItems = orderItems
		};

		_context.Orders.Add(order);
		await _context.SaveChangesAsync();

		return order;
	}

	public async Task<IEnumerable<Order>> GetAllOrdersAsync()
	{
		return await _context.Orders
			.Include(o => o.OrderItems)
			.ThenInclude(oi => oi.Dish)
			.ToListAsync();
	}


	public async Task<IEnumerable<OrderedItem>> GetAllOrderedItemsAsync()
	{
		return await _context.OrderItems
			.Include(oi => oi.Dish) // Ensure Dish is included to access Dish.Name
			.Select(oi => new OrderedItem
			{
				OrderItemId = oi.OrderItemId,
				OrderId = oi.OrderId,
				DishId = oi.DishId,
				Quantity = oi.Quantity,
				DishName = oi.Dish.Name // Only set the DishName
			})
			.ToListAsync();
	}

	public Order GetOrderById(int orderId)
	{
		return _context.Orders
			.Include(o => o.OrderItems)
			.ThenInclude(oi => oi.Dish)
			.FirstOrDefault(o => o.OrderId == orderId);
	}

	public void UpdateOrderStatus(int orderId, string status)
	{
		var order = _context.Orders.Find(orderId);
		if (order != null)
		{
			order.Status = status;
			_context.SaveChanges();
		}
	}

	public void AddOrderItem(int orderId, OrderedItem orderItem)
	{
		var order = _context.Orders.Find(orderId);
		if (order != null)
		{
			order.OrderItems.Add(orderItem);
			_context.SaveChanges();
		}
	}
}
