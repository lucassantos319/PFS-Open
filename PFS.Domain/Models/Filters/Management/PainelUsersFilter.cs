using PFS.Domain.Models.Entities.Relations;

namespace PFS.Domain.Models.Filters;

public class PainelUsersFilter : BaseFilter
{
    public IEnumerable<int>? painels { get; set; }
    public IEnumerable<int>? users { get; set; }
    public IEnumerable<int>? roles { get; set; }
    public IEnumerable<string>? emails { get; set; }
}