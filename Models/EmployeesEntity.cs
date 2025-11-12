using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Models
{
    public class EmployeeEntity : AuditableEntity
    {

        // Employee Details
        public int Id { get; set; }
        [Required]
        [Display(Name = "Employee ID")]
        public string EmployeeIdNumber { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        public string? Address { get; set; }

        [Required]
        [Display(Name = "Office")]
        public int OfficeId { get; set; }

        // User Account Details
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }


        
        [ForeignKey("OfficeEntity")]
        public virtual required OfficeEntity Office { get; set; }


    }
}