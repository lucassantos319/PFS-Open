namespace PFS.Domain.Models.Filters;

public class AccountCreditCardFilter : BaseFilter
{
    public IEnumerable<int> accounts { get; set; }
    public IEnumerable<int> creditCards { get; set; }
}