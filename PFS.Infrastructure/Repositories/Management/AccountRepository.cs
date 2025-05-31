using PFS.Domain.Models.Entities.Management;
using PFS.Domain.Models.Filters;

namespace PFS.Infrastructure.Repositories
{
    public class AccountRepository : BaseRepository<Accounts>
    {
        public AccountRepository(string connection) : base(connection)
        {
        }

        public override string[] _columns => new[] {"name","username","access_token","refresh_token","password","current_amount","bank_id","status_id","painel_id"};

        public override string _tableName => "accounts";
        public override string[] _innerJoinTable => new[] { "" };
        public override string _abvTable => "ac";
        public override string[] _columnsInnerJoin { get; }

        public IEnumerable<Accounts> Get(AccountFilter filter)
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

        private string[] CreateQuery(AccountFilter filter)
        {
            var whereList = new List<string>();

            if (filter != null)
            {
                if (filter.ids != null && filter.ids.Any())
                    whereList.Add($"{_abvTable}.id in ({string.Join(",", filter.ids)})");

                if (filter.bank != null && filter.bank.Count() > 0)
                    whereList.Add($"{_abvTable}.bank_id in ({string.Join(",", filter.bank)})");

                if (filter.status != null && filter.status.Count() > 0)
                    whereList.Add($"{_abvTable}.status_id in ({string.Join(",", filter.status)})");
                
                if (filter.painels != null && filter.painels.Count() > 0)
                    whereList.Add($"{_abvTable}.painel_id in ({string.Join(",", filter.status)})");
            }

            return whereList.ToArray();
        }
    }
}