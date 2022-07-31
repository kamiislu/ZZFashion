using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB2022Apr_P02_T3.DAL;
using WEB2022Apr_P02_T3.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;

namespace WEB2022Apr_P02_T3.Controllers
{
    public class CreditCardController : Controller
    {
        // GET: CreditCardController
        public async Task<ActionResult> Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(
                "https://date.nager.at/api/v3/publicholidays/2017/AT");
            HttpResponseMessage response = await client.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Console.WriteLine(data);
                List<CreditCard> creditCardList = JsonConvert.DeserializeObject<List<CreditCard>>(data);
                Console.WriteLine(creditCardList);
                return View(creditCardList);
            }
            else
            {
                return View(new List<CreditCard>());
            }
        }

        // GET: CreditCardController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CreditCardController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CreditCardController/Create
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

        // GET: CreditCardController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CreditCardController/Edit/5
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

        // GET: CreditCardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CreditCardController/Delete/5
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
