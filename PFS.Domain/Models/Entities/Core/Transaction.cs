using System.Text.Json.Serialization;

namespace PFS.Domain.Models.Entities;

public class Transaction : BaseEntity
{
    public string description { get; set; }
    public double amount { get; set; }
    [JsonIgnore]
    public int categories_id { get; set; }
    public string category_name { get; set; }
    public int account_id { get; set; }
    public int painel_id { get; set; }
    public int user_id { get; set; }
    public int status_id { get; set;}
}