using Common.Search;
using System;

namespace Common.Models
{
    public class TrackedUser 
    {
        public long UserId { get; set; }
        public Platform PlatForm { get; set; }
        public UserData Data { get; set; }
        public DateTime? LastUpdate { get; set; }

    }
}
