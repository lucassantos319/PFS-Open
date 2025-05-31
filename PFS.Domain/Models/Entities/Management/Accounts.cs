namespace PFS.Domain.Models.Entities.Management
{
    public class Accounts : BaseEntity
    {
        public int bank_id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string password { get; set; }
        public double current_amount { get; set; }
        public int painel_id { get; set; }
        public int user_created_id { get; set; }
        public int status_id { get; set; }
    }
}