using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WEB2022Apr_P02_T3.DAL;
using WEB2022Apr_P02_T3.Models;


namespace WEB2022Apr_P02_T3.Controllers
{
    public class ProductController : Controller
    {
        private ProductDAL productContext = new ProductDAL();

        public IActionResult Index()
        {

            return View();
        }

        public ActionResult Products()
        {
            List<Product> products = productContext.GetAllProduct();
            ViewBag.products = products;
            return View();
        }
        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View();
        }


    
    }
}
