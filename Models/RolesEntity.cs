using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventorySystem.Models
{
    public class RolesEntity : AuditableEntity
    {
        public RolesEntity()
        {
        }
        public int Id { get; set; }

        [Required]
        public string? RoleName { get; set; }

        [ForeignKey("PrivilegeEntity")]
        [Display(Name = "Privilege")]
        [Required(ErrorMessage = "Privilege is required.")]
        [ValidateNever]
        public int PrivilegeId { get; set; }
        public virtual required PrivilegeEntity Privilege { get; set; }
    }
}
