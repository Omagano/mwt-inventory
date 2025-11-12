using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventorySystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MvcMovie.Data
{
    public class MvcAppContext : IdentityDbContext<IdentityUser> 
    {
        public MvcAppContext(DbContextOptions<MvcAppContext> options)
            : base(options)
        {
            //this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<InventorySystem.Models.RolesEntity> RolesEntity { get; set; } = default!;
        public DbSet<InventorySystem.Models.SuppliersEntity> SuppliersEntity { get; set; } = default!;
        public DbSet<InventorySystem.Models.DivisionEntity> DivisionEntity { get; set; } = default!;
        public DbSet<InventorySystem.Models.DirectorateEntity> DirectorateEntity { get; set; } = default!;
        public DbSet<InventorySystem.Models.EmployeeEntity> EmployeeEntity { get; set; } = default!;
        //public DbSet<InventorySystem.Models.EquipmentsEntity> EquipmentsEntity { get; set; } = default!;
        public DbSet<InventorySystem.Models.OfficeEntity> OfficeEntity { get; set; } = default!;
        public DbSet<InventorySystem.Models.StoreroomEntity> StoreroomEntity { get; set; } = default!;
        public DbSet<InventorySystem.Models.PrivilegeEntity> PrivilegeEntity { get; set; } = default!;
        public DbSet<InventorySystem.Models.DepartmentEntity> DepartmentEntity { get; set; } = default!;
        public DbSet<InventorySystem.Models.EquipmentTypes> EquipmentTypes { get; set; } = default!;
        public DbSet<InventorySystem.Models.BrandsEntity> BrandsEntity { get; set; } = default!;
        public DbSet<InventorySystem.Models.Model> Model { get; set; } = default!;
        public DbSet<InventorySystem.Models.EquipmentsEntity> EquipmentsEntity { get; set; } = default!;

        
    }
}
