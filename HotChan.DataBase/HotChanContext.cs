using HotChan.DataBase.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChan.DataBase
{
	public class HotChanContext : IdentityDbContext<User, Role, Guid,
		IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
		IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
	{
		public HotChanContext(DbContextOptions<HotChanContext> options): base(options)
		{	
		}

		public DbSet<Post>	Posts	{ get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			// this must be set in case of inheriting from IdentityDbContext 
			base.OnModelCreating(builder);

			// User Roles
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

			// One to Many user -> posts. ef conventions were not working for some reason
			builder.Entity<Post>()
			.HasOne(p => p.User)
			.WithMany(b => b.Posts)
			.HasForeignKey(fk => fk.Id);


		}
	}
}
