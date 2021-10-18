using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace HotChan.DataBase.Models.Entities;

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

