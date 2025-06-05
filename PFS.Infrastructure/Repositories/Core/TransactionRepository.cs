using PFS.Domain.Extensions;
using PFS.Domain.Models.DTOs;
using PFS.Domain.Models.Entities;
using PFS.Domain.Models.Enums;
using PFS.Domain.Models.Filters;
using PFS.Domain.Models.RequestBody;

namespace PFS.Infrastructure.Repositories;

public class TransactionRepository : BaseRepository<Transaction>
{
    public TransactionRepository(string connection) : base(connection)
    {
    }

    public override string[] _columns => new[]{ "description","amount","categories_id","account_id","painel_id","user_id","type_of","importance_id","status_id"};
    public override string _tableName => "transaction";
    public override string[] _innerJoinTable => new[] { "categories c on c.id = t.categories_id" };
    public override string _abvTable => "t";
    public override string[] _columnsInnerJoin => new[] { "c.name as category_name" } ;

    public IEnumerable<Transaction> Get(TransactionFilter filter)
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

    public IEnumerable<MonthlyTransactionRecord> GetMonthlyTransactionRecords(int painelId)
    {
        try
        {
            var query = $"SELECT EXTRACT(MONTH FROM t.created_at) AS month," +
                            $"SUM(CASE WHEN t.amount > 0 THEN t.amount ELSE 0 END) AS incomes," +
                            $"SUM(CASE WHEN t.amount < 0 THEN t.amount * -1 ELSE 0 END) AS expenses" +
                            $" FROM transaction t WHERE t.painel_id = {painelId} AND t.status_id = {(int)EStatus.Ativo} GROUP BY EXTRACT(MONTH FROM t.created_at);";
            
            return ExecuteReturn<MonthlyTransactionRecord>(query);
        } 
        catch (Exception ex)
        {
            return null;
        }
    }

    private string[] CreateQuery(TransactionFilter filter)
    {
        var whereList = new List<string>();

        if (filter != null)
        {
            if (filter.ids != null && filter.ids.Any())
                whereList.Add($"{_abvTable}.id in {string.Join(",",filter.ids)}");
                
            if (filter.status != null && filter.status.Any())
                whereList.Add($"{_abvTable}.status_id in ({string.Join(",", filter.status)})");
            
            if (filter.users != null && filter.users.Any())
                whereList.Add($"{_abvTable}.user_id in ({string.Join(",", filter.users)})");
            
            if (filter.painels != null && filter.painels.Any())
                whereList.Add($"{_abvTable}.painel_id in ({string.Join(",", filter.painels)})");
            
            if (filter.account != null && filter.account.Any())
                whereList.Add($"{_abvTable}.account_id in ({string.Join(",", filter.account)})");
            
            if (filter.categories != null && filter.categories.Any())
                whereList.Add($"{_abvTable}.categories_id in ({string.Join(",", filter.categories)})");
            
            if (filter.importances != null && filter.importances.Any())
                whereList.Add($"{_abvTable}.importance_id in ({string.Join(",", filter.importances)})");
        }

        return whereList.ToArray();
    }
    
    
    public ResponseResult<int> Create(Transaction obj)
    {
        var result= new ResponseResult<int>();
            
        var insertTransaction = InsertDB(obj);
        if (insertTransaction == 0)
        {
            result.GenerateErrorStatus("Erro ao adicionar transação !");
            return result;
        }

        result.obj = new[]{insertTransaction};
        return result;
    }
}