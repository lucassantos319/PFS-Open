namespace PFS.Domain.Models.Entities
{
    public class Categories : BaseEntity
    {
        public string name { get; set; }
        public int status_id { get; set; }
        public string color { get; set; }
        public decimal total_amount { get; set; }
    }
}