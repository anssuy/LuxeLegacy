using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuxeLegacy.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }

        public double TotalAmount => CartItems.Sum(item => item.TotalPrice);
    }
}