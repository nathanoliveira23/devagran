using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devagran.Context;
using Devagran.Models;

namespace Devagran.Repository.Impl
{
    public class FollowerRepository : IFollowerRepository
    {
        private readonly DevagranContext _context;

        public FollowerRepository(DevagranContext context)
        {
            _context = context;
        }

        public bool Follow(Follower follower)
        {
            try
            {
                _context.Followers.Add(follower);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Unfollow(Follower follower)
        {
            try
            {
                _context.Followers.Remove(follower);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Follower GetFollower(int followerId, int followedId)
        {
            return _context.Followers.FirstOrDefault(x => x.FollowedId == followedId && x.FollowerId == followerId);
        }
    }
}