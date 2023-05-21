using System;
using System.Collections.Generic;
using CIP.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CIP.API.Identity
{
    public partial class CIPIdentityDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<ApiUser>
    {
        public CIPIdentityDbContext()
        {
        }

        public CIPIdentityDbContext(DbContextOptions<CIPIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData
            (
                new IdentityRole()
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = "d0dec921-3991-4506-904e-598d1e049fa7"
                },
                 new IdentityRole()
                 {
                     Name = "Admin",
                     NormalizedName = "ADMIN",
                     Id = "15b2141c-bfdb-4720-b932-c23b95e46863"
                 }
            );

            var hasher = new PasswordHasher<CustomUser>();

            modelBuilder.Entity<ApiUser>().HasData
            (
                new ApiUser()
                {
                    Id = "7520b2f8-cd10-42ca-b8ea-cd3419257019",
                    UserName = "test-user",
                    NormalizedUserName = "TEST-USER",
                    Email = "user@test-user.com",
                    NormalizedEmail = "USER@TEST-USER.COM",
                    PasswordHash = hasher.HashPassword(default!, "@User#1")
                },
                 new ApiUser()
                 {
                     Id = "553677ef-05e0-4baa-9298-484bb047c73d",
                     UserName = "test-admin",
                     NormalizedUserName = "TEST-ADMIN",
                     Email = "admin@test-admin.com",
                     NormalizedEmail = "ADMIN@TEST-ADMIN.COM",
                     PasswordHash = hasher.HashPassword(default!, "@Admin#1")
                 }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData
                (
                    new IdentityUserRole<string>()
                    {
                        RoleId = "d0dec921-3991-4506-904e-598d1e049fa7",
                        UserId = "7520b2f8-cd10-42ca-b8ea-cd3419257019"
                    },
                    new IdentityUserRole<string>()
                    {
                        RoleId = "15b2141c-bfdb-4720-b932-c23b95e46863",
                        UserId = "553677ef-05e0-4baa-9298-484bb047c73d"
                    }

                );
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
