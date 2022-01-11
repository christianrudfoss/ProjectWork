using App.Extensions;
using App.Models;
using ApplicationCore.Entities.UserAggregate;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class UserController : Controller
    {
        //Hosted web API REST Service base url
        string Baseurl = "https://localhost:44343/ProjectWork";
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<ActionResult> Users()
        {
            List<User> lstUsers = new List<User>();
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("Api/Users/GetAll");

                if (Res.IsSuccessStatusCode)
                {
                    var ProjectWorkResponse = Res.Content.ReadAsStringAsync().Result;
                    lstUsers = JsonConvert.DeserializeObject<List<User>>(ProjectWorkResponse);
                }

                IEnumerable<UserResponseModel> model = _mapper.Map<IEnumerable<UserResponseModel>>(lstUsers);
                return View(model);
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                User user = await GetUser(id);

                if (user == null)
                    return NotFound();
                else
                {
                    UserResponseModel model = _mapper.Map<UserResponseModel>(user);
                    foreach (var item in model.Work)
                        item.SetMinutesOfWork();

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }

        private async Task<User> GetUser(int? id)
        {
            User user = new();
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var Res = await client.GetAsync(string.Format("Api/Users/Get/{0}", id));

                
                if (Res.IsSuccessStatusCode)
                {
                    var ProjectWorkResponse = Res.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<User>(ProjectWorkResponse);
                }
            }
            catch (Exception)
            {
            }
            return user;
        }
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                User user = await GetUser(id);

                if (user == null)
                    return NotFound();
                else
                {
                    UserResponseModel model = _mapper.Map<UserResponseModel>(user);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Email,CreatedDate,CreatedBy, Password,Gender,Id")] UpdateUserRequestModel requestmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            try
            {
                var model = _mapper.Map<User>(requestmodel);

                using var client = new HttpClient();
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                using var response = await client.PostAsync(string.Format("Api/Users/Edit/{0}", id), content);

                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;

                if (result.ToLower().Equals("success"))
                    return RedirectToAction(nameof(Details), new { id = model.Id });
                else
                {
                    ViewBag.Status = result;
                    return View();
                }
                    
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,CreatedDate,CreatedBy,Password,Gender")] CreateUserRequestModel requestmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                var model = _mapper.Map<User>(requestmodel);
                model.CreatedBy = 2;
                model.CreatedDate = DateTime.Now;

                var content = new StringContent(JsonConvert.SerializeObject(model),Encoding.UTF8, "application/json");
                using var response = await client.PostAsync("Api/Users/Create", content);
                response.EnsureSuccessStatusCode();
                var responseString = response.Content.ReadAsStringAsync().Result;
                var newUser= JsonConvert.DeserializeObject<User>(responseString);
                return RedirectToAction(nameof(Details), new { id = newUser.Id });
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                User user = await GetUser(id);

                if (user == null)
                    return NotFound();
                else
                {
                    UserResponseModel model = _mapper.Map<UserResponseModel>(user);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
                return NotFound();
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(string.Format("Api/Users/Delete/{0}", id));

                response.EnsureSuccessStatusCode();
                var responseString = response.Content.ReadAsStringAsync().Result;
                string result = JsonConvert.DeserializeObject<string>(responseString);

                if (result.ToLower().Equals("success"))
                    return RedirectToAction(nameof(Users));
                else
                {
                    ViewBag.Status = result;
                    return View();
                }
                   
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }
    }
}
