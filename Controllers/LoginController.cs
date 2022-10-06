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
using Devagran.Repository;
using Devagran.Ultils;

namespace Devagran.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserRepository _userRepository;

        public LoginController(ILogger<LoginController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
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
                    User user = _userRepository.GetUserLogin(loginRequest.Email.ToLower(), MD5Ultils.MD5HashGenerator(loginRequest.Password));

                    if (user != null)
                    {
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
                            Description = "E-mail ou senha inválidos.",
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