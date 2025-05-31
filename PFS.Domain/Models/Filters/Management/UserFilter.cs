using PFS.Domain.Models.Entities;

namespace PFS.Domain.Models.Filters
{
    public class UserFilter : Users
    {
        public IEnumerable<int> status { get; set; }
    }
}