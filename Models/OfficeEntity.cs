using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventorySystem.Models
{
    public class OfficeEntity : AuditableEntity

    {
        public OfficeEntity()
        {

        }
        public int Id { get; set; }
        [Required]
        [DisplayName("Office Name")]
        public required string Name { get; set; }

        

    }
}
