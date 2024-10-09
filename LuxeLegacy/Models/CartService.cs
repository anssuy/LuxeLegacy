using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuxeLegacy.Models
{
    public class CartService
    {
        public int Id { get; set; }
        private readonly ApplicationDbContext db;
        private readonly string _userId;

        public CartService(ApplicationDbContext context, string userId)
        {
            db = context;
            _userId = userId;
        }

        public Cart GetOrCreateCart()
        {
            var cart = db.Carts.SingleOrDefault(c => c.UserId == _userId);

            if (cart == null)
            {
                cart = new Cart { UserId = _userId };
                cart.CartItems = new List<CartItem>();
                db.Carts.Add(cart);
                db.SaveChanges();
            }

            return cart;
        }

        public void AddToCart(int productId, int quantity)
        {
            var cart = GetOrCreateCart();
            var cartItem = cart.CartItems.SingleOrDefault(ci => ci.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                var product = db.Products.Find(productId);
                if (product != null)
                {
                    cart.CartItems.Add(new CartItem
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = cart.CartId
                    });
                }
            }

            db.SaveChanges();
        }

        public void RemoveFromCart(int productId, int quantity)
        {
            var cart = GetOrCreateCart();
            var cartItem = cart.CartItems.SingleOrDefault(ci => ci.ProductId == productId);

            if (cartItem != null)
            {
                if (cartItem.Quantity > quantity)
                {
                    cartItem.Quantity -= quantity;
                }
                else
                {
                    db.CartItems.Remove(cartItem);
                }
                db.SaveChanges();
            }
        }

        public void ClearCart()
        {
            var cart = GetOrCreateCart();

            if (cart != null)
            {
                db.CartItems.RemoveRange(cart.CartItems);
            }
            db.SaveChanges();
        }
    }
}
