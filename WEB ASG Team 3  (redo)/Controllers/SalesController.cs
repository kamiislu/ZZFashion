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
    public class SalesController : Controller
    {
        private CustomerDAL customerContext = new CustomerDAL();
        private VoucherDAL voucherContext = new VoucherDAL();
        // GET: SalesController
        public ActionResult Index()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "SalesPersonnel"))
            {
                return RedirectToAction("Index", "Home");
            }

            List<Customer> customerList = customerContext.GetAllCustomer();
            return View(customerList);
        }


        private List<SelectListItem> GetCountries()
        {
            List<SelectListItem> countries = new List<SelectListItem>();
            countries.Add(new SelectListItem
            {
                Value = "Singapore",
                Text = "Singapore"
            });
            countries.Add(new SelectListItem
            {
                Value = "Malaysia",
                Text = "Malaysia"
            });
            countries.Add(new SelectListItem
            {
                Value = "Indonesia",
                Text = "Indonesia"
            });
            countries.Add(new SelectListItem
            {
                Value = "China",
                Text = "China"
            });
            countries.Add(new SelectListItem
            {
                Value = "United Kingdom",
                Text = "United Kingdom"
            });
            countries.Add(new SelectListItem
            {
                Value = "India",
                Text = "India"
            });



            return countries;
        }

        // GET: SalesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            // Stop accessing the action if not logged in
            // or account not in the "SalesPersonnel" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "SalesPersonnel"))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["CountryList"] = GetCountries();
            return View();
        }

        // POST: SalesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            {
                //Get country list for drop-down list
                //in case of the need to return to Create.cshtml view
                ViewData["CountryList"] = GetCountries();

                if (ModelState.IsValid)
                {
                    //Add staff record to database
                    customer.MemberId = customerContext.Add(customer).ToString();
                    //Redirect user to Sales/Index view
                    return RedirectToAction("Index");
                }
                else
                {
                    //Input validation fails, return to the Create view
                    //to display error message
                    return View(customer);
                }
            }
        }

        // GET: SalesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SalesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalesController/Delete/5
        public ActionResult Delete(string id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "SalesPersonnel" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "SalesPersonnel"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            Customer customer = customerContext.GetDetails(id);
            if (customer == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }

            return View(customer);
        }


        // POST: SalesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Customer customer)
        {

            // Delete the customner record from database
            customerContext.Delete(customer.MemberId);
            return RedirectToAction("Index");
        }

        public ActionResult PassVoucher(string searchString)
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "SalesPersonnel"))
            {
                return RedirectToAction("Index", "Home");
            }


            var searchCustomer = from c in voucherContext.GetAllCashVoucher()
                                 select c;


            if (!String.IsNullOrEmpty(searchString))
            {

                searchCustomer = searchCustomer.Where(c => c.MemberId.Contains(searchString));

            }

            return View(searchCustomer);
        }

        // GET: StaffController/Edit/5
        public ActionResult Collect(int id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "SalesPersonnel"))
            {
                return RedirectToAction("Index", "Home");
            }

            CashVoucher cashvoucher = voucherContext.GetDetails(id);
            if (cashvoucher == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("PassVoucher");
            }

            if (cashvoucher.Status.ToString() == "1" || cashvoucher.Status.ToString() == "2")
            {
                return RedirectToAction("PassVoucher");
            }


            return View(cashvoucher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Collect(CashVoucher cashVoucher)
        {
            //in case of the need to return to Edit.cshtml view
            if (ModelState.IsValid)
            {
                //Update staff record to database
                voucherContext.Collect(cashVoucher);
                return RedirectToAction("PassVoucher");
            }
            else
            {
                //Input validation fails, return to the view
                //to display error message
                return View(cashVoucher);
            }
        }

        // GET: StaffController/Edit/5
        public ActionResult Redeem(int id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "SalesPersonnel"))
            {
                return RedirectToAction("Index", "Home");
            }

            CashVoucher cashvoucher = voucherContext.GetDetails(id);
            if (cashvoucher == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("PassVoucher");
            }

            if (cashvoucher.Status.ToString() == "0" || cashvoucher.Status.ToString() == "2")
            {
                return RedirectToAction("PassVoucher");
            }


            return View(cashvoucher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Redeem(CashVoucher cashVoucher)
        {
            //in case of the need to return to Edit.cshtml view
            if (ModelState.IsValid)
            {
                //Update staff record to database
                voucherContext.Collect(cashVoucher);
                return RedirectToAction("PassVoucher");
            }
            else
            {
                //Input validation fails, return to the view
                //to display error message
                return View(cashVoucher);
            }
        }

    }
}
