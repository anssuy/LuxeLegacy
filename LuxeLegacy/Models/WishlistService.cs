using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuxeLegacy.Models
{
    public class WishlistService
    {
        public int Id { get; set; }
        private readonly ApplicationDbContext db;
        private readonly string _userId;

        public WishlistService(ApplicationDbContext context, string userId)
        {
            db = context;
            _userId = userId;
        }

        private Wishlist GetOrCreateWishlist()
        {
            var wishlist = db.Wishlists.SingleOrDefault(c => c.UserId == _userId);

            if (wishlist == null)
            {
                wishlist = new Wishlist { UserId = _userId };
                wishlist.WishlistItems = new List<WishlistItem>();
                db.Wishlists.Add(wishlist);
                db.SaveChanges();
            }

            return wishlist;
        }

        public void AddToWishlist(int productId)
        {
            var wishlist = GetOrCreateWishlist();
            var wishlistItem = wishlist.WishlistItems.SingleOrDefault(ci => ci.ProductId == productId);

            if (wishlistItem == null)
            {
                var product = db.Products.Find(productId);
                if (product != null)
                {
                    wishlist.WishlistItems.Add(new WishlistItem
                    {
                        ProductId = productId,
                        WishlistId = wishlist.WishlistId
                    });
                }

            }

            db.SaveChanges();
        }

        public void RemoveFromWishlist(int productId)
        {
            var wishlist = GetOrCreateWishlist();
            var wishlistItem = wishlist.WishlistItems.SingleOrDefault(ci => ci.ProductId == productId);

            if (wishlistItem != null)
            {
                db.WishlistItems.Remove(wishlistItem);
                db.SaveChanges();
            }
        }

        public Wishlist GetWishlist()
        {
            return GetOrCreateWishlist();
        }
    }
}