using HotChan.DataBase.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotChanApi.Data
{
	public class DataContext : IdentityDbContext<User, Role, Guid,
		IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
		IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
	{
		public DataContext(DbContextOptions<DataContext> options)
		: base(options)
		{
		}

		public DbSet<Post>	Posts	{ get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			// this must be set in case of inheriting from IdentityDbContext 
			base.OnModelCreating(builder);

			builder.Entity<UserRole>(userRole =>
			{
				userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

				userRole.HasOne(ur => ur.Role)
				.WithMany(r => r.UserRoles)
				.HasForeignKey(ur => ur.RoleId)
				.IsRequired();

				userRole.HasOne(ur => ur.User)
				.WithMany(r => r.UserRoles)
				.HasForeignKey(ur => ur.UserId)
				.IsRequired();
			});
		}
	}
}
