using DMA_BLL.Interfaces;
using DMA_BLL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DMA_DAL.Repos
{
	public class OrderRepos : IOrderRepos
	{
		private readonly ApplicationDbContext _context;

		public OrderRepos(ApplicationDbContext context)
		{
			_context = context;
		}

		public Order CreateOrder(string customerName, List<OrderedItem> orderItems)
		{
			var order = new Order
			{
				OrderTime = TimeOnly.MinValue,
				OrderItems = orderItems,
			};

			_context.Orders.Add(order);
			_context.SaveChanges();

			return order;
		}

		public Order GetOrderById(int orderId)
		{
			return _context.Orders
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.Dish)
				.FirstOrDefault(o => o.OrderId == orderId);
		}

	/*	public List<Order> GetOrdersByCustomer(string customerName)
		{
			return _context.Orders
				.Where(o => o.CustomerName == customerName)
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.Dish)
				.ToList();
		} */

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
}
