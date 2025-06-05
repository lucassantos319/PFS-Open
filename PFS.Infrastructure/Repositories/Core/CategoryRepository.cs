using PFS.Domain.Extensions;
using PFS.Domain.Models.Entities;
using PFS.Domain.Models.Filters;
using PFS.Domain.Models.RequestBody;

namespace PFS.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Categories>
    {
        public CategoryRepository(string connection) : base(connection)
        {
        }

        public override string[] _columns => new[] {"name","status_id"};

        public override string _tableName => "categories";
        public override string[] _innerJoinTable => new[] { "" };
        public override string _abvTable => "ca";
        public override string[] _columnsInnerJoin { get; }

        public IEnumerable<Categories> Get(CategoriesFilter filter)
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

        private string[] CreateQuery(CategoriesFilter filter)
        {
            var whereList = new List<string>();

            if (filter != null)
            {
                if (filter.ids != null && filter.ids.Any())
                    whereList.Add($"{_abvTable}.id in ({string.Join(",",filter.ids)})");
                
                if ( !string.IsNullOrEmpty(filter.name) )
                    whereList.Add($"{_abvTable}.name like '%{filter.name}%'");
                
                if (filter.status != null && filter.status.Any())
                    whereList.Add($"{_abvTable}.status_id in ({string.Join(",", filter.status)})");
                
                if (filter.painels != null && filter.painels.Any())
                    whereList.Add($"{_abvTable}.painel_id in ({string.Join(",", filter.painels)})");
            }

            return whereList.ToArray();
        }
        
        public ResponseResult<int> Create(Categories obj)
        {
            var result= new ResponseResult<int>();
            
            var insertCategory = InsertDB(obj);
            if (insertCategory == 0)
            {
                result.GenerateErrorStatus("Erro ao adicionar categoria !");
                return result;
            }

            result.obj = new[]{insertCategory};
            return result;
        }
    }
}