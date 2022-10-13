using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devagran.Models;

namespace Devagran.Repository
{
    public interface IFollowerRepository
    {
        public bool Follow(Follower follower);
        public bool Unfollow(Follower follower);
        public Follower GetFollower(int followerId, int followedId);
    }
}