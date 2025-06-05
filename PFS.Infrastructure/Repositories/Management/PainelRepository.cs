using PFS.Domain.Extensions;
using PFS.Domain.Models.Entities.Management;
using PFS.Domain.Models.Filters;
using PFS.Domain.Models.RequestBody;

namespace PFS.Infrastructure.Repositories;

public class PainelRepository : BaseRepository<Painel>
{
    public PainelRepository(string connection) : base(connection)
    {
    }

    public override string[] _columns => new[]
        { "name","current_amount","current_debit_amount","current_income_amount","db_connection","status_id","percentual_month_comparation"}; 
        
    public override string _tableName => "painel";
    public override string[] _innerJoinTable => new[] { "painel_users pu on pu.painel_id = pa.id" };
    public override string[] _columnsInnerJoin => new[] { "pu.role_id as role" };
    public override string _abvTable => "pa"; 

    public IEnumerable<Painel> Get(PainelFilter filter)
    {
        try
        {
            var result = Get(false,filter.limit, CreateQuery(filter));
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

    public ResponseResult<int> Create(Painel obj)
    {
        var result= new ResponseResult<int>();
            
        var insertPainel = InsertDB(obj);
        if (insertPainel == 0)
        {
            result.GenerateErrorStatus("Erro ao adicionar painel !");
            return result;
        }

        result.obj = new[]{insertPainel};
        return result;
    }
}