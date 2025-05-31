using PFS.Domain.Models.Entities.Management;

namespace PFS.Domain.Models.Filters
{
    public class AccountFilter : BaseFilter
    {
        public IEnumerable<int> bank { get; set; }
        public IEnumerable<int> painels{ get; set; }
    }
}