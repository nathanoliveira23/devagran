using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Devagran.Dtos;
using Devagran.Models;
using Devagran.Services;

namespace Devagran.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
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
                if (!String.IsNullOrEmpty(loginRequest.Email) &&
                    !String.IsNullOrEmpty(loginRequest.Password) && 
                    !String.IsNullOrWhiteSpace(loginRequest.Email) && 
                    !String.IsNullOrWhiteSpace(loginRequest.Password))
                {
                    string email = "nathan@email.com";
                    string password = "coringao1910";

                    if (loginRequest.Email == email && loginRequest.Password == password)
                    {
                        User user = new User() 
                        {
                            Id = 12,
                            Name = "Nathan Oliveira",
                            Email = loginRequest.Email,
                        };

                        return Ok(new LoginResponseDto()
                        {
                          Name = user.Name,
                          Email = user.Email,
                          Token = TokenService.CreateToken(user),
                        });
                    }
                    else
                    {
                        return BadRequest(new ErrorResponseDto()
                        {
                            Description = "O usuário não preencheu os dados de login corretamente.",
                            Status = StatusCodes.Status400BadRequest
                        });
                    }
                }
                else
                {
                    return BadRequest(new ErrorResponseDto()
                    {
                        Description = "O usuário não preencheu os dados de login corretamente.",
                        Status = StatusCodes.Status400BadRequest
                        
                    });
                }
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