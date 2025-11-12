using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Models
{
    public class SuppliersEntity : AuditableEntity
    {
        public SuppliersEntity()
        {
            
        }
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }

        [DisplayName("Contact Person")]
        public string ContactPerson { get; set; }

        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }
        
        public string Email { get; set; }
        public string Website { get; set; }
        [DisplayName("Street Address")]
        public string StreetAddress { get; set; }
        [DisplayName("City")]
        public string City { get; set; }
        public string Country { get; set; }

        [DisplayName("Postal Code")]
        //[RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid postal code format.")]
        public string PostalCode { get; set; }
    }
}
