using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using ActivitySignUp.Web.ViewModels;
using System.Text.Json;
using System.Net.Http.Headers;

namespace ActivitySignUp.Web.Controllers
{
    public class AppController : Controller
    {
        private readonly HttpClient _client;
        public AppController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:8888/api/");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("signup")]
        public IActionResult SignUp()
        {
            IEnumerable<ActivityViewModel> activities;
            var readApi = _client.GetAsync("activities");
            readApi.Wait();

            var responseMessage = readApi.Result;

            if(responseMessage.IsSuccessStatusCode)
            {
                var data = responseMessage.Content.ReadAsAsync<IList<ActivityViewModel>>();
                data.Wait();
                activities = data.Result;
            }
            else
            {
                activities = Enumerable.Empty<ActivityViewModel>();
                ModelState.AddModelError("Error","No Records Found!");
            }

            ViewBag.FunActivities = activities;
            return View();
        }

        [HttpPost("signup")]
        public IActionResult SignUp(SubscriptionViewModel model)
        {

            if (ModelState.IsValid)
            {
                var serializedModel = JsonSerializer.Serialize(model);

                var request = new HttpRequestMessage(HttpMethod.Post, "subscriptions");
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Content = new StringContent(serializedModel);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = _client.SendAsync(request);
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var data = response.Result.Content.ReadAsAsync<SubscriptionViewModel>();
                    data.Wait();
                    return RedirectToAction("subscribers", new { activityId = data.Result.ActivityId }); //("myActionName", new { value1 = "queryStringValue1" });
                }
                else
                {
                    ModelState.AddModelError("Error", "Failed to Subscribe!");
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Unable to Subscribe!");
            }

            return View(model);
        }

        [HttpGet("subscribers")]
        public IActionResult Subscribers(int activityId)
        {
            ViewBag.Title = "Subscribers";

            IEnumerable<SubscriptionViewModel> subscriptions;

            _client.BaseAddress = new Uri("http://localhost:8888/api/");

            var readApi = _client.GetAsync($"subscriptions/{activityId}");
            readApi.Wait();

            var responseMessage = readApi.Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                var data = responseMessage.Content.ReadAsAsync<IList<SubscriptionViewModel>>();
                data.Wait();
                subscriptions = data.Result;
            }
            else
            {
                subscriptions = Enumerable.Empty<SubscriptionViewModel>();
                ModelState.AddModelError("Error", "No Records Found!");
            }
            return View(subscriptions);
        }
    }
}
