using System.ComponentModel.DataAnnotations.Schema;

namespace Devagran.Models
{
    public class Follower
    {
        public int Id { get; set; }
        public int? FollowerId { get; set; }
        public int? FollowedId { get; set; }

        [ForeignKey("Follower_User_Id")]
        public virtual User FollowerUserId { get; private set; }

        [ForeignKey("Followed_User_Id")]
        public virtual User FollowedUserId { get; private set; }
    }
}