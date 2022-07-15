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
using Microsoft.AspNetCore.Http.Extensions;

namespace WEB2022Apr_P02_T3.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerDAL customerContext = new CustomerDAL();
        public IActionResult Index()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Customer"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            string id = HttpContext.Session.GetString("LoginID");
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Customer"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            { //Query string parameter not provided
              //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            Customer cust = customerContext.GetDetails(id);
            if (cust == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string pwd)
        {
            string id = HttpContext.Session.GetString("LoginID");
            Customer cust = customerContext.GetDetails(id);
            if (ModelState.IsValid)
            {
                //Update staff record to database
                customerContext.ChangePassword(cust);
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the view
                //to display error message
                return View();
            }
        }
    }
}
