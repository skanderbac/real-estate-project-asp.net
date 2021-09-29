using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
                IEnumerable<Achat> table = JsonConvert.DeserializeObject<IEnumerable<Achat>>(data);
                
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
                a.titre = collection.Get("titre");
                a.description = collection.Get("description");
                a.date = DateTime.Now;
                a.phone = collection.Get("phone");
                a.name = collection.Get("name");
                a.status = collection.Get("status");
                a.type = collection.Get("type");
                a.address = collection.Get("address");
                a.city = collection.Get("city");
                a.email = collection.Get("email");
                a.phone = collection.Get("phone");
                a.bathrooms = int.Parse(collection.Get("bathrooms"));
                a.bedrooms = int.Parse(collection.Get("bedrooms"));
                a.areas = int.Parse(collection.Get("areas"));
                a.price = double.Parse(collection.Get("price"));
                a.age = collection.Get("age");
                if (!string.IsNullOrEmpty(collection["elevator"]))
                {
                    a.elevator = true;
                }
                if (!string.IsNullOrEmpty(collection["internet"]))
                {
                    a.internet = true;
                }
                if (!string.IsNullOrEmpty(collection["ac"]))
                {
                    a.ac = true;
                }
                if (!string.IsNullOrEmpty(collection["parking"]))
                {
                    a.parking = true;
                }
                if (!string.IsNullOrEmpty(collection["pool"]))
                {
                    a.pool = true;
                }
                if (Request.Files.Count > 0)
                {
                    try
                    {
                        HttpFileCollectionBase files = Request.Files;

                        HttpPostedFileBase file = files[0];
                        string fileName = file.FileName;

                        // create the uploads folder if it doesn't exist
                        Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/"));
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles/"), fileName);
                        file.SaveAs(path);
                        a.photo = fileName;
                    }
                    catch (Exception e)
                    {
                        
                    }
                }

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
