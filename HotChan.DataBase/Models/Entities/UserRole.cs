using Microsoft.AspNetCore.Identity;
using System;

namespace HotChan.DataBase.Models.Entities;

public class UserRole : IdentityUserRole<Guid>
{
	public User User { get; set; }
	public Role Role { get; set; }
}

