using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElEmpleoProyecto.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;

namespace ElEmpleoProyecto.Controllers
{

    public class CiudadController : Controller
    {
        private List<CiudadModel> oCiudades = new List<CiudadModel>();

        public async Task<ActionResult> listCiudad()
        {

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:50720/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseTask = await client.GetAsync("ciudad");


                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsStringAsync().Result;
                    oCiudades = JsonConvert.DeserializeObject<List<CiudadModel>>(readTask);
                }
                return View(oCiudades);
            }
            
        }


        public ActionResult newCiudad()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> newCiudad(CiudadModel ciudad)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50720/api/");
                var postTask = await client.PostAsJsonAsync<CiudadModel>("ciudad", ciudad);


                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("listCiudad");
                }
                ModelState.AddModelError(String.Empty, "Error");
                return View(oCiudades);
            }

        }

        public async Task<ActionResult> ModifyCiudad(int id)
        {
            CiudadModel ciudad = new CiudadModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50720/api/");
                var responseTask = await client.GetAsync("ciudad/"+id.ToString());


                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask=responseTask.Content.ReadAsStringAsync().Result;
                    ciudad = JsonConvert.DeserializeObject<CiudadModel>(readTask);
                    
                }
            }
            return View(ciudad);
        }


        [HttpPost]
        public async Task<ActionResult> ModifyCiudad(CiudadModel ciudad)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50720/api/");
                var postTask = await client.PutAsJsonAsync($"ciudad/{ ciudad.Codigo}", ciudad);


                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("listCiudad");
                }
                return View(ciudad);
            }

        }

        public ActionResult DeleteCiudad(int id)
        {
            CiudadModel ciudad = new CiudadModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50720/api/");
                var responseTask =  client.GetAsync("ciudad/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CiudadModel>();
                    readTask.Wait();
                    ciudad = readTask.Result;

                }
            }
            return View(ciudad);

        }

        [HttpPost]
        public async Task<ActionResult> DeleteCiudad(CiudadModel ciudad, int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50720/api/");
                var postTask = await client.DeleteAsync($"ciudad/" + id.ToString());


                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("listCiudad");
                }
                return View(ciudad);
            }

        }

    }
}