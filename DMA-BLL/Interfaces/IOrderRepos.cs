using DMA_BLL.Models;
using System.Collections.Generic;

namespace DMA_BLL.Interfaces
{
	public interface IOrderRepos
	{
		Task<Order> CreateOrderAsync(int tableId, List<OrderedItem> orderItems, decimal totalAmount);

		Task<IEnumerable<Order>> GetAllOrdersAsync();
		Order GetOrderById(int orderId);
		//List<Order> GetOrdersByCustomer(string customerName);
		void UpdateOrderStatus(int orderId, string status);
		void AddOrderItem(int orderId, OrderedItem orderItem);

		Task<IEnumerable<OrderedItem>> GetAllOrderedItemsAsync();
	}
}
