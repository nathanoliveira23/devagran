using Devagran.Dtos;
using Devagran.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Devagran.Repository;
using Devagran.Ultils;
using Devagran.Services;

namespace Devagran.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        public UserController(ILogger<UserController> logger, IUserRepository userRepository) : base(userRepository)
        {
            _logger = logger;
        }
        private readonly ILogger<UserController> _logger;

        [HttpGet]
        public IActionResult GetUser()
        {
            try
            {
                User user = ReadToken();

                return Ok(new UserResponseDto
                {
                    Name = user.Name,
                    Email = user.Email
                });
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
        [AllowAnonymous]
        public IActionResult SaveUser([FromForm] UserRequestDto userRequestDto)
        {
            try
            {
                if (userRequestDto != null)
                {
                    List<string> errors = new List<string>();

                    if (String.IsNullOrEmpty(userRequestDto.Name) || String.IsNullOrWhiteSpace(userRequestDto.Name))
                    {
                        errors.Add("Nome inválido.");
                    }
                    if (String.IsNullOrEmpty(userRequestDto.Email) || String.IsNullOrWhiteSpace(userRequestDto.Email) || !userRequestDto.Email.Contains("@"))
                    {
                        errors.Add("E-mail inválido.");
                    }
                    if (String.IsNullOrEmpty(userRequestDto.Password) || String.IsNullOrWhiteSpace(userRequestDto.Password))
                    {
                        errors.Add("Senha inválida.");
                    }

                    if (errors.Count > 0) 
                        return BadRequest(new ErrorResponseDto()
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Errors = errors
                        });

                    CosmicService cosmicService = new CosmicService();

                    User user = new User()
                    {
                        Name = userRequestDto.Name,
                        Email = userRequestDto.Email,
                        Password = userRequestDto.Password,
                        ImageProfile = cosmicService.ImageUpload(new ImageDto { Image = userRequestDto.ImageProfile, Title = userRequestDto.Name.Replace(" ", "") }),
                    };


                    user.Password = MD5Ultils.MD5HashGenerator(user.Password);
                    user.Email = user.Email.ToLower();
                    
                    if (!_userRepository.EmailVerify(user.Email))
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

                    return Ok("Usuário salvo com sucesso.");
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