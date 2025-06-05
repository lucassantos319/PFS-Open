using PFS.Domain.Extensions;
using PFS.Domain.Interfaces;
using PFS.Domain.Models.DTOs;
using PFS.Domain.Models.Entities;
using PFS.Domain.Models.Enums;
using PFS.Domain.Models.Filters;
using PFS.Domain.Models.RequestBody;
using PFS.Infrastructure.Repositories;

namespace PFS.Application.Applications.Core;

public class TransactionApplication(string connectionString) : IApplication<Transaction>
{
    private readonly TransactionRepository _repository = new(connectionString);

    public ResponseResult<int> Create(Transaction obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        obj.status_id = (int)EStatus.Ativo;
        var creation = _repository.Create(obj);
        return creation;
    }

    public void Update(Transaction obj)
    {
        throw new NotImplementedException();
    }

    public void DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public ResponseResult<Transaction> Get(TransactionFilter filter)
    {
        var responseResult = new ResponseResult<Transaction>();
            
        var transactions = _repository.Get(filter);
        var responseResultObj = transactions as Transaction[] ?? transactions.ToArray();
        if (!responseResultObj.Any())
        {
            responseResult.GenerateErrorStatus();
            return responseResult;
        }

        responseResult.obj = responseResultObj;
        return responseResult;
    }

    public ResponseResult<MonthlyTransactionRecord>  GetMonthlyDataRecord(int painelId)
    {
        var responseResult = new ResponseResult<MonthlyTransactionRecord>();
        var monthlyRecords = _repository.GetMonthlyTransactionRecords(painelId);

        var monthlyTransactionRecords = monthlyRecords as MonthlyTransactionRecord[] ?? monthlyRecords.ToArray();
        if (!monthlyTransactionRecords.Any())
        {
            responseResult.GenerateErrorStatus("Monthly data records not found");
            return responseResult;
        }

        responseResult.obj = monthlyTransactionRecords;
        return responseResult;
    }
}