using System;
using System.ComponentModel.DataAnnotations;

namespace HotChan.DataBase.Models.Entities;

public class Post : BaseEntity
{
    [Key]
    public Guid PostId { get; set; }

    [Required]
    [MaxLength(20)]
    public string PostTitle { get; set; }

    public string Description { get; set; }

    public Uri MediaUrl { get; set; }

    public Uri ThumbnailUrl { get; set; }

    public string[] Tags { get; set; }

    public bool Hidden { get; set; }
    
    public ReplyThread Thread { get; set; } //comments table

    public User User { get; set; }

    public int Score { get; set; }
}
