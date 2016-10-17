using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyAndCare.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            ListItem = new List<ShoppingCartItem>();
        }
        public void AddToCart(ShoppingCartItem item)
        {
            if (ListItem.Where(x=>x.ProductName.Equals(item.ProductName)).Any())
            {
                var myItem = ListItem.Single(x => x.ProductName.Equals(item.ProductName));
                myItem.Quanlity += item.Quanlity;
                myItem.Total += item.Quanlity * Convert.ToDouble(item.Price.Trim().Replace(",", string.Empty));
            }
            else
            {
                ListItem.Add(item);
            }
        }
        public List<ShoppingCartItem> ListItem { get; set; }
    }
    public class ShoppingCartItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public int Quanlity { get; set; }
        public double Total { get; set; }
    }
}