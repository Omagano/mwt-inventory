using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Models
{
    public class StoreroomEntity : AuditableEntity
    {
        
        [Key]
        public int Id { get; set; }
        public StoreroomEntity() { }

        [ForeignKey("DirectorateEntity")]
        public int DirectoryID { get; set; }

        [ForeignKey("DivisionEntinty")]
        public int DivisionId { get; set; }

        [ForeignKey("OfficeEntity")]
        public int OfficeId { get; set; }


        public virtual required DirectorateEntity Directorate { get; set; }
        public virtual required DivisionEntity Division { get; set; }
        public virtual required OfficeEntity Office { get; set; }
    }
}
