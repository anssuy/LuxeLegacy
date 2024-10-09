using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LuxeLegacy.Models;
using Microsoft.AspNet.Identity;

namespace LuxeLegacy.Controllers
{
    public class CartsController : Controller
    {
        private ApplicationDbContext db;
        private CartService _cartService;

        public CartsController()
        {
            db = new ApplicationDbContext();
        }

        private CartService CartService
        {
            get
            {
                if (_cartService == null)
                {
                    var userId = User.Identity.GetUserId();
                    _cartService = new CartService(db, userId);
                }
                return _cartService;
            }
        }

        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity)
        {
            CartService.AddToCart(productId, quantity); 
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            var cart = CartService.GetOrCreateCart();
            return View(cart);
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int productId, int quantity)
        {
            CartService.RemoveFromCart(productId, quantity);
            return RedirectToAction("Index");
        }

        public ActionResult ClearCart()
        {
            CartService.ClearCart();
            return RedirectToAction("Index");
        }

        public ActionResult Checkout()
        {
            var cart = CartService.GetOrCreateCart();
            return View(cart);
        }

        public ActionResult OrderCompleted()
        {
            return View();
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
