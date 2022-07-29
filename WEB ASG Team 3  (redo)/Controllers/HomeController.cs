using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WEB2022Apr_P02_T3.DAL;
using WEB2022Apr_P02_T3.Models;
using Microsoft.AspNetCore.Http;

namespace WEB2022Apr_P02_T3.Controllers
{
    public class HomeController : Controller
    {
        private CustomerDAL customerContext = new CustomerDAL();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StaffLogin(IFormCollection formData)
        {
            // Read inputs from textboxes
            // Email address converted to lowercase
            string loginID = formData["txtLoginID"].ToString();
            string password = formData["txtPassword"].ToString();
            if (loginID == "Marketing" && password == "passMarketing")
            {
                // Store Login ID in session with the key “LoginID”
                HttpContext.Session.SetString("LoginID", loginID);
                HttpContext.Session.SetString("Role", "Marketing");

                // Redirect user to the "StaffMain" view through an action
                return RedirectToAction("MarketingMain");
            }
            else if (loginID == "ProductManager" && password == "passProduct")
            {
                // Store Login ID in session with the key “LoginID”
                HttpContext.Session.SetString("LoginID", loginID);
                HttpContext.Session.SetString("Role", "ProductManager");

                // Redirect user to the "StaffMain" view through an action
                return RedirectToAction("ProductMain");
            }
            else if (loginID == "SG-Orchard" && password == "passSales")
            {
                // Store Login ID in session with the key “LoginID”
                HttpContext.Session.SetString("LoginID", loginID);
                HttpContext.Session.SetString("Role", "SalesPersonnel");

                // Redirect user to the "StaffMain" view through an action
                return RedirectToAction("SalesMain");
            }
            else if (customerContext.ValidatePassword(loginID, password))
            {
                // Store Login ID in session with the key “LoginID”
                HttpContext.Session.SetString("LoginID", loginID);
                HttpContext.Session.SetString("Role", "Customer");
                if (password == "AbC@123#")
                {
                    TempData["Title"] = "Change Default Password";
                    return RedirectToAction("ChangePassword", "Customer");
                }
                else
                {
                    TempData["Title"] = "Change Password";
                    // Redirect user to the "CustomerMain" view through an action
                    return RedirectToAction("CustomerMain");
                }
            }
            else
            {
                // Store an error message in TempData for display at the index view
                TempData["Message"] = "Invalid Login Credentials!";

                // Redirect user back to the index view through an action
                return RedirectToAction("Index");
            }
        }
        public ActionResult MarketingMain()
        {
            return View();
        }
        public ActionResult ProductMain()
        {
            return View();
        }
        public ActionResult CustomerMain()
        {
            return View();
        }
        public ActionResult SalesMain()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            // Clear all key-values pairs stored in session state
            HttpContext.Session.Clear();
            // Call the Index action of Home controller
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
