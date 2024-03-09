using AdminPanel.Models;
using AdminPanel.Service.IService;
using AdminPanel.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace AdminPanel.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto obj)
        {
            try
            {
                ResponseDto responseDto = await _authService.LoginAsync(obj);
                if (responseDto != null && responseDto.IsSuccess)
                {
                    LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));

                    await SignInUser(loginResponseDto);
                    _tokenProvider.SetToken(loginResponseDto.Token);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = responseDto.Message;
                    return View(obj);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(obj);
            }

        }

        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                new SelectListItem{Text=SD.RoleCustomer,Value=SD.RoleCustomer}
            };
            ViewBag.roleList = roleList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {
            ResponseDto result = await _authService.RegisterAsync(obj);
            ResponseDto assingRole;
            if (result != null && result.IsSuccess)
            {
                if (string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = SD.RoleAdmin;
                }
                assingRole = await _authService.AssignRoleAsync(obj);
                if (assingRole != null && assingRole.IsSuccess)
                {
                    TempData["success"] = "Registration Successfull";
                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData["Error"] = result.Message;
            }
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},  // New Admin Register
                new SelectListItem{Text=SD.RoleCustomer,Value=SD.RoleCustomer}
            };
            ViewBag.roleList = roleList;
            return View(obj);
        }

        [HttpGet]
        public IActionResult SecurityQuestion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpsertSecurityQuestion(SecurityQuestionRequestDto securityQuestionRequest)
        {
            ResponseDto result = await _authService.UpsertSecurityQuestion(securityQuestionRequest);
            return Json(new { data = "" });
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearedToken();
            return RedirectToAction(nameof(Login));
        }
        private async Task SignInUser(LoginResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(model.Token);
            if (jwt.Claims.FirstOrDefault(u => u.Type == "role").Value == "ADMIN")
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));


                identity.AddClaim(new Claim(ClaimTypes.Name,
                    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
                identity.AddClaim(new Claim(ClaimTypes.Role,
                    jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
            else
            {
                throw new Exception("Only an admin can login this application!!!");
            }
        }


    }

}
