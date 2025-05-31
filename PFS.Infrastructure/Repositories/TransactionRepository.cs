using PFS.Domain.Models.Entities;
using PFS.Domain.Models.Filters;

namespace PFS.Infrastructure.Repositories;

public class TransactionRepository : BaseRepository<Transaction>
{
    public TransactionRepository(string connection) : base(connection)
    {
    }

    public override string[] _columns => new[]{ "description","amount","categories_id","account_id","painel_id","user_id","type_of","importance_id","status_id"};
    public override string _tableName => "transactions";
    public override string[] _innerJoinTable => new[] { "" };
    public override string _abvTable => "t";
    public override string[] _columnsInnerJoin { get; }

    public IEnumerable<Transaction> Get(TransactionFilter filter)
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

    private string[] CreateQuery(TransactionFilter filter)
    {
        var whereList = new List<string>();

        if (filter != null)
        {
            if (filter.ids != null && filter.ids.Any())
                whereList.Add($"{_abvTable}.id in {string.Join(",",filter.ids)}");
                
            if (filter.status != null && filter.status.Any())
                whereList.Add($"{_abvTable}.status in {string.Join(",", filter.status)}");
            
            if (filter.users != null && filter.users.Any())
                whereList.Add($"{_abvTable}.user in {string.Join(",", filter.users)}");
            
            if (filter.painels != null && filter.painels.Any())
                whereList.Add($"{_abvTable}.painel in {string.Join(",", filter.painels)}");
            
            if (filter.account != null && filter.account.Any())
                whereList.Add($"{_abvTable}.account in {string.Join(",", filter.account)}");
            
            if (filter.categories != null && filter.categories.Any())
                whereList.Add($"{_abvTable}.categories in {string.Join(",", filter.categories)}");
            
            if (filter.importances != null && filter.importances.Any())
                whereList.Add($"{_abvTable}.importances in {string.Join(",", filter.importances)}");
        }

        return whereList.ToArray();
    }
}