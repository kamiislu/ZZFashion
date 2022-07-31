using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB2022Apr_P02_T3.DAL;
using WEB2022Apr_P02_T3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEB2022Apr_P02_T3.Controllers
{
    public class FeedbackController : Controller
    {
        private FeedbackDAL feedbackContext = new FeedbackDAL();
        // GET: FeedbackController
        public ActionResult Index()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Marketing"))
            {
                return RedirectToAction("Index", "Home");
            }
            List<Feedback> feedbackList = feedbackContext.GetAllFeedback();
            return View(feedbackList);
        }

        // GET: FeedbackController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FeedbackController/Create
        public ActionResult Create()
        {
            string id = HttpContext.Session.GetString("LoginID");// Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Customer"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: FeedbackController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Feedback feedback, IFormCollection formData)
        {
            string title = formData["txtTitle"].ToString();
            string text = formData["txtFeedback"].ToString();
            string image = formData["txtImage"].ToString();
            if (ModelState.IsValid)
            {
                string id = HttpContext.Session.GetString("LoginID");
                feedback.MemberID = id;
                feedback.Title = title;
                feedback.Text = text;
                feedback.Image = image;
                //Add staff record to database
                feedback.FeedbackID = feedbackContext.Create(feedback);
                //Redirect user to Staff/Index view
                return RedirectToAction("CustomerMain", "Customer");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View();
            }
        }

        // GET: FeedbackController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FeedbackController/Edit/5
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

        // GET: FeedbackController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FeedbackController/Delete/5
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
