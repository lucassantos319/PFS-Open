using PFS.Domain.Extensions;
using PFS.Domain.Models.Entities.Relations;
using PFS.Domain.Models.Filters;
using PFS.Domain.Models.RequestBody;

namespace PFS.Infrastructure.Repositories;

public class AccountCreditCardRepository : BaseRepository<AccountCreditCard>
{
    public AccountCreditCardRepository(string connection) : base(connection)
    {
    }

    public override string[] _columns => new[]{"account_id,credit_card_id,status_id"};
    public override string _tableName => "account_credit_card";
    public override string[] _innerJoinTable => new[] { "" };
    public override string _abvTable => "acc";
    public override string[] _columnsInnerJoin { get; }

    public IEnumerable<AccountCreditCard> Get(AccountCreditCardFilter filter)
    {
        try
        {
            return Get(false,filter.limit,CreateQuery(filter));
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    private string[] CreateQuery(AccountCreditCardFilter filter)
    {
        var whereList = new List<string>();

        if (filter != null)
        {
            if (filter.ids != null && filter.ids.Any())
                whereList.Add($"{_abvTable}.id in {string.Join(",",filter.ids)}");
            
            if (filter.status != null && filter.status.Any())
                whereList.Add($"{_abvTable}.id in {string.Join(",",filter.status)}");
            
            if (filter.accounts != null && filter.accounts.Any())
                whereList.Add($"{_abvTable}.account in {string.Join(",",filter.accounts)}");
            
            if (filter.creditCards != null && filter.creditCards.Any())
                whereList.Add($"{_abvTable}.credit_card in {string.Join(",", filter.creditCards)}");
        }

        return whereList.ToArray();
    }
    
    public ResponseResult<int> Create(AccountCreditCard obj)
    {
        var result= new ResponseResult<int>();
            
        var insertAccountCreditCard = InsertDB(obj);
        if (insertAccountCreditCard == 0)
        {
            result.GenerateErrorStatus("Erro ao adicionar relação conta e cartão de credito !");
            return result;
        }

        result.obj = new[]{insertAccountCreditCard};
        return result;
    }
}