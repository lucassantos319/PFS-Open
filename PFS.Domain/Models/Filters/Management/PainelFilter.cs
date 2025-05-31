using PFS.Domain.Models.Entities.Management;

namespace PFS.Domain.Models.Filters;

public class PainelFilter : BaseFilter
{
    public IEnumerable<int>? painels { get; set; }
    public IEnumerable<int>? users { get; set; }
    public IEnumerable<int>? roles { get; set; }
    public IEnumerable<string>? emails { get; set; }
}