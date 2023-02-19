using HotChan.DataBase.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotChan.DataBase;

public class HotChanContext : IdentityDbContext<User, Role, Guid,
    IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
	IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
	public HotChanContext(DbContextOptions<HotChanContext> options): base(options)
	{	
	}

	public virtual DbSet<Post> Posts => Set<Post>();
	public virtual DbSet<Comment> Comments => Set<Comment>();
	public virtual DbSet<Bookmarks> Bookmarks => Set<Bookmarks>();
	public virtual DbSet<PostScore> PostScores => Set<PostScore>();
	//public virtual DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

	protected override void OnModelCreating(ModelBuilder builder)
	{
		// this must be set in case of inheriting from IdentityDbContext 
		base.OnModelCreating(builder);

		builder.Entity<User>()
			.HasMany(ur => ur.UserRoles)
			.WithOne(ur => ur.User)
			.HasForeignKey(ur => ur.UserId)
			.IsRequired();
		
		builder.Entity<Role>()
			.HasMany(ur => ur.UserRoles)
			.WithOne(ur => ur.Role)
			.HasForeignKey(ur => ur.RoleId)
			.IsRequired();
	}
}

