namespace PFS.Domain.Models.DTOs;

public class MonthlyTransactionRecord
{
    public string Month { get; set; }
    public decimal incomes { get; set; }
    public decimal Expenses {get; set;}
}