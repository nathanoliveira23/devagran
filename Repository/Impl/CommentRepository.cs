using Devagran.Context;
using Devagran.Models;

namespace Devagran.Repository.Impl
{
  public class CommentRepository : ICommentRepository
  {
    private readonly DevagranContext _context; 

    public CommentRepository(DevagranContext context)
    {
            _context = context;
    }

    public void Comment(PublicationComments comment)
    {
        _context.Comments.Add(comment);
        _context.SaveChanges();
    }
  }
}