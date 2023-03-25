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
    public class FootballController : Controller
    {
        private static readonly HttpClient client;

        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static FootballController ()
        {
            client = new HttpClient ();
            client.BaseAddress = new Uri("https://localhost:44397/api/");

        }


        // GET: Football
        public ActionResult List()
        {

            string url = "FootballData/Listfootballs";
            HttpResponseMessage response = client.GetAsync(url).Result;;

            IEnumerable<FootballDto> footballs = response.Content.ReadAsAsync<IEnumerable<FootballDto>>().Result;

            return View(footballs);
        }

        // GET: Football/Details/5
        public ActionResult Details(int id)
        {
          DetailsFootball ViewModel = new DetailsFootball();

            string url = "FootballData/findfootball/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            FootballDto clubSelected = response.Content.ReadAsAsync<FootballDto>().Result;

            ViewModel.selectedFootball = clubSelected;

            url = "MatchesData/ListMatchesPlayed/" + id;
            response = client.GetAsync(url).Result;

            IEnumerable<MatchesDto> matchesPlayed = response.Content.ReadAsAsync<IEnumerable<MatchesDto>>().Result;

            ViewModel.ResponsibleMatches = matchesPlayed;

            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }
        // GET: Football/New
        public ActionResult New()
        {

            return View();
        }

        // POST: Football/Create
        [HttpPost]
        public ActionResult Create(Football football)
        {
            string url = "FootballData/AddFootball";
            
            string jsonPayload = jss.Serialize(football);
            HttpContent content= new StringContent(jsonPayload);
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

        // GET: Football/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "FootballData/findfootball/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is");
            Debug.WriteLine(response.StatusCode);

            FootballDto selectedClub = response.Content.ReadAsAsync<FootballDto>().Result;
          

            return View(selectedClub);
        }

        // POST: Football/Edit/5
        [HttpPost]
        public ActionResult Update(int id, Football football)
        {
            string url = "FootballData/UpdateFootball/" + id;

            string jsonPayload = jss.Serialize(football);
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

        // GET: Football/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FootballData/findfootball/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is");
            //Debug.WriteLine(response.StatusCode);

            FootballDto selectedClub = response.Content.ReadAsAsync<FootballDto>().Result;
           

            return View(selectedClub);
        }

        // POST: Football/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Football football)
        {
            Debug.WriteLine("the json payload is :");
            string url = "FootballData/DeleteFootball/" + id;


            string jsonpayload = jss.Serialize(football);

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
