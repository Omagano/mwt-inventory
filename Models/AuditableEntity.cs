using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Models
{
    public abstract class AuditableEntity
    {
        //private const string  = "System";

        //remove the edit and upade time and by and add them to the controller.
        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        [Display(Name = "Created By")]
        public String? CreatedBy { get; set; } 
        [Display(Name = "Date Updated")] 
        public DateTime? DateUpdated { get; set; } 
        [Display(Name = "Updtaes By")]
        public String? UpdatedBy { get; set; } 
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public String? DeletedBy { get; set; } 
    }

}
