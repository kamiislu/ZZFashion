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

            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "ProductManager"))
            {
                return RedirectToAction("Index", "Home");
            }
            List<Product> productList = productContext.GetAllProduct();
            return View(productList);
        }
    }
}
