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
    public class IssueVoucherController : Controller
    {
        private IssueVoucherDAL issuevouchercontext = new IssueVoucherDAL();
        // GET: IssueVoucher
        public ActionResult Index()
        {
            return View();
        }

        // GET: IssueVoucher/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IssueVoucher/Create
        public ActionResult Create(string id, decimal amt, DateTime date)
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Marketing"))
            {
                return RedirectToAction("Index", "Home");
            }
            IssueVoucher voucher = new IssueVoucher();
            voucher.MemberID = id;
            voucher.MonthIssuedFor = date.Month;
            voucher.YearIssuedFor = date.Year;
            voucher.DateTimeIssued = DateTime.Now;
            if ((200 <= amt) && (amt < 500))
            {
                voucher.Amount = 20;
            }
            else if ((500 <= amt) && (amt < 1000))
            {
                voucher.Amount = 40;
            }
            else if ((1000 <= amt) && (amt < 1500))
            {
                voucher.Amount = 80;
            }
            else if (amt >= 1500)
            {
                voucher.Amount = 160;
            }

            return View(voucher);
        }

        // POST: IssueVoucher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IssueVoucher voucher)
        {

            if (ModelState.IsValid)
            {
                voucher.IssuingID = issuevouchercontext.Create(voucher);
                return RedirectToAction("Rank", "SalesTransaction");
            }
            else
            {
                return View(voucher);
            }

        }

        // GET: IssueVoucher/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IssueVoucher/Edit/5
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

        // GET: IssueVoucher/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IssueVoucher/Delete/5
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
