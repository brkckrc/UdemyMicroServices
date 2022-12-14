using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class OrderItem:Entity
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }


        public OrderItem()
        {

        }

        //public int OrderId { get; set; } efcore db'de tanımlayacak
        public OrderItem(string productId, string productName, string pictureUrl, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            Price = price;
        }

        public void UpdateOrderItem(string productName,string pictureUrl, decimal price)
        {
            ProductName = productName;
            Price = price;
            PictureUrl = pictureUrl;
        }

    }
}
