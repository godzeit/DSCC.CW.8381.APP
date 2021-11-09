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
    public class MoviesController : Controller
    {
        private readonly string baseUrl = "https://localhost:5001/api/";
        private readonly string pathUrl = "Movies";
        private readonly HttpClient client = new HttpClient();


        public MoviesController()
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            List<Movie> list = new List<Movie>();
            HttpResponseMessage response = await client.GetAsync(pathUrl);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<Movie>>(result);
            }
            return View(list);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Category,Duration")] MovieViewModel movieViewModel)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(movieViewModel);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(pathUrl, data);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(movieViewModel);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie movie;
            HttpResponseMessage response = await client.GetAsync(pathUrl + "/" + id.Value);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                movie = JsonConvert.DeserializeObject<Movie>(result);
                var movieViewModel = new MovieViewModel(movie);
                return View(movieViewModel);
            }
            return NotFound();
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Category,Duration")] MovieViewModel movieViewModel)
        {
            if (id != movieViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(movieViewModel);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(pathUrl + "/" + id, data);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return NotFound();
            }
            return View(movieViewModel);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie movie;
            HttpResponseMessage response = await client.GetAsync(pathUrl + "/" + id.Value);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                movie = JsonConvert.DeserializeObject<Movie>(result);
                var movieViewModel = new MovieViewModel(movie);
                return View(movieViewModel);
            }
            return NotFound();
        }

        // POST: Movies/Delete/5
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
