using System.ComponentModel.DataAnnotations.Schema;

namespace Devagran.Models
{
    public class Publication
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ImagePublication { get; set; }
        public int UserId { get; set; }

        [ForeignKey("user_id")]
        public virtual User User { get; set; }
    }
}