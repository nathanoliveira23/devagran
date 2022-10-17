using Devagran.Models;
namespace Devagran.Repository
{
    public interface ICommentRepository
    {
        public void Comment(PublicationComments comment);
    }
}