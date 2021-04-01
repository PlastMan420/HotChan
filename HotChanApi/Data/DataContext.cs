using HotChanApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChanApi.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options)
		: base(options)
		{
		}
		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlServer();

		public DbSet<Post>	Posts	{ get; set; }
		public DbSet<Reply> Replies { get; set; }
		public DbSet<User>	UserDb	{ get; set; }

		//#region Required
		//protected override void OnModelCreating(ModelBuilder modelBuilder)
		//{
		//	modelBuilder.Entity<User>()
		//		.Property(b => b.PostIds)
		//		.IsRequired();
		//}
		//#endregion
	}
}
