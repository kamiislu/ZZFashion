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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEB2022Apr_P02_T3.Controllers
{
    public class CustomerController : Controller
    {
        private FeedbackDAL feedbackContext = new FeedbackDAL();
        private CustomerDAL customerContext = new CustomerDAL();
        private ResponseDAL responseContext = new ResponseDAL();
        public ActionResult Index(string searchString)
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "SalesPersonnel") &&
                (HttpContext.Session.GetString("Role") != "Customer"))
            {
                return RedirectToAction("Index", "Home");
            }

            //List<Customer> customerList = customerContext.GetAllCustomer();

            var searchCustomer = from c in customerContext.GetAllCustomer()
                                 select c;

            if (!String.IsNullOrEmpty(searchString))
            {               
                
                searchCustomer = searchCustomer.Where(c => c.MemberId.Contains(searchString) ||  c.MName.Contains(searchString));

            }

            return View(searchCustomer);
        }



        public ActionResult Login()
            {
            return View();
            }
        public ActionResult CustomerMain()
        {
            string id = HttpContext.Session.GetString("LoginID");
            Customer cust = customerContext.GetDetails(id);
            TempData["CustomerName"] = cust.MName;
            return View(cust);
        }
        public ActionResult Edit()
        {
            string id = HttpContext.Session.GetString("LoginID");// Stop accessing the action if not logged in
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
            return View(cust);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IFormCollection formData)
        {
            string id = HttpContext.Session.GetString("LoginID");
            string telNo = formData["txtTelNo"].ToString();
            string emailAddr = formData["txtEmailAddr"].ToString();
            string address = formData["txtAddress"].ToString();    
            if (customerContext.IsEmailExist(emailAddr, id) == true && customerContext.IsPhoneNumExist(telNo, id) == true)
            {
                TempData["validateEmail"] = "Email address already exists!";
                TempData["validatePhoneNum"] = "Phone number already exists!";
            }
            else if (customerContext.IsEmailExist(emailAddr, id) == true)
            {
                //Input validation fails, return to the Create view
                //to display error message
                TempData["validateEmail"] = "Email address already exists!";
            }
            else if (customerContext.IsPhoneNumExist(telNo, id) == true)
            {
                //Input validation fails, return to the Create view
                //to display error message
                TempData["validatePhoneNum"] = "Phone number already exists!";
            }
            else
            {
                //Add staff record to database
                customerContext.Update(telNo, address, emailAddr, id);
                //Redirect user to Staff/Index view
            }
            Customer cust = customerContext.GetDetails(id);
            return View(cust);
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
        public ActionResult ChangePassword(IFormCollection formData)
        {
            string id = HttpContext.Session.GetString("LoginID");
            Customer cust = customerContext.GetDetails(id);
            string pwd = cust.MPassword;
            string oldPwd = formData["txtPassword"].ToString();
            string newPwd = formData["txtNewPassword"].ToString();
            string confirmPwd = formData["txtConfirmPassword"].ToString();
           if (oldPwd == pwd && newPwd == "" && confirmPwd == "")
            {
                // Store an error message in TempData for display at the index view
                TempData["newMessage"] = "New password cannot be blank!";
            }
            else if (confirmPwd == pwd)
            {
                // Store an error message in TempData for display at the index view
                TempData["confirmMessage"] = "New password cannot be the same as old!";
            }
            else if (oldPwd == pwd && newPwd == confirmPwd)
            {
                customerContext.ChangePassword(confirmPwd, id);
            }
            else if (oldPwd == pwd  && newPwd != confirmPwd)
            {
                // Store an error message in TempData for display at the index view
                TempData["confirmMessage"] = "Wrong confirm password!";
            }
            else if (oldPwd == "" && newPwd == "" && confirmPwd == "")
            {
                // Store an error message in TempData for display at the index view
                TempData["confirmMessage"] = "Please input your new password!";
            }
            else if (oldPwd != pwd)
            {
                // Store an error message in TempData for display at the index view
                TempData["Message"] = "Incorrect password!";
            }
   
            return View();
        }
        public ActionResult ViewFeedback(List<Feedback> newList)
        {
            List<Feedback> feedbackList = feedbackContext.GetAllFeedback();
            string id = HttpContext.Session.GetString("LoginID");
            foreach (Feedback f in feedbackList)
            {
                if (f.MemberID.ToString() == id)
                {
                    newList.Add(f);
                }
            }
            return View(newList);
        }

        public ActionResult ViewResponse(List<Response> newList)
        {
            List<Response> responseList = responseContext.GetAllResponse();
            List<Feedback> feedbackList = feedbackContext.GetAllFeedback();
            string id = HttpContext.Session.GetString("LoginID");
            foreach (Feedback f in feedbackList)
            {
                if (f.MemberID.ToString() == id)
                {
                    foreach (Response r in responseList)
                    {
                        if (f.FeedbackID == r.FeedbackID && r.StaffID == "Marketing")
                        {
                            newList.Add(r);
                        }
                    }
                }        
            }
            return View(newList);
        }
        
    }
}
