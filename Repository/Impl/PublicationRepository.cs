using Devagran.Context;
using Devagran.Models;

namespace Devagran.Repository.Impl
{
    public class PublicationRepository : IPublicationRepository
    {
        private readonly DevagranContext _context; 

        public PublicationRepository(DevagranContext context)
        {
            _context = context;
        }

        public void AddPublication(Publication publication)
        {
            _context.Publications.Add(publication);
            _context.SaveChanges();
        }
    }
}