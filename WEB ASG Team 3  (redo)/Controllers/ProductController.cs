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

            if (product.Obsolete == "1")
            {
                product.isActive = false;
            }
            else if (product.Obsolete == "0")
            {
                product.isActive = true;
            }

            return View(product);
 
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Product product)
        {
            if (product.fileToUpload != null &&
product.fileToUpload.Length > 0)
            {
                try
                {
                    // Find the filename extension of the file to be uploaded.
                    string fileExt = Path.GetExtension(
                     product.fileToUpload.FileName);
                    // Rename the uploaded file with the staff’s name.
                    string uploadedFile = product.fileToUpload + fileExt;
                    // Get the complete path to the images folder in server
                    string savePath = Path.Combine(
                     Directory.GetCurrentDirectory(),
                     "wwwroot\\images", uploadedFile);
                    // Upload the file to server
                    using (var fileSteam = new FileStream(
                     savePath, FileMode.Create))
                    {
                        product.fileToUpload.CopyTo(fileSteam);
                    }
                    product.ProductImage = uploadedFile;
                    ViewData["Message"] = "File uploaded successfully.";
                }
                catch (IOException)
                {
                    //File IO error, could be due to access rights denied
                    ViewData["Message"] = "File uploading fail!";
                }
                catch (Exception ex) //Other type of error
                {
                    ViewData["Message"] = ex.Message;
                }
            }
            else if (product.fileToUpload == null)
            {
                if (product.ProductImage != null)
                {

                }
            }
            else
            {
      
                
            }

           if (product.isActive == false)
            {
                product.Obsolete = "1";
            }
           else if (product.isActive == true)
            {
                product.Obsolete = "0";
            }

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
