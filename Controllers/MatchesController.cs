using FootballLeague.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using FootballLeague.Models.ViewModels;

namespace FootballLeague.Controllers
{
    public class MatchesController : Controller
    {
        private static readonly HttpClient client;

        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static MatchesController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44397/api/");
        }

        // GET: Matches
        public ActionResult List()
        {
          
            string url = "MatchesData/ListMatches";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<MatchesDto> matches = response.Content.ReadAsAsync<IEnumerable<MatchesDto>>().Result;

            return View(matches);
        }

        // GET: Matches/Details/5
        public ActionResult Details(int id)
        {
            DetailsMatches ViewModel = new DetailsMatches(); 

            string url = "MatchesData/FindMatches/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            MatchesDto matchesSelected = response.Content.ReadAsAsync<MatchesDto>().Result;

            ViewModel.selectedMatches = matchesSelected;


            url = "FootballData/ListFootballForMatches/" + id;
            response = client.GetAsync(url).Result;

            IEnumerable<FootballDto> playedClubs = response.Content.ReadAsAsync<IEnumerable<FootballDto>>().Result;

            ViewModel.ResponsibleFootball = playedClubs; 

            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Matches/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Matches/Create
        [HttpPost]
        public ActionResult Create(Matches matches)
        {
            string url = "MatchesData/AddMatches";
            
            string jsonPayload = jss.Serialize(matches);
            Debug.WriteLine(jsonPayload);
            HttpContent content = new StringContent(jsonPayload);

            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Matches/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "MatchesData/findMatches/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is");
            Debug.WriteLine(response.StatusCode);

            MatchesDto matchesDto = response.Content.ReadAsAsync<MatchesDto>().Result;


            return View(matchesDto);
        }

        // POST: Matches/Update/5
        [HttpPost]
        public ActionResult Update(int id, Matches matches)
        {
            string url = "MatchesData/UpdateMatches/" + id;

            string jsonPayload = jss.Serialize(matches);
            HttpContent content = new StringContent(jsonPayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(jsonPayload);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Matches/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "MatchesData/findmatches/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is");
            //Debug.WriteLine(response.StatusCode);

            MatchesDto selectedMatch = response.Content.ReadAsAsync<MatchesDto>().Result;


            return View(selectedMatch);
        }

        // POST: Matches/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Matches matches)
        {
            Debug.WriteLine("the json payload is :");
            string url = "MatchesData/DeleteMatches/" + id;


            string jsonpayload = jss.Serialize(matches);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);

            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
