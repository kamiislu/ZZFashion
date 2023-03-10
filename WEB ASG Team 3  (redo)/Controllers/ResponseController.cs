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
    public class ResponseController : Controller
    {
        private ResponseDAL responseContext = new ResponseDAL();
        private FeedbackDAL feedbackContext = new FeedbackDAL();
        // GET: Response
        public ActionResult Index()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Marketing") &&
                (HttpContext.Session.GetString("Role") != "Customer"))
            {
                return RedirectToAction("Index", "Home");
            }
            List<Response> responseList = responseContext.GetAllResponse();
            return View(responseList);
        }

        // GET: Response/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Response/Create
        public ActionResult Create(int? id)
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Marketing"))
            {
                return RedirectToAction("Index", "Home");
            }
            Response response= feedbackContext.GetDetails(id.Value);
            response.StaffID = HttpContext.Session.GetString("Role");
            return View(response);
        }

        // POST: Response/Create
        [HttpPost]  
        [ValidateAntiForgeryToken]
        public ActionResult Create(Response response)
        {
            List<Response> responseList = responseContext.GetAllResponse();
            if (ModelState.IsValid && !String.IsNullOrEmpty(response.Text))
            {
                foreach (Response r in responseList)
                {
                    if (r.Text != response.Text)
                    {
                        //Add staff record to database
                        response.DatePosted = DateTime.Now;
                        response.ResponseID = responseContext.Create(response);
                        break;
                    }
                    else
                    {
                        return View(response);
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {   
                //Input validation fails, return to the Create view
                //to display error message
                return View(response);
            }
        }

        // GET: Response/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Response/Edit/5
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

        // GET: Response/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Response/Delete/5
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
