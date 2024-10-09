using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuxeLegacy.Models
{
    public class WishlistItem
    {
        public int WishlistItemId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int WishlistId { get; set; }
        public virtual Wishlist Wishlist { get; set; }
    }
}