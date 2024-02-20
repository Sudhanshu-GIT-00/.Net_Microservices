using AdminPanel.Models;
using AdminPanel.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdminPanel.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            List<UserDto>? userList = new();
            var response = await _customerService.GetUser();
            if (response != null && response.IsSuccess)
            {
                userList = JsonConvert.DeserializeObject<List<UserDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(userList);
        }
    }
}
