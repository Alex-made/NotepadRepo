using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
	    private readonly IProductRepository _repository;
	    public CartController(IProductRepository repository)
	    {
		    _repository = repository;
	    }

	    public RedirectToRouteResult AddToCart(int productId, string returnUrl)
	    {
		    var Product = _repository.Products
			    .FirstOrDefault(x => x.ProductID == productId);
		    if (Product != null)
		    {
			    var Cart = GetCart();
				Cart.AddItem(Product, 1);
		    }

		    return RedirectToAction("Index", new {returnUrl});
	    }

	    public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
	    {
		    var Product = _repository.Products
			    .FirstOrDefault(x => x.ProductID == productId);
		    if (Product != null)
		    {
			    var Cart = GetCart();
			    Cart.RemoveLine(Product);
		    }

		    return RedirectToAction("Index", new { returnUrl });
	    }

		private Cart GetCart()
	    {
		    Cart cart = (Cart) Session["Cart"];
		    if (cart == null)
		    {
				cart = new Cart();
				Session["Cart"] = cart;
				return cart;
		    }

		    return cart;
	    }
	    
	    // GET: Cart
        public ViewResult Index(string returnUrl)
        {
	        return View(new CartIndexViewModel
	        {
		        Cart = GetCart(),
		        ReturnUrl = returnUrl
	        });
        }
    }
}