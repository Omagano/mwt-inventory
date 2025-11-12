using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InventorySystem.Models
{
    public class DepartmentEntity : AuditableEntity
    {
       
        public int Id { get; set; }
        [Required]
        public  string Name { get; set; }
      
        //public virtual DepartmentEntity Department { get; set; }

    }
}
