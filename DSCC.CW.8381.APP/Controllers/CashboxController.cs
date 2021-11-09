using DSCC.CW._8381.APP.DBO;
using DSCC.CW._8381.APP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DSCC.CW._8381.APP.Controllers
{
    public class CashboxController : Controller
    {
        private readonly string baseUrl = "https://localhost:5001/api/";
        private readonly string pathUrl = "Cashbox";
        private readonly string sesssionsUrl = "Sessions";
        private readonly HttpClient client = new HttpClient();

        public CashboxController()
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Cashbox
        public async Task<IActionResult> Index()
        {
            List<Session> list = new List<Session>();
            HttpResponseMessage response = await client.GetAsync(pathUrl);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<Session>>(result);
            }
            return View(list);
        }

        // GET: Cashbox/Sell/5
        public async Task<IActionResult> Sell(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await client.GetAsync(sesssionsUrl + "/" + id.Value);
            if (response.IsSuccessStatusCode)
            {
                return View();
            }
            return NotFound();
        }

        // POST: Cashbox/Sell/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sell(int id, [Bind("Count,SessionId")] TicketViewModel ticketViewModel)
        {
            ticketViewModel.SessionId = id;
            var json = JsonConvert.SerializeObject(ticketViewModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(pathUrl, data);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError("Count", $"Not enough available tickets");
                /*ModelState.AddModelError("Count", $"Not enough available tickets. {response.Content} left");*/
            }
            return View(ticketViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
