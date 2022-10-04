using Devagran.Dtos;
using Devagran.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Devagran.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        private readonly ILogger<UserController> _logger;

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
    }
}