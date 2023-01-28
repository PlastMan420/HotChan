using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotChan.DataBase.Models.Entities;

[Index(nameof(postId), nameof(userId), IsUnique = true)]
[PrimaryKey(nameof(userId), nameof(postId))]
public class PostScore
{
    public Guid postId { get; set; }

    [NotMapped]
    public Post post { get; set; }

    public Guid userId { get; set; }

    [NotMapped]
    public User user { get; set; }
    public int score { get; set; }
}
