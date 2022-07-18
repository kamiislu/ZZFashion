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

        public ViewResult Create()
        {
            return View();
        }
        // POST: Staff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {

            if (ModelState.IsValid)
            {
                //Add staff record to database
                product.ProductId = productContext.Add(product);
                //Redirect user to Staff/Index view
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(product);
            }
        }

        public ActionResult Update(int? id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "ProductManager"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            { //Query string parameter not provided
              //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            Product product = productContext.GetDetails(id.Value);
            if (product == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            return View(product);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Product product)
        {

            if (ModelState.IsValid)
            {
                //Update staff record to database
                productContext.Update(product);
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the view
                //to display error message
                return View(product);
            }
        }



        public ActionResult Delete(int? id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "ProductManager"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            { //Query string parameter not provided
              //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            Product product = productContext.GetDetails(id.Value);
            if (product == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            return View(product);
        }
        // POST: StaffController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Product product)
        {
            // Delete the staff record from database
            productContext.Delete(product.ProductId);
            return RedirectToAction("Index");
        }



    }
}
