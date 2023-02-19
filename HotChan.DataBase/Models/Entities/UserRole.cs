using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotChan.DataBase.Models.Entities;

public class UserRole : IdentityUserRole<Guid>
{
    public Role Role { get; set; }

    public User User { get; set; }
}
