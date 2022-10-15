using Devagran.Dtos;
using Devagran.Models;
using Devagran.Repository;
using Devagran.Services;
using Microsoft.AspNetCore.Mvc;

namespace Devagran.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublicationController : BaseController
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly ILogger<PublicationController> _logger;

        public PublicationController(IPublicationRepository publicationRepository, 
                                    ILogger<PublicationController> logger,
                                    IUserRepository userRepository) : base(userRepository)
        {
            _publicationRepository = publicationRepository;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AddPublication([FromForm] PublicationRequestDto publicationDto)
        {
            try
            {
                User user = ReadToken();
                CosmicService cosmicService = new CosmicService();

                if (publicationDto != null)
                {
                    if (String.IsNullOrEmpty(publicationDto.Description) && String.IsNullOrWhiteSpace(publicationDto.Description))
                    {
                         _logger.LogError("A descrição está inválida.");
                        return BadRequest("É obrigatório adicionar descrição na publicação.");
                    }

                    if (publicationDto.ImagePublication == null)
                    {
                        _logger.LogError("A imagem está inválida.");
                        return BadRequest("É obrigatório adicionar a imagem na publicação.");
                    }

                    Publication publication = new Publication()
                    {
                        UserId = user.Id,
                        Description = publicationDto.Description,
                        ImagePublication = cosmicService.ImageUpload(new ImageDto{ Image = publicationDto.ImagePublication, Title = "Publication" }),
                    };

                    _publicationRepository.AddPublication(publication);
                }
                    return Ok("Publicação salva com sucesso.");
            }
            catch (Exception ex)
            {
               _logger.LogError($"Ocorreu um erro ao realizar a publicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                {
                    Description = $"Ocorreu o seguinte erro: {ex.Message}",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}