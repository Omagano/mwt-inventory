using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using System;
using System.Linq;

namespace MvcMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcAppContext>>()))
            {/*
                // Cleanup RolesEntity records with invalid PrivilegeId
                var validPrivilegeIds = context.PrivilegeEntity.Select(p => p.Id).ToList();
                var invalidRoles = context.RolesEntity.Where(r => !validPrivilegeIds.Contains(r.PrivilegeId)).ToList();
                if (invalidRoles.Any())
                {
                    context.RolesEntity.RemoveRange(invalidRoles);
                    context.SaveChanges();
                }

                // Seed PrivilegeEntity if none exist
                if (!context.PrivilegeEntity.Any())
                {
                    context.PrivilegeEntity.AddRange(
                        new PrivilegeEntity { Privilege = "Admin" },
                        new PrivilegeEntity { Privilege = "User" },
                        new PrivilegeEntity { Privilege = "Guest" }
                    );
                    context.SaveChanges();
                }
            */}
        }
    }
}
