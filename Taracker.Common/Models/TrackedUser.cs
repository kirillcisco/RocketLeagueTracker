using Common.Search;
using System;

namespace Common.Models
{
    public class TrackedUser  : IComparable
    {
        public long UserId { get; set; }
        public Platform PlatForm { get; set; }
        public UserData Data { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int? SortOrder { get; set; }

        public int CompareTo(object obj)
        {
            TrackedUser a = this;
            TrackedUser b = (TrackedUser)obj;

            if (a.SortOrder == b.SortOrder)
                return this.UserId.CompareTo(b.UserId);

            return a.SortOrder.Value.CompareTo(b.SortOrder.Value);
        }

        public override string ToString()
        {
            return $"({this.Data?.PlatformInfo?.PlatformUserHandle} {this.UserId} {this.SortOrder})";
        }
    }
}
