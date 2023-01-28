using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotChan.DataBase.Models.Entities;

[Index(nameof(BookmarkListId), nameof(userId), IsUnique = true)]
[PrimaryKey(nameof(userId), nameof(BookmarkListId))]
public class Bookmarks
{
    [Key]
    public Guid BookmarkListId { get; set; }
    public Guid userId { get; set; }

    [NotMapped]
    public User user { get; set; }
    public ICollection<Post> Favorites { get; set; }
}
