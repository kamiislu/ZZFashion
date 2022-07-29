using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB2022Apr_P02_T3.DAL;
using WEB2022Apr_P02_T3.Models;


namespace WEB2022Apr_P02_T3.Controllers
{
    public class SalesTransactionController : Controller
    {
        private SalesTransactionDAL salesTransactionContext = new SalesTransactionDAL();
        // GET: SalesTransaction
        public ActionResult Index(string? id)
        {
            if ((HttpContext.Session.GetString("Role") == null) || 
                (HttpContext.Session.GetString("Role") != "Marketing"))
            {
                return RedirectToAction("Index", "Home");
            }
            CustomerViewModel custVM = new CustomerViewModel();
            custVM.customerList = salesTransactionContext.GetAllCustomers();

            if(id != null)
            {
                ViewData["selectedCustomer"] = id;
                custVM.salesTransactionList = salesTransactionContext.GetCustomerSalesTransaction(id);
            }
            else
            {
                ViewData["selectedCustomer"]  = "";
            }
            return View(custVM);
        }

        public ActionResult Rank(string? id)
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
               (HttpContext.Session.GetString("Role") != "Marketing"))
            {
                return RedirectToAction("Index", "Home");
            }
            List<TotalAmountViewModel> customerTotalAmountlist = salesTransactionContext.GetCustomerByTotalAmount();
            return View(customerTotalAmountlist);
        }

        // GET: SalesTransaction/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalesTransaction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesTransaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: SalesTransaction/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SalesTransaction/Edit/5
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

        // GET: SalesTransaction/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SalesTransaction/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
