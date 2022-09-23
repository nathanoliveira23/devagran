using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Devagran.Dtos;

namespace Devagran.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequestDto loginRequest)
        {
            try 
            {
                throw new Exception("Erro ao efetuar o login.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no login: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                {
                    Description = "Ocorreru um erro ao realizar o login",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}