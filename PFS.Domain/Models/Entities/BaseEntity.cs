using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PFS.Domain.Models.Entities;

public class BaseEntity
{
    [JsonIgnore]
    public DateTime created_at { get; set; } = DateTime.Now;
    [JsonIgnore]
    public DateTime updated_at { get; set; }  = DateTime.Now;
    public int id { get; set; }
}