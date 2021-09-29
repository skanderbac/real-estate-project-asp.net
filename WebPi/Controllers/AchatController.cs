using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebPi.Models;

namespace WebPi.Controllers
{
    public class AchatController : Controller
    {
        // GET: Achat
        public async Task<ActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:8080/SpringMVC/servlet/allAchat");
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var table = JsonConvert.DeserializeObject<IEnumerable<Achat>>(data);
                
                ViewBag.result = table;
            }
            else
            {
                ViewBag.result = "error";
            }
            return View(ViewBag.result);
        }

        // GET: Achat/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:8080/SpringMVC/servlet/getAchat/"+id);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var a = JsonConvert.DeserializeObject<Achat>(data);

                ViewBag.result = a;
            }
            else
            {
                ViewBag.result = "error";
            }
            return View(ViewBag.result);
        }

        // GET: Achat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Achat/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Achat a = new Achat();
                a.titre = collection.Get(1);
                a.description = collection.Get(2);
                a.date = DateTime.Now;
                var stringContent = new StringContent(JsonConvert.SerializeObject(a), Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync("http://localhost:8080/SpringMVC/servlet/addAchat", stringContent);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.result = response;
                    return View(ViewBag.result);
                }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Achat/Edit/5
        public ActionResult Edit(int id)
        {

            return View();
        }

        // POST: Achat/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // POST: Achat/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(FormCollection collection)
        {
            try
            {
                Achat a = new Achat();
                a.id = int.Parse(collection.Get("id"));
                var stringContent = new StringContent(JsonConvert.SerializeObject(a), Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync("http://localhost:8080/SpringMVC/servlet/deleteAchat", stringContent);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
