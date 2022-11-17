using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    public OrderItem RequestByOrderIDProductID(int orderId, int productId);
    public List<OrderItem> RequestByOrderId(int orderId);
}
