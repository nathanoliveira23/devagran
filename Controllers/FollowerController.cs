using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devagran.Dtos;
using Devagran.Models;
using Devagran.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Devagran.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowerController : BaseController
    {
        private readonly ILogger<FollowerController> _logger;
        private readonly IFollowerRepository _followerRepository;

        public FollowerController(ILogger<FollowerController> logger, 
                                    IFollowerRepository followerRepository,
                                    IUserRepository userRepository) : base(userRepository)
        {
            _logger = logger;
            _followerRepository = followerRepository;
        }

        [HttpPut]
        public IActionResult Follow(int follwedId)
        {
            try
            {
                User userFollowed = _userRepository.GetUserById(follwedId);
                User userFollower = ReadToken();

                if (userFollowed != null)
                {
                    Follower follower = _followerRepository.GetFollower(userFollower.Id, userFollowed.Id);

                    if (follower != null)
                    {
                        _followerRepository.Unfollow(follower);
                    }
                    else
                    {
                        Follower newFollower = new Follower()
                        {
                            FollowedId = userFollowed.Id,
                            FollowerId = userFollower.Id,
                        };

                        _followerRepository.Follow(newFollower);
                    }
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