using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotChan.DataBase.Models.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public string CommentText { get; set; }
    public int Score { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public bool IsModified { get; set; }
    public ReplyThread Thread { get; set; }
    public User User { get; set; }
}
