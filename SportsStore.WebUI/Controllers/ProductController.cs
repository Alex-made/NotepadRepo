using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;
            
        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        private int pageSize = 1;

        public ViewResult List(int page = 1)
        {
	        var result = new ProductsListViewModel
	        {
		        Products = _repository.Products
			        .Skip(pageSize * (page - 1))
			        .Take(pageSize),
		        PagingInfo = new PagingInfo
		        {
			        TotalItems = _repository.Products.Count(),
			        CurrentPage = page,
			        ItemsPerPage = pageSize
		        }
	        };
	        
	        return View(result);
        }
    }
}