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
        public bool EmptyCart()
        {
            ListItem.Clear();
            return true;
        }
        public bool UpdateCart(int productId,int Quantity)
        {
            ShoppingCartItem exitItem = ListItem.Where(x => x.ProductID == productId).SingleOrDefault();
            if (exitItem!=null)
            {
                exitItem.Quanlity = Quantity;
                exitItem.Total= exitItem.Quanlity * Convert.ToDecimal(exitItem.Price);
                exitItem.SubTotal = exitItem.Quanlity * Convert.ToDecimal(exitItem.Price);
            }
            return true;
        }
        public void AddToCart(ShoppingCartItem item)
        {
            if (ListItem.Where(x=>x.ProductName.Equals(item.ProductName)).Any())
            {
                var myItem = ListItem.Single(x => x.ProductName.Equals(item.ProductName));
                myItem.Quanlity += item.Quanlity;
                myItem.Total += item.Quanlity * Convert.ToDecimal(item.Price);
                myItem.SubTotal += item.Quanlity * Convert.ToDecimal(item.Price);
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
        public Decimal Total { get; set; }
        public int checkCoupon { get; set; }
        public string PriceCoupon { get; set; }
        public Decimal SubTotal { get; set; }
        public Decimal TotalAll { get; set; }
    }
}