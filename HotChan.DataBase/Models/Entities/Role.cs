using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChan.DataBase.Models.Entities;

public class Role : IdentityRole<Guid>
{
	public Role(string roleName) : base(roleName)
	{
	}

	public ICollection<UserRole> UserRoles { get; set; }

}

