using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Models
{

    //[Index(nameof(Name), nameof(OrbitalPeriod), IsUnique = true)]
    public class EquipmentsEntity : AuditableEntity
    {
        public EquipmentsEntity()
        {
        }
        public int Id { get; set; }

        //[Index(IsUnique = true, Name = "IX_Equipment_SerialNumber")]
        [Required]
        [DisplayName("Serial Number")]

        public required string SerialNumber { get; set; }

        [Required]
        [DisplayName("Status")]
        public required string Status { get; set; }


        [Required]
        [Range(0.01, 99999999.99)]
        [DisplayName("Vendor Price")]
        public decimal VendorPrice { get; set; }

        [Range(0.01, 99999999.99)]
        public decimal? Value { get; set; }

        [DisplayName("Arrival Date")]
        [DataType(DataType.Date)]
        public DateTime? ArrivalDate { get; set; }

         [DisplayName("Warranty From")]
        [DataType(DataType.Date)]
        public DateTime? Warantyfrom { get; set; }

        [DisplayName("Warranty Validity")]
        [DataType(DataType.Date)]
        public DateTime? WarantyEnd { get; set; }

        [Required]
        [DisplayName("Received By")]
        public required string ReceiverName { get; set; }


        [ForeignKey("SupplierEntity")]
        public int SuppliersId { get; set; }
        [ValidateNever]
        public virtual SuppliersEntity SupplierEntity { get; set; }

        [ForeignKey("EquipmentTypes")]
        [DisplayName("Equipment Type")]
        [Required(ErrorMessage = "Equipment Type is required.")]
        public int TypeId { get; set; }

        [ForeignKey("Model")]
        [DisplayName("Model")]
        [Required(ErrorMessage = "Model is required.")]
        public int ModelId { get; set; }

        [ForeignKey("BrandsEntity")]
        [DisplayName("Brand")]
        [Required(ErrorMessage = "Brand is required.")]



        public int BrandId { get; set; }

        //[ValidateNever]
        //public virtual SuppliersEntity Supplier { get; set; }

        [ValidateNever]
        public virtual required EquipmentTypes EquipmentTypes { get; set; }
        [ValidateNever]
        public virtual required BrandsEntity BrandsEntity { get; set; }
        [ValidateNever]
        public virtual required Model Model { get; set; }


    }
}
