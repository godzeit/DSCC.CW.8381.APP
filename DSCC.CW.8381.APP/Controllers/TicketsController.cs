using DSCC.CW._8381.APP.DBO;
using DSCC.CW._8381.APP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DSCC.CW._8381.APP.Controllers
{
    public class TicketsController : Controller
    {
        private readonly string baseUrl = "https://localhost:5001/api/";
        private readonly string pathUrl = "Tickets";
        private readonly HttpClient client = new HttpClient();

        public TicketsController()
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            List<Ticket> list = new List<Ticket>();
            HttpResponseMessage response = await client.GetAsync(pathUrl);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<Ticket>>(result);
            }
            return View(list);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Count,SessionId")] TicketViewModel ticketViewModel)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(ticketViewModel);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(pathUrl, data);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(ticketViewModel);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket ticket;
            HttpResponseMessage response = await client.GetAsync(pathUrl + "/" + id.Value);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                ticket = JsonConvert.DeserializeObject<Ticket>(result);
                var ticketViewModel = new TicketViewModel(ticket);
                return View(ticketViewModel);
            }
            return NotFound();
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(pathUrl + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
