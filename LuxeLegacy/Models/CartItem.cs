using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuxeLegacy.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }

        public double TotalPrice => Quantity * Product.Price;
    }
}