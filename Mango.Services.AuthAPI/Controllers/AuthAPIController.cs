using AutoMapper;
using Mango.MessageBus;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMessageBus _messageBus;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        protected ResponseDto _response;
        public AuthAPIController(IAuthService authService, IMessageBus messageBus, IConfiguration configuration, IMapper mapper)
        {
            _authService = authService;
            _configuration = configuration;
            this._mapper = mapper;
            _messageBus = messageBus;
            _response = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            await _messageBus.PublishMessage(model.Email, _configuration.GetValue<string>("TopicAndQueueNames:RegisterUserQueue"));
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or Password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());

            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost("GetUsers")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetUsers()
        {
            _response.Result = await _authService.GetUsers();

            if (_response.Result == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost(Name = "UpsertSecurityQuestion")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpsertSecurityQuestion([FromBody] SecurityQuestionRequestDto securityQuestionRequest)
        {
            var result = await _authService.UpsertSecurityQuestion(_mapper.Map<SecurityQuestions>( securityQuestionRequest));
            if (_response.Result == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);
        }

    }
}
