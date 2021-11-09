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
    public class SessionsController : Controller
    {
        private readonly string baseUrl = "https://localhost:5001/api/";
        private readonly string pathUrl = "Sessions";
        private readonly string hallsPathUrl = "Halls";
        private readonly string moviesPathUrl = "Movies";
        private readonly HttpClient client = new HttpClient();


        public SessionsController()
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Sessions
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

        // GET: Sessions/Create
        public async Task<IActionResult> Create()
        {
            var sessionViewModel = new SessionViewModel();

            List<Hall> halls = new List<Hall>();
            HttpResponseMessage hallsResponse = await client.GetAsync(hallsPathUrl);
            if (hallsResponse.IsSuccessStatusCode)
            {
                var hallsResult = hallsResponse.Content.ReadAsStringAsync().Result;
                halls = JsonConvert.DeserializeObject<List<Hall>>(hallsResult);
            }

            sessionViewModel.Halls = new SelectList(halls, "Id", "FullName");

            List<Movie> movies = new List<Movie>();
            HttpResponseMessage moviesResponse = await client.GetAsync(moviesPathUrl);
            if (moviesResponse.IsSuccessStatusCode)
            {
                var moviesResult = moviesResponse.Content.ReadAsStringAsync().Result;
                movies = JsonConvert.DeserializeObject<List<Movie>>(moviesResult);
            }

            sessionViewModel.Movies = new SelectList(movies, "Id", "Name");

            return View(sessionViewModel);
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HallId,MovieId,DateTime")] SessionViewModel sessionViewModel)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(sessionViewModel);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(pathUrl, data);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            List<Hall> halls = new List<Hall>();
            HttpResponseMessage hallsResponse = await client.GetAsync(hallsPathUrl);
            if (hallsResponse.IsSuccessStatusCode)
            {
                var hallsResult = hallsResponse.Content.ReadAsStringAsync().Result;
                halls = JsonConvert.DeserializeObject<List<Hall>>(hallsResult);
            }

            sessionViewModel.Halls = new SelectList(halls, "Id", "FullName");

            List<Movie> movies = new List<Movie>();
            HttpResponseMessage moviesResponse = await client.GetAsync(moviesPathUrl);
            if (moviesResponse.IsSuccessStatusCode)
            {
                var moviesResult = moviesResponse.Content.ReadAsStringAsync().Result;
                movies = JsonConvert.DeserializeObject<List<Movie>>(moviesResult);
            }

            sessionViewModel.Movies = new SelectList(movies, "Id", "Name");

            return View(sessionViewModel);
        }

        // GET: Sessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Session session;
            HttpResponseMessage response = await client.GetAsync(pathUrl + "/" + id.Value);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                session = JsonConvert.DeserializeObject<Session>(result);
                var sessionViewModel = new SessionViewModel(session);

                List<Hall> halls = new List<Hall>();
                HttpResponseMessage hallsResponse = await client.GetAsync(hallsPathUrl);
                if (hallsResponse.IsSuccessStatusCode)
                {
                    var hallsResult = hallsResponse.Content.ReadAsStringAsync().Result;
                    halls = JsonConvert.DeserializeObject<List<Hall>>(hallsResult);
                }

                sessionViewModel.Halls = new SelectList(halls, "Id", "FullName", sessionViewModel.HallId);

                List<Movie> movies = new List<Movie>();
                HttpResponseMessage moviesResponse = await client.GetAsync(moviesPathUrl);
                if (moviesResponse.IsSuccessStatusCode)
                {
                    var moviesResult = moviesResponse.Content.ReadAsStringAsync().Result;
                    movies = JsonConvert.DeserializeObject<List<Movie>>(moviesResult);
                }

                sessionViewModel.Movies = new SelectList(movies, "Id", "Name", sessionViewModel.MovieId);

                return View(sessionViewModel);
            }
            return NotFound();
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HallId,MovieId,DateTime")] SessionViewModel sessionViewModel)
        {
            if (id != sessionViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(sessionViewModel);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(pathUrl + "/" + id, data);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return NotFound();
            }

            List<Hall> halls = new List<Hall>();
            HttpResponseMessage hallsResponse = await client.GetAsync(hallsPathUrl);
            if (hallsResponse.IsSuccessStatusCode)
            {
                var hallsResult = hallsResponse.Content.ReadAsStringAsync().Result;
                halls = JsonConvert.DeserializeObject<List<Hall>>(hallsResult);
            }

            sessionViewModel.Halls = new SelectList(halls, "Id", "FullName", sessionViewModel.HallId);

            List<Movie> movies = new List<Movie>();
            HttpResponseMessage moviesResponse = await client.GetAsync(moviesPathUrl);
            if (moviesResponse.IsSuccessStatusCode)
            {
                var moviesResult = moviesResponse.Content.ReadAsStringAsync().Result;
                movies = JsonConvert.DeserializeObject<List<Movie>>(moviesResult);
            }

            sessionViewModel.Movies = new SelectList(movies, "Id", "Name", sessionViewModel.MovieId);

            return View(sessionViewModel);
        }

        // GET: Sessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Session session;
            HttpResponseMessage response = await client.GetAsync(pathUrl + "/" + id.Value);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                session = JsonConvert.DeserializeObject<Session>(result);
                var sessionViewModel = new SessionViewModel(session);
                return View(sessionViewModel);
            }
            return NotFound();
        }

        // POST: Sessions/Delete/5
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
