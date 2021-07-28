using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HotChan.DataBase.Models
{
	public class User : IdentityUser<Guid>
	{
		[PersonalData]
		public DateTimeOffset RegisterationDate { get; set; }
		public DateTimeOffset LastOnline { get; set; }
		public DateTimeOffset DOB { get; set; }

		public Uri Avatar { get; set; }

		public ICollection<UserRole> UserRoles { get; set; }
		public ICollection<Post> Posts { get; set; }
		public ICollection<Post> Favorits { get; set; }

		public string Salt { get; set; }

	}
}
