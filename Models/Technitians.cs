using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventorySystem.Models
{
    public class Technitian : AuditableEntity

    {
        public Technitian()
        {

        }
        public int Id { get; set; }

        [Required]
        [DisplayName("Technitian First & Last Name")]
        [StringLength(100, ErrorMessage = "Employee names cannot be longer than 100 characters.")]
        public required string Name { get; set; }


         [Required]
        [DisplayName("Employee Code")]
        [StringLength(100, ErrorMessage = "Enter Code Correctly.")]
        public required string EmployeeCode { get; set; }

    }
}
