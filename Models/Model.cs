    using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventorySystem.Models
{
    public class Model : AuditableEntity

    {
        public Model()
        {

        }
        public int Id { get; set; }

        [Required]
        [DisplayName("Model")]
        [StringLength(100, ErrorMessage = "Model name cannot be longer than 100 characters.")]
        public required string Name { get; set; }



    }
}
