using PFS.Domain.Models.Entities;

namespace PFS.Domain.Models.Filters
{
    public class CategoriesFilter : BaseFilter
    {
        public IEnumerable<int> ? painels { get; set; }
        public string? name { get; set; }
    }
}