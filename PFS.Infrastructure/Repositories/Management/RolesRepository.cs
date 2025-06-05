using PFS.Domain.Extensions;
using PFS.Domain.Models.Entities.Management;
using PFS.Domain.Models.Filters;
using PFS.Domain.Models.RequestBody;

namespace PFS.Infrastructure.Repositories.Management;

public class RolesRepository : BaseRepository<Roles>
{
    public RolesRepository(string connection) : base(connection)
    {
    }

    public override string[] _columns => new[]{"role"};
    public override string _tableName => "roles";
    public override string[] _innerJoinTable => new[] { "" };
    public override string _abvTable => "ro";
    public override string[] _columnsInnerJoin { get; }

    public IEnumerable<Roles> Get(RolesFilter filter)
    {
        try
        {
            return Get(false,filter.limit, CreateQuery(filter));
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private string[] CreateQuery(RolesFilter filter)
    {
        var whereList = new List<string>();

        if (filter != null)
        {
            if (filter.ids != null && filter.ids.Any())
                whereList.Add($"{_abvTable}.id in {string.Join(",", filter.ids)}");
            
            if (filter.status != null && filter.status.Any())
                whereList.Add($"{_abvTable}.status in {string.Join(",", filter.status)}");
        }
        
        return whereList.ToArray();
    }
    
    public ResponseResult<int> Create(Roles obj)
    {
        var result= new ResponseResult<int>();
            
        var insertRole = InsertDB(obj);
        if (insertRole == 0)
        {
            result.GenerateErrorStatus("Erro ao adicionar role !");
            return result;
        }

        result.obj = new[]{insertRole};
        return result;
    }
}