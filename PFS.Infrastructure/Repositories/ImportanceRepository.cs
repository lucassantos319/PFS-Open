using PFS.Domain.Models.Entities;
using PFS.Domain.Models.Filters;

namespace PFS.Infrastructure.Repositories
{
    public class ImportanceRepository : BaseRepository<Importance>
    {
        public ImportanceRepository(string connection) : base(connection)
        {
        }

        public override string[] _columns => new[] {"name"};

        public override string _tableName => "importance";
        public override string[] _innerJoinTable { get; }
        public override string _abvTable => "i";
        public override string[] _columnsInnerJoin { get; }

        public IEnumerable<Importance> Get(ImportanceFilter filter)
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

        private string[] CreateQuery(ImportanceFilter filter)
        {
            var whereList = new List<string>();

            if (filter != null)
            {
                if (filter.ids != null && filter.ids.Any())
                    whereList.Add($"{_abvTable}.id in ({string.Join(",",filter.ids)})");
                
                if (filter.status != null && filter.status.Any())
                    whereList.Add($"{_abvTable}.status in ({string.Join(",", filter.status)})");
            }

            return whereList.ToArray();
        }
    }
}