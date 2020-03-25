using ElEmpleoProyecto.Models;
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

namespace ElEmpleoProyecto.Controllers
{
    public class VendedorController : Controller
    {
        private List<VendedorModel> oVendedores = new List<VendedorModel>();

        public ActionResult listVendedor()
        {

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:50720/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseTask = client.GetAsync("vendedor");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync().Result;

                    oVendedores = JsonConvert.DeserializeObject<List<VendedorModel>>(readTask);
                }

            }
            return View(oVendedores);
        }

        public ActionResult newVendedor()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> newVendedor(VendedorModel vendedor)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50720/api/");
                var postTask = await client.PostAsJsonAsync<VendedorModel>("vendedor", vendedor);


                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("listVendedor");
                }
                ModelState.AddModelError(String.Empty, "Error");
                return View(oVendedores);
            }

        }

        public async Task<ActionResult> ModifyVendedor(int id)
        {
            VendedorModel vendedor = new VendedorModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50720/api/");
                var responseTask = await client.GetAsync("vendedor/" + id.ToString());


                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsStringAsync().Result;
                    vendedor = JsonConvert.DeserializeObject<VendedorModel>(readTask);

                }
            }
            return View(vendedor);
        }


        [HttpPost]
        public async Task<ActionResult> ModifyVendedor(VendedorModel vendedor)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50720/api/");
                var postTask = await client.PutAsJsonAsync($"vendedor/{ vendedor.Codigo}", vendedor);


                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("listVendedor");
                }
                return View(vendedor);
            }

        }

        public ActionResult DeleteVendedor(int id)
        {
            VendedorModel vendedor = new VendedorModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50720/api/");
                var responseTask = client.GetAsync("vendedor/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<VendedorModel>();
                    readTask.Wait();
                    vendedor = readTask.Result;

                }
            }
            return View(vendedor);

        }

        [HttpPost]
        public async Task<ActionResult> DeleteVendedor(VendedorModel vendedor, int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50720/api/");
                var postTask = await client.DeleteAsync($"vendedor/" + id.ToString());


                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("listVendedor");
                }
                return View(vendedor);
            }

        }

    }

}