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
    public class MonthlyTransactionController : Controller
    {
        private SalesTransactionDAL salesTransactionContext = new SalesTransactionDAL();
        // GET: MonthlyTransactions
        public ActionResult Index()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
               (HttpContext.Session.GetString("Role") != "Marketing"))
            {
                return RedirectToAction("Index", "Home");
            }
            MonthlyTransactionVM mthVM = new MonthlyTransactionVM();

            List<SalesTransaction> salesTransactionsList = new List<SalesTransaction>
                (salesTransactionContext.GetAllTransactions());
            List<SalesTransaction> chosenTransactionList = new List<SalesTransaction>();
            foreach (SalesTransaction salesTransaction in salesTransactionsList)
            {
                if(salesTransaction.dateCreated.Month == (DateTime.Now.Month - 1))
                {
                    chosenTransactionList.Add(salesTransaction);
                }
            }
            return View(mthVM);
        }

        // GET: MonthlyTransactions/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MonthlyTransactions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MonthlyTransactions/Create
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

        // GET: MonthlyTransactions/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MonthlyTransactions/Edit/5
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

        // GET: MonthlyTransactions/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MonthlyTransactions/Delete/5
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
