using PFS.Domain.Extensions;
using PFS.Domain.Models.Entities;
using PFS.Domain.Models.Filters;
using PFS.Domain.Models.RequestBody;

namespace PFS.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<Users>
    {
        public UserRepository(string connection) : base(connection)
        {
        }

        public override string[] _columns => new[]{ "first_name","last_name","email","password","access_token","refresh_token","status_id"};
        public override string _tableName => "users";
        public override string[] _innerJoinTable => new[] { "" };
        public override string _abvTable => "u";
        public override string[] _columnsInnerJoin {get;}

        public IEnumerable<Users> Get(UserFilter filter)
        {
            try
            {
                return Get(false, filter.limit,CreateQuery(filter));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string[] CreateQuery(UserFilter filter)
        {
            var whereList = new List<string>();

            if (filter != null)
            {
                if (filter.id != 0)
                    whereList.Add($"{_abvTable}.id = {filter.id}");

                if (filter.email != null)
                    whereList.Add($"{_abvTable}.email like '{filter.email}'");

                if (filter.password != null)
                    whereList.Add($"{_abvTable}.password like '{filter.password}'");

                if (filter.status != null && filter.status.Count() > 0)
                    whereList.Add($"{_abvTable}.status_id in ({string.Join(",", filter.status)})");
            }

            return whereList.ToArray();
        }
        
        public ResponseResult<int> Create(Users obj)
        {
            var result= new ResponseResult<int>();
            
            var insertUser = InsertDB(obj);
            if (insertUser == 0)
            {
                result.GenerateErrorStatus("Erro ao adicionar usuario !");
                return result;
            }

            result.obj = new[]{insertUser};
            return result;
        }
    }
}