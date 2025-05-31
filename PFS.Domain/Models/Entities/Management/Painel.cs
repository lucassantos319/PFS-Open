using System.Text.Json.Serialization;
using PFS.Domain.Models.Enums;

namespace PFS.Domain.Models.Entities.Management
{
    public class Painel : BaseEntity
    {
        public string name {get; set;}
        public double current_amount { get; set; }
        public double current_debit_amount { get; set; }
        public double current_income_amount { get; set; }
        [JsonIgnore]
        public ERole role{get; set;}
        
        public string role_user {get; set;}
        
        [JsonIgnore] 
        public string db_connection { get; set; }
        public int status_id { get; set; }
    }
}

