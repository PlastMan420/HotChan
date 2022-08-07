using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotChan.DataBase.Models.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public string CommentText { get; set; }
    public DateTimeOffset CreatedOn
    {
        get => CreatedOn;
        set
        {
            CreatedOn = DateTime.UtcNow;
        }
    }
    public DateTimeOffset LastModified
    {
        get => LastModified;
        set
        {
            LastModified = DateTime.UtcNow;
        }
    }
    public bool IsModified { get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public User User { get; set; }

    [ForeignKey("Post")]
    public Guid PostId { get; set; }
    public Post Post { get; set; }
}
