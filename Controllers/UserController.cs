using Devagran.Dtos;
using Devagran.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Devagran.Repository;
using Devagran.Ultils;

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

                    user.Password = MD5Ultils.MD5HashGenerator(user.Password);
                    user.Email = user.Email.ToLower();
                    
                    if (_userRepository.EmailVerify(user.Email))
                    {
                        _userRepository.Save(user);
                    }
                    else
                    {
                        return BadRequest(new ErrorResponseDto()
                        {
                            Description = "Usuário já cadastrado.",
                            Status = StatusCodes.Status400BadRequest
                        });
                    }

                    return Ok(user);
                }
                else
                {
                    // código caso o usuário seja nulo
                    return BadRequest(new ErrorResponseDto()
                    {
                        Description = "É necessário informar todos os campos.",
                        Status = StatusCodes.Status400BadRequest
                    });
                }
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