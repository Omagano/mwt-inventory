using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventorySystem.Models
{
    public class EquipmentTypes : AuditableEntity

    {
        public EquipmentTypes()
            {

            }
        public int Id { get; set; }

        [Required]
        [DisplayName("Type")]
        public required string Name { get; set; }

        

    }
}
