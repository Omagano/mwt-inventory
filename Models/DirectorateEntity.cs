using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventorySystem.Models
{
    public class DirectorateEntity : AuditableEntity
    {
        public DirectorateEntity()
        {
        }

        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }


        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [ValidateNever]
        public virtual DepartmentEntity Department { get; set; }

        
    }
}
