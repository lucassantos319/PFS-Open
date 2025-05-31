namespace PFS.Domain.Models.Entities
{
    public class Users : BaseEntity
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string password { get; set; }
        public int status_id { get; set; }
    }
}