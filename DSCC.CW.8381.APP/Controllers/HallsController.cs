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
    public class HallsController : Controller
    {
        private readonly string baseUrl = "https://localhost:5001/api/";
        private readonly string pathUrl = "Halls";
        private readonly string cinemasPathUrl = "Cinemas";
        private readonly HttpClient client = new HttpClient();

        public HallsController()
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Halls
        public async Task<IActionResult> Index()
        {
            List<Hall> list = new List<Hall>();
            HttpResponseMessage response = await client.GetAsync(pathUrl);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<Hall>>(result);
            }
            return View(list);
        }

        // GET: Halls/Create
        public async Task<IActionResult> Create()
        {
            var hallViewModel = new HallViewModel();

            List<Cinema> cinemas = new List<Cinema>();
            HttpResponseMessage cinemasResponse = await client.GetAsync(cinemasPathUrl);
            if (cinemasResponse.IsSuccessStatusCode)
            {
                var cinemasResult = cinemasResponse.Content.ReadAsStringAsync().Result;
                cinemas = JsonConvert.DeserializeObject<List<Cinema>>(cinemasResult);
            }

            hallViewModel.Cinemas = new SelectList(cinemas, "Id", "Name");
            return View(hallViewModel);
        }

        // POST: Halls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PlacesCount,CinemaId")] HallViewModel hallViewModel)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(hallViewModel);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(pathUrl, data);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            List<Cinema> cinemas = new List<Cinema>();
            HttpResponseMessage cinemasResponse = await client.GetAsync(cinemasPathUrl);
            if (cinemasResponse.IsSuccessStatusCode)
            {
                var cinemasResult = cinemasResponse.Content.ReadAsStringAsync().Result;
                cinemas = JsonConvert.DeserializeObject<List<Cinema>>(cinemasResult);
            }

            hallViewModel.Cinemas = new SelectList(cinemas, "Id", "Name");

            return View(hallViewModel);
        }

        // GET: Halls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Hall hall;
            HttpResponseMessage response = await client.GetAsync(pathUrl + "/" + id.Value);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                hall = JsonConvert.DeserializeObject<Hall>(result);
                var hallViewModel = new HallViewModel(hall);

                List<Cinema> cinemas = new List<Cinema>();
                HttpResponseMessage cinemasResponse = await client.GetAsync(cinemasPathUrl);
                if (cinemasResponse.IsSuccessStatusCode)
                {
                    var cinemasResult = cinemasResponse.Content.ReadAsStringAsync().Result;
                    cinemas = JsonConvert.DeserializeObject<List<Cinema>>(cinemasResult);
                }

                hallViewModel.Cinemas = new SelectList(cinemas, "Id", "Name", hallViewModel.CinemaId);

                return View(hallViewModel);
            }
            return NotFound();
        }

        // POST: Halls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PlacesCount,CinemaId")] HallViewModel hallViewModel)
        {
            if (id != hallViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(hallViewModel);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(pathUrl + "/" + id, data);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return NotFound();
            }

            List<Cinema> cinemas = new List<Cinema>();
            HttpResponseMessage cinemasResponse = await client.GetAsync(cinemasPathUrl);
            if (cinemasResponse.IsSuccessStatusCode)
            {
                var cinemasResult = cinemasResponse.Content.ReadAsStringAsync().Result;
                cinemas = JsonConvert.DeserializeObject<List<Cinema>>(cinemasResult);
            }

            hallViewModel.Cinemas = new SelectList(cinemas, "Id", "Name", hallViewModel.CinemaId);

            return View(hallViewModel);
        }

        // GET: Halls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Hall hall;
            HttpResponseMessage response = await client.GetAsync(pathUrl + "/" + id.Value);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                hall = JsonConvert.DeserializeObject<Hall>(result);
                var hallViewModel = new HallViewModel(hall);
                return View(hallViewModel);
            }
            return NotFound();
        }

        // POST: Halls/Delete/5
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
