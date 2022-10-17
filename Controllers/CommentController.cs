using Devagran.Dtos;
using Devagran.Models;
using Devagran.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Devagran.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : BaseController
    {
        private readonly ILogger<FollowerController> _logger;
        private readonly ICommentRepository _commentRepository;

        public CommentController(ILogger<FollowerController> logger, 
                                    ICommentRepository commentRepository,
                                    IUserRepository userRepository) : base(userRepository)
        {
            _logger = logger;
            _commentRepository = commentRepository;
        }
        

        [HttpPut]
        public IActionResult Comment(CommentDto commentDto)
        {
            try
            {
                if (commentDto != null)
                {
                    if (String.IsNullOrEmpty(commentDto.Description) || string.IsNullOrWhiteSpace(commentDto.Description))
                    {
                        _logger.LogError("O comentário recebido está vazio");
                        return BadRequest("Por favor, coloque seu comentário");
                    }

                    var comment = new PublicationComments();
                    comment.Description = commentDto.Description;
                    comment.PublicationId = commentDto.PublicationId;
                    comment.Id = ReadToken().Id;

                    _commentRepository.Comment(comment);
                }

                return Ok("Comentário salvo com sucesso");
            }
            catch (Exception ex)
            {
               _logger.LogError($"Ocorreu um erro ao commentar na publicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                {
                    Description = $"Ocorreu o seguinte erro: {ex.Message}",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}