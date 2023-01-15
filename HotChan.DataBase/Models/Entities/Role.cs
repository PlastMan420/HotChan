using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using static HotChan.DataBase.Enums;

namespace HotChan.DataBase.Models.Entities;

public class Role : IdentityRole<Guid>
{
	public Role() : base()
	{
	}

	public ICollection<UserRole> UserRoles { get; set; }
	public eRole eRoleId { get; set; }
}
