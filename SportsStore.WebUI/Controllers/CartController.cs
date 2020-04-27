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
	    private IOrderProcessor _orderProcessor;
		public CartController(IProductRepository repository, IOrderProcessor orderProcessor)
	    {
		    _repository = repository;
		    _orderProcessor = orderProcessor;

	    }

	    public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
	    {
		    var Product = _repository.Products
			    .FirstOrDefault(x => x.ProductID == productId);
		    if (Product != null)
		    {
			    cart.AddItem(Product, 1);
		    }

		    return RedirectToAction("Index", new {returnUrl});
	    }

	    public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
	    {
		    var Product = _repository.Products
			    .FirstOrDefault(x => x.ProductID == productId);
		    if (Product != null)
		    {
			    cart.RemoveLine(Product);
		    }

		    return RedirectToAction("Index", new { returnUrl });
	    }

		// GET: Cart
        public ViewResult Index(Cart cart, string returnUrl)
        {
	        return View(new CartIndexViewModel
	        {
		        Cart = cart,
		        ReturnUrl = returnUrl
	        });
        }
		
        public PartialViewResult Summary(Cart cart)
        {
	        return PartialView(cart);
        }
        [HttpGet]
		public ViewResult Checkout()
        {
	        return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
	        if (cart.Lines.Count() == 0)
	        {
		        ModelState.AddModelError("", "Sorry, your cart is empty!");
	        }
	        if (ModelState.IsValid)
	        {
		        _orderProcessor.ProcessOrder(cart, shippingDetails);
		        cart.Clear();
		        return View("Completed");
	        }
	        else
	        {
		        return View(shippingDetails);
	        }
        }
	}
}