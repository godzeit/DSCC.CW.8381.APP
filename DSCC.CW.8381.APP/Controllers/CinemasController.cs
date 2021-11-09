using DSCC.CW._8381.APP.DBO;
using DSCC.CW._8381.APP.Models;
using Microsoft.AspNetCore.Mvc;
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
    public class CinemasController : Controller
    {
        private readonly string baseUrl = "http://dscccw8381api-test.us-east-1.elasticbeanstalk.com/api/";
        private readonly string pathUrl = "Cinemas";
        private readonly HttpClient client = new HttpClient();
       

        public CinemasController()
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Cinemas
        public async Task<IActionResult> Index()
        {
            List<Cinema> list = new List<Cinema>();
            HttpResponseMessage response = await client.GetAsync(pathUrl);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<Cinema>>(result);
            }
            return View(list);
        }

        // GET: Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cinemas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CinemaViewModel cinemaViewModel)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(cinemaViewModel);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(pathUrl, data);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(cinemaViewModel);
        }

        // GET: Cinemas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cinema cinema;
            HttpResponseMessage response = await client.GetAsync(pathUrl + "/" + id.Value);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                cinema = JsonConvert.DeserializeObject<Cinema>(result);
                var cinemaViewModel = new CinemaViewModel(cinema);
                return View(cinemaViewModel);
            }

            return NotFound();
            
        }

        // POST: Cinemas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CinemaViewModel cinemaViewModel)
        {
            if (id != cinemaViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(cinemaViewModel);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(pathUrl + "/" + id, data);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return NotFound();
            }
            return View(cinemaViewModel);
        }

        // GET: Cinemas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cinema cinema;
            HttpResponseMessage response = await client.GetAsync(pathUrl + "/" + id.Value);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                cinema = JsonConvert.DeserializeObject<Cinema>(result);
                var cinemaViewModel = new CinemaViewModel(cinema);
                return View(cinemaViewModel);
            }
            return NotFound();
        }

        // POST: Cinemas/Delete/5
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
