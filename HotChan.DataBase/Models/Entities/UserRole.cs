using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotChan.DataBase.Models.Entities;

public class UserRole : IdentityUserRole<Guid>
{
    [NotMapped]
    public Role Role { get; set; }
}
