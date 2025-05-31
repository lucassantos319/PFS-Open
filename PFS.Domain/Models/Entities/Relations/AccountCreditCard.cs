namespace PFS.Domain.Models.Entities.Relations;

public class AccountCreditCard : BaseEntity
{
    public int account_id { get; set; }
    public int credit_card_id { get; set; }
    public int status_id { get; set; }
}