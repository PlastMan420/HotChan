using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace HotChan.DataBase.Models.Entities;

public class User : IdentityUser<Guid>
{
	[PersonalData]
	public DateTimeOffset RegisterationDate {
		get => RegisterationDate;
		init => RegisterationDate = value;
    }
	public DateTimeOffset LastOnline { 
		get => LastOnline;
		init => LastOnline = DateTime.UtcNow;
	}
	public DateTimeOffset DOB { get; set; }

	public Uri Avatar { get; set; }

	public ICollection<UserRole> UserRoles { get; set; }
	public ICollection<Post> Posts { get; set; }
	public ICollection<bookmarks> Favorits { get; set; }

	public string Salt { get; set; }

	public string SaltHash { get; set; }

	public bool IsBanned { get; set; }
	public bool IsDisabled { get; set; }
}

