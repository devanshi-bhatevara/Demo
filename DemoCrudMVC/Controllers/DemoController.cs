using DemoCrudMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace DemoCrudMVC.Controllers
{
    public class DemoController : Controller
    {
        private readonly IHttpClientService _httpClientService;
        private string endPoint;

        public DemoController(IHttpClientService httpClientService, IConfiguration configuration)
        {
            _httpClientService = httpClientService;
            endPoint = configuration["EndPoint:MyDemoApi"];

        }
        public IActionResult Index()
        {
            var apiGetContactsUrl = $"{endPoint}Demo/GetAll"; ;

            ServiceResponse<IEnumerable<DemoViewModel>> response = new ServiceResponse<IEnumerable<DemoViewModel>>();

            response = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<DemoViewModel>>>
                (apiGetContactsUrl, HttpMethod.Get, HttpContext.Request);

            if (response.Success)
            {
                return View(response.Data);
            }

            return View(new List<DemoViewModel>());
        }

        public IActionResult Create()
        {
            AddDemoViewModel viewModel = new AddDemoViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(AddDemoViewModel addDemoViewModel)
        {
            if (ModelState.IsValid)
            {
                var apiUrl = $"{endPoint}Demo/Create";
                var response = _httpClientService.PostHttpResponseMessage<AddDemoViewModel>(apiUrl, addDemoViewModel, HttpContext.Request);
                if (response.IsSuccessStatusCode)
                {
                    string successMessage = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(successMessage);

                    TempData["successMessage"] = serviceResponse?.Message;
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorMessage = response.Content.ReadAsStringAsync().Result;
                    var errorResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(errorMessage);
                    if (errorResponse != null)
                    {
                        TempData["errorMessage"] = errorResponse.Message;

                    }
                    else
                    {
                        TempData["errorMessage"] = "Something went wrong. Please try after sometime";
                    }


                }

            }
            return View(addDemoViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var apiUrl = $"{endPoint}Demo/GetDemoById/" + id;
            var response = _httpClientService.GetHttpResponseMessage<DemoViewModel>(apiUrl, HttpContext.Request);

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<DemoViewModel>>(data);

                if (serviceResponse != null && serviceResponse.Success && serviceResponse.Data != null)
                {
                    DemoViewModel viewModel = serviceResponse.Data;
                    return View(viewModel);
                }
                else
                {
                    TempData["errorMessage"] = serviceResponse?.Message;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                string errorData = response.Content.ReadAsStringAsync().Result;
                var errorResponse = JsonConvert.DeserializeObject<ServiceResponse<DemoViewModel>>(errorData);
                if (errorResponse != null)
                {
                    TempData["errorMessage"] = errorResponse.Message;

                }
                else
                {
                    TempData["errorMessage"] = "Something went wrong. Please try after sometime";
                }
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult Edit(DemoViewModel demoViewModel)
        {
            if (ModelState.IsValid)
            {
                var apiUrl = $"{endPoint}Demo/Modify";

                HttpResponseMessage response = _httpClientService.PutHttpResponseMessage(apiUrl, demoViewModel, HttpContext.Request);

                if (response.IsSuccessStatusCode)
                {
                    string successMessage = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(successMessage);

                    TempData["successMessage"] = serviceResponse?.Message;
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorMessage = response.Content.ReadAsStringAsync().Result;
                    var errorResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(errorMessage);
                    if (errorResponse != null)
                    {
                        TempData["errorMessage"] = errorResponse.Message;

                    }
                    else
                    {
                        TempData["errorMessage"] = "Something went wrong. Please try after sometime";
                    }
                    return RedirectToAction("Index");

                }
            }

            return View(demoViewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var apiUrl = $"{endPoint}Demo/Remove/" + id;
            var response = _httpClientService.ExecuteApiRequest<ServiceResponse<string>>($"{apiUrl}", HttpMethod.Delete, HttpContext.Request);
            if (response.Success)
            {
                TempData["successMessage"] = response.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["errorMessage"] = response.Message;
                return RedirectToAction("Index");
            }

        }



    }
}
