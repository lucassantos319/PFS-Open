using PFS.Domain.Extensions;
using PFS.Domain.Models.Entities.Management;
using PFS.Domain.Models.Filters;

namespace PFS.Infrastructure.Repositories;

public class PainelRepository : BaseRepository<Painel>
{
    public PainelRepository(string connection) : base(connection)
    {
    }

    public override string[] _columns => new[]
        { "name","current_amount","current_debit_amount","current_income_amount","db_connection","status_id"}; 
        
    public override string _tableName => "painel";
    public override string[] _innerJoinTable => new[] { "painel_users pu on pu.painel_id = pa.id" };
    public override string[] _columnsInnerJoin => new[] { "pu.role_id as role" };
    public override string _abvTable => "pa"; 

    public IEnumerable<Painel> Get(PainelFilter filter)
    {
        try
        {
            var result = Get(false, CreateQuery(filter));
             result.All(x =>
             {
                  x.role_user = x.role.ToString();
                  return true;
             });

             return result;

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private string[] CreateQuery(PainelFilter filter)
    {
        var whereList = new List<string>();
        if (filter != null)
        {
            if ( filter.ids != null && filter.ids.Count() > 0)
                whereList.Add($"{_abvTable}.id in ({string.Join(",",filter.ids)})");
            
            if ( filter.status != null && filter.status.Count() > 0)
                whereList.Add($"{_abvTable}.status in ({string.Join(",", filter.status)})");
        }
        
        return whereList.ToArray();
    }

    public int Create(Painel obj)
    {
        try
        {
            var query = $"insert into {_tableName} ({_columns.SelectQueryInsert()}) values ({_columns.ValuesQueryInsert()})";
            var id = int.Parse(ExecuteInsert(query,obj).ToString());

            if (id != 0)
                return id;
        }
        catch (Exception ex)
        {
            return 0;
        }
        
        return 0;
    }
}