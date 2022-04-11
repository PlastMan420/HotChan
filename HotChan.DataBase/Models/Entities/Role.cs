using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace HotChan.DataBase.Models.Entities;

public class Role : IdentityRole<Guid>
{
	public Role() : base()
	{
	}

	public ICollection<UserRole> UserRoles { get; set; }

}

