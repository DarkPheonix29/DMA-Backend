using DMA_BLL.Models;
using System.Collections.Generic;

namespace DMA_BLL.Interfaces
{
	public interface IOrderRepos
	{
		Order CreateOrder(string customerName, List<OrderedItem> orderItems);
		Order GetOrderById(int orderId);
		//List<Order> GetOrdersByCustomer(string customerName);
		void UpdateOrderStatus(int orderId, string status);
		void AddOrderItem(int orderId, OrderedItem orderItem);
	}
}
