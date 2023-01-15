using HotChan.DataBase.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotChan.DataBase;

public class HotChanContext : IdentityDbContext<User, IdentityRole<Guid>, Guid,
    IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
	IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
	public HotChanContext(DbContextOptions<HotChanContext> options): base(options)
	{	
	}

	public virtual DbSet<Post> Posts => Set<Post>();
	public virtual DbSet<Comment> Comments => Set<Comment>();
	public virtual DbSet<bookmarks> Bookmarks => Set<bookmarks>();
	//public virtual DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

	protected override void OnModelCreating(ModelBuilder builder)
	{
		// this must be set in case of inheriting from IdentityDbContext 
		base.OnModelCreating(builder);
	}
}

