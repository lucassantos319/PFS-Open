using PFS.Domain.Models.Entities.Management;
using PFS.Domain.Models.Filters;

namespace PFS.Infrastructure.Repositories
{
    public class BankRepository : BaseRepository<Banks>
    {
        public BankRepository(string connection) : base(connection)
        {
        }

        public override string[] _columns => new[] { "name" };

        public override string _tableName => "banks";
        public override string[] _innerJoinTable => new[] { "" };
        public override string _abvTable => "ba";
        public override string[] _columnsInnerJoin { get; }

        public IEnumerable<Banks> Get(BankFilter filter)
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

        private string[] CreateQuery(BankFilter filter)
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