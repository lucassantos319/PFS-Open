using System.ComponentModel.DataAnnotations;

namespace PFS.Domain.Models.Entities.Relations;

public class PainelUsers : BaseEntity
{
    [Required]
    public int painel_id { get; set; }
    public int user_id { get; set; }
    public int role_id { get; set; }
    
    [Required]
    public int status_id { get; set; }
}