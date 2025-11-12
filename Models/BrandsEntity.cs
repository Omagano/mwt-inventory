using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventorySystem.Models
{
    public class BrandsEntity : AuditableEntity

    {
        public BrandsEntity()
        {

        }
        public int Id { get; set; }

        [Required]
        [DisplayName("Brand")]
        [StringLength(100, ErrorMessage = "Brand name cannot be longer than 100 characters.")]
        public required string Name { get; set; }



    }
}
