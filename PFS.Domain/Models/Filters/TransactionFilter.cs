using System.Collections;

namespace PFS.Domain.Models.Filters;

public class TransactionFilter : BaseFilter
{
    public IEnumerable<int>? categories { get; set; }
    public IEnumerable<int>? account { get; set; }
    public IEnumerable<int>? painels { get; set; }
    public IEnumerable<int>? users { get; set; }
    public IEnumerable<int>? importances { get; set; }
}