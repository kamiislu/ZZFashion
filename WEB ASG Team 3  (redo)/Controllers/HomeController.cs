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
        private ProductDAL productContext = new ProductDAL();
        private StaffDAL staffContext = new StaffDAL();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Product> newProductList = productContext.GetNewProduct();
            return View(newProductList);
        }

        [HttpPost]
        public ActionResult StaffLogin(IFormCollection formData)
        {
            List<Staff> staffList = staffContext.GetAllStaff();
            // Read inputs from textboxes
            // Email address converted to lowercase
            string loginID = formData["txtLoginID"].ToString();
            string password = formData["txtPassword"].ToString();
            if (loginID != null && password != null)
            {
                foreach (Staff s in staffList)
                {
                    if (loginID == s.StaffID && password == s.SPassword)
                    {
                        // Store Login ID in session with the key “LoginID”
                        HttpContext.Session.SetString("LoginID", loginID);
                        HttpContext.Session.SetString("Role", loginID);
                        if (s.StaffID == "SG-Bishan" || s.StaffID == "SG-Jurong" ||  s.StaffID == "SG-Orchard")
                        {
                            HttpContext.Session.SetString("Role", "SalesPersonnel");
                            return RedirectToAction("SalesMain");
                        }
                        else if (s.StaffID == "ProductManager")
                        {
                            HttpContext.Session.SetString("Role", "ProductManager");
                            return RedirectToAction("ProductMain");
                        }
                        else
                        {
                            return RedirectToAction(s.StaffID.ToString() + "Main");
                        }
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
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public ActionResult MarketingMain()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
               (HttpContext.Session.GetString("Role") != "Marketing"))
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult ProductMain()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
               (HttpContext.Session.GetString("Role") != "ProductManager"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult CustomerMain()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
               (HttpContext.Session.GetString("Role") != "Customer"))
            {
                return RedirectToAction("Index", "Home");
            }
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
            // test
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult ValidateBirthdayMonth()
        {
            int thisMonth = DateTime.Now.Month;
            string id = HttpContext.Session.GetString("LoginID");
            Customer cust = customerContext.GetDetails(id);
            int custBday = cust.MBirthDate.Month;
            if (custBday == thisMonth)
            {
                TempData["BirthdayMessage"] = "Happy birthday! It's the birthday month for you! Enjoy additional 15% " +
                    "discount for every purchase this month!";
                return RedirectToAction("CustomerMain");
            }
            else
            {
                return RedirectToAction("CustomerMain");
            }
        }
    }
}
