namespace PFS.Domain.Models.Entities;

public class CreditCard:BaseEntity
{
    public string name { get; set; }
    public string credit_card_number { get; set; }
    public string safety_number { get; set; }
    public DateTime valid_date { get; set; }
    public int status_id { get; set; }
}