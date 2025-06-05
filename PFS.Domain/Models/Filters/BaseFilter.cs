namespace PFS.Domain.Models.Filters;

public class BaseFilter
{
    public IEnumerable<int>? ids { get; set; }
    public IEnumerable<int>? status { get; set; }   
    public int limit { get; set; }
}