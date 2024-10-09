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
    public class WishlistsController : Controller
    {
        private ApplicationDbContext db;
        private WishlistService _wishlistService;

        public WishlistsController()
        {
            db = new ApplicationDbContext();
        }

        private WishlistService WishlistService
        {
            get
            {
                if (_wishlistService == null)
                {
                    var userId = User.Identity.GetUserId();  
                    _wishlistService = new WishlistService(db, userId);
                }
                return _wishlistService;
            }
        }

        [HttpPost]
        public ActionResult AddToWishlist(int productId)
        {
            WishlistService.AddToWishlist(productId);
            return Json(new { success = true });
        }
        [Authorize]
        public ActionResult Index()
        {
            var wishlist = WishlistService.GetWishlist();  
            return View(wishlist);
        }

        [HttpPost]
        public ActionResult RemoveFromWishlist(int productId)
        {
            WishlistService.RemoveFromWishlist(productId);
            return Json(new { success = true });
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
