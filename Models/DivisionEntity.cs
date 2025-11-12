using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace InventorySystem.Models
{
    public class DivisionEntity : AuditableEntity
    {

        public DivisionEntity()
        {
        }
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }

        [ForeignKey("DirectorateEntity")]
        public int DirectorateId { get; set; }

        [ValidateNever]
        public virtual required DirectorateEntity Directorate { get; set; }



    }
}
