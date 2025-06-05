using PFS.Domain.Models.Entities;

namespace PFS.Domain.Models.DTOs;

public class DashboardInfos
{
    public double balance { get; set; }
    public double percentageLastMonth { get; set; }
    public bool isPositive { get; set; }
    public IEnumerable<Transaction> transactions { get; set; }
    public IEnumerable<Categories> categories { get; set; }
    public IEnumerable<MonthlyTransactionRecord> monthlyTransactionRecords { get; set; }
            
}