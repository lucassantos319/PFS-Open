using PFS.Domain.Models.Entities;
using PFS.Domain.Models.Filters;

namespace PFS.Infrastructure.Repositories;

public class CreditCardRepository : BaseRepository<CreditCard>
{
    public CreditCardRepository(string connection) : base(connection)
    {
    }

    public override string[] _columns => new[]{"name","credit_card_number","safety_number","valid_date","status_id"};
    public override string _tableName => "credit_card";
    public override string[] _innerJoinTable => new[] { "" };
    public override string _abvTable => "cc";
    public override string[] _columnsInnerJoin { get; }

    public IEnumerable<CreditCard> Get(CreditCardFilter filter)
    {
        try
        {
            return Get(false, CreateQuery(filter));
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private string[] CreateQuery(CreditCardFilter filter)
    {
        var whereList = new List<string>();

        if (filter != null)
        {
            if (filter.ids != null && filter.ids.Any())
                whereList.Add($"{_abvTable}.id in {string.Join(",",filter.ids)}");
                
            if (filter.status != null && filter.status.Any())
                whereList.Add($"{_abvTable}.status in {string.Join(",", filter.status)}");
        }

        return whereList.ToArray();
    }
}