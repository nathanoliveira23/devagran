using System.ComponentModel.DataAnnotations.Schema;

namespace Devagran.Models
{
    public class PublicationComments
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int PublicationId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; private set; }

        [ForeignKey("PublicationId")]
        public virtual Publication? Publication { get; private set; }
    }
}