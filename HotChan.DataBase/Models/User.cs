using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace HotChan.DataBase.Models
{
	public class User : IdentityUser<Guid>
	{
		public DateTimeOffset RegisterationDate { get; set; }
		public DateTimeOffset LastOnline { get; set; }
		public DateTimeOffset DOB { get; set; }

		[PersonalData]
		public Uri Avatar { get; set; }

		public ICollection<UserRole> UserRoles { get; set; }
		public ICollection<Post> Posts { get; set; }
		public ICollection<Post> Favorits { get; set; }

	}
}
