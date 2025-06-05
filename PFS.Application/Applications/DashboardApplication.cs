using Newtonsoft.Json;
using PFS.Application.Applications.Core;
using PFS.Application.Applications.Management;
using PFS.Domain.Extensions;
using PFS.Domain.Models.DTOs;
using PFS.Domain.Models.Entities.Management;
using PFS.Domain.Models.Enums;
using PFS.Domain.Models.Errors;
using PFS.Domain.Models.RequestBody;

namespace PFS.Applications;

public class DashboardApplication 
{
     private readonly string _connectionStringMG;
     private readonly string _connectionStringCore;
     private readonly PainelApplication _painelApplication;
     private readonly TransactionApplication _transactionApplication;
     private readonly CategoryApplication _categoryApplication;
     
    public DashboardApplication(string connectionStringMG, string connectionStringCore)
    {
        _connectionStringMG = connectionStringMG;
        _connectionStringCore = connectionStringCore;

        _painelApplication = new(_connectionStringMG);
        _transactionApplication = new(_connectionStringCore);
        _categoryApplication = new(_connectionStringCore);
    }

    private void GetPainelInfos(int painelId, ResponseResult<DashboardInfos> dashboardInfos)
    {
        var painelInfoResult = _painelApplication.Get(new()
        {
            painels = new int[]{painelId},
            status = new int[]{(int)EStatus.Ativo}
        });

        if (!painelInfoResult.success)
        {
            var response = JsonConvert.SerializeObject(new ErrorType
            {
                status = 400,
                title = "Erro na buscado painel",
                detail = string.Join(",", painelInfoResult.errors),
                instance = $"dashboard/info/{painelId}"
            });
        
            dashboardInfos.GenerateErrorStatus(response);
            return;
        }
        
        SetDashboardPainelInfos(painelInfoResult,dashboardInfos);
    }

    private void SetDashboardPainelInfos(ResponseResult<Painel> painelInfoResult, ResponseResult<DashboardInfos> dashboardInfos)
    {
        var painelInfo = painelInfoResult.obj.FirstOrDefault();
        var dashboardInfo = dashboardInfos.obj.FirstOrDefault();
        
        if ( painelInfo != null && dashboardInfo != null )
        {
            dashboardInfo.balance = painelInfo.current_amount;
            dashboardInfo.percentageLastMonth = painelInfo.percentual_month_comparation;
            dashboardInfo.isPositive = painelInfo.current_amount > 0;
        }
    }

    public ResponseResult<DashboardInfos> GetDashboardInfo(int painelId)
    {
        var dashboardInfos = new ResponseResult<DashboardInfos>()
        {
            obj = new []
            {
                new DashboardInfos()
            }
        };
        
        GetPainelInfos(painelId,dashboardInfos);
        if ( !dashboardInfos.success)
            return dashboardInfos;

        GetTransactionsInfos(painelId, dashboardInfos);
        if (!dashboardInfos.success)
            return dashboardInfos;

        GetMainCategories(painelId, dashboardInfos);
        if (!dashboardInfos.success)
            return dashboardInfos;
        
        GetMonthlyRecords(painelId,dashboardInfos);
        return dashboardInfos;
    }

    private void GetMonthlyRecords(int painelId, ResponseResult<DashboardInfos> dashboardInfos)
    {
        var dashboardInfo = dashboardInfos.obj.FirstOrDefault();
        var monthlyDataInfo = _transactionApplication.GetMonthlyDataRecord(painelId);
        
        if (!monthlyDataInfo.success)
        {
            var response = JsonConvert.SerializeObject(new ErrorType
            {
                status = 400,
                title = "Erro na buscado painel",
                detail = string.Join(",", monthlyDataInfo.errors),
                instance = $"dashboard/info/{painelId}"
            });
        
            dashboardInfos.GenerateErrorStatus(response);
            return;
        }
        
        dashboardInfo.monthlyTransactionRecords = monthlyDataInfo.obj;
    }

    private void GetMainCategories(int painelId, ResponseResult<DashboardInfos> dashboardInfos)
    {
        var categoriesInfoResult = _categoryApplication.Get(new()
        {
            painels = new []{painelId},
            status = new []{(int)EStatus.Ativo}
        });

        if (!categoriesInfoResult.success)
        {
            var response = JsonConvert.SerializeObject(new ErrorType
            {
                status = 400,
                title = "Erro na buscado painel",
                detail = string.Join(",", categoriesInfoResult.errors),
                instance = $"dashboard/info/{painelId}"
            });
        
            dashboardInfos.GenerateErrorStatus(response);
            return;
        }

        var dashboardInfo = dashboardInfos.obj.FirstOrDefault();
        dashboardInfo.categories = categoriesInfoResult.obj;
    }

    private void GetTransactionsInfos(int painelId, ResponseResult<DashboardInfos> dashboardInfos)
    {
        var dashboardInfo = dashboardInfos.obj.FirstOrDefault();
        var transactionsResult = _transactionApplication.Get(new()
        {
            painels = new int[]{painelId},
            limit = 5
        });
        
        if ( !transactionsResult.success)
        {
            var response = JsonConvert.SerializeObject(new ErrorType
            {
                status = 400,
                title = "Erro na buscado painel",
                detail = string.Join(",", transactionsResult.errors),
                instance = $"dashboard/info/{painelId}"
            });
    
            dashboardInfos.GenerateErrorStatus(response);
            return;
        }

        dashboardInfo.transactions = transactionsResult.obj;

    }
}