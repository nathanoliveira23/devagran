using Devagran.Dtos;
using Devagran.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Devagran.Repository;

namespace Devagran.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        [HttpGet]
        public IActionResult GetUser()
        {
            try
            {
                User user = new User()
                {
                    Id = 1,
                    Name = "Nathan",
                    Email = "nathan@email.com",
                    Password = "1234"
                };

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao buscar o usuário");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                {
                    Description = $"Ocorreu o seguinte erro: {ex.Message}",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpPost]
        public IActionResult SaveUser([FromBody] User user)
        {
            try
            {
                if (user != null)
                {
                    List<string> errors = new List<string>();

                    if (String.IsNullOrEmpty(user.Name) || String.IsNullOrWhiteSpace(user.Name))
                    {
                        errors.Add("Nome inválido.");
                    }
                    if (String.IsNullOrEmpty(user.Email) || String.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains("@"))
                    {
                        errors.Add("E-mail inválido.");
                    }
                    if (String.IsNullOrEmpty(user.Password) || String.IsNullOrWhiteSpace(user.Password))
                    {
                        errors.Add("Senha inválida.");
                    }

                    if (errors.Count > 0) 
                        return BadRequest(new ErrorResponseDto()
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Errors = errors
                        });

                    _userRepository.Save(user);

                    return Ok(user);
                }
                else
                {
                    // código caso o usuário seja nulo
                    return BadRequest("Usuário inválido.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
               _logger.LogError("Ocorreu um erro ao salvar o usuário");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                {
                    Description = $"Ocorreu o seguinte erro: {ex.Message}",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}