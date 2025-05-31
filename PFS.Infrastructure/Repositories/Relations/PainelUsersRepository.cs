using PFS.Domain.Extensions;
using PFS.Domain.Models.Entities.Relations;
using PFS.Domain.Models.Filters;

namespace PFS.Infrastructure.Repositories;

public class PainelUsersRepository : BaseRepository<PainelUsers>
{
    public PainelUsersRepository(string connection) : base(connection)
    {
    }

    public override string[] _columns => new[]{"painel_id","user_id","role_id","status_id"};
    public override string _tableName => "painel_users";
    public override string _abvTable => "pu";
    public override string[] _columnsInnerJoin { get; }
    public override string[] _innerJoinTable => new[] { "users u on u.id = pu.user_id" };

    public IEnumerable<PainelUsers> Get(PainelUsersFilter filter)
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
    
    private string[] CreateQuery(PainelUsersFilter filter)
    {
        var whereList = new List<string>();
        if (filter != null)
        {
            if ( filter.painels != null && filter.painels.Count() > 0)
                whereList.Add($"{_abvTable}.painel_id in ({string.Join(",", filter.painels)})");
            
            if ( filter.users != null && filter.users.Count() > 0)
                whereList.Add($"{_abvTable}.user_id in ({string.Join(",", filter.users)})");
            
            if (filter.status != null && filter.status.Count() > 0)
                whereList.Add($"{_abvTable}.status_id in ({string.Join(",", filter.status)})");
            
            if ( filter.roles != null && filter.roles.Count() > 0)
                whereList.Add($"{_abvTable}.role_id in ({string.Join(",", filter.roles)})");

            if (filter.emails != null && filter.emails.Count() > 0)
            {
                var sqlEmail = string.Empty;
                foreach (var email in filter.emails)
                    sqlEmail += string.Join(",", "'" + email + "'");
            
                whereList.Add($"u.email in ({sqlEmail})");
            }

        }
        
        return whereList.ToArray();
    }
    
    public int Create(PainelUsers obj)
    {
        try
        {
            var id = Insert(obj);
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