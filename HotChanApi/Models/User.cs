using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace HotChanApi.Models
{
	public class User : IdentityUser<Guid>
	{
		public DateTime RegisterationDate { get; set; }
		public DateTime LastOnline { get; set; }
		public DateTime DOB { get; set; }

		[PersonalData]
		public Uri Avatar { get; set; }

		public ICollection<UserRole> UserRoles { get; set; }
		public ICollection<Post> Posts { get; set; }

	}
}
