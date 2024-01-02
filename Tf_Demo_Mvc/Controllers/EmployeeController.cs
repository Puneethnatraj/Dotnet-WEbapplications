using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Text;
using Tf_Demo_Mvc.Models;

namespace Tf_Demo_Mvc.Controllers
{
    public class EmployeeController : Controller
    {
        Uri baseaddress = new Uri("http://localhost:5180/api");
        private readonly HttpClient _client;

        public EmployeeController()
        {
           _client = new HttpClient();
            _client.BaseAddress = baseaddress;
        }

        
        [HttpGet]
        public IActionResult Index()
        {
            List<EmployeeViewModel> EmployeeList= new List<EmployeeViewModel>();
            HttpResponseMessage response=_client.GetAsync(_client.BaseAddress + "/Employee/Get").Result;

            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                EmployeeList=JsonConvert.DeserializeObject<List<EmployeeViewModel>>(data);
            }
           
            return View(EmployeeList);
        }
        [HttpGet]
        public IActionResult Create() {

            return View();
        }
        public IActionResult Create(EmployeeViewModel model) 
        {
            try 
            { 
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Employee/Post", content).Result;
              if (response.IsSuccessStatusCode)
              {
                    TempData["SuccessMessage"] = "Employee Created.";
                return RedirectToAction("Index");
              }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

            return View();
        
        
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {

                EmployeeViewModel employee = new EmployeeViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Employee/Get/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    employee = JsonConvert.DeserializeObject<EmployeeViewModel>(data);
                    return View(employee);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;

                return View();
            }
            return View();
            
            
        
        }
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model) 
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress +"/Employee/Put", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Employee details updated";
                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;

                return View();
            }
            return View();
        
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + "/Employee/Delete/" + id);

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Employee deleted successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Failed to delete employee.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }




    }
}
