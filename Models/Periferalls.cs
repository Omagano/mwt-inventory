using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Models
{
    public class Periferrals : AuditableEntity
    {
       
        public int Id { get; set; }
        [Required]
        public string? Item { get; set; } 
    }
}
