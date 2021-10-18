using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace HotChan.DataBase.Models.Entities;

[Index(nameof(ReplyId), IsUnique = true)]
public class Reply
{
	[Key]
	public Guid		ReplyId				{ get; set; }

	[Required]
	public Guid		PostId				{ get; set; }

	[Required]
	public Guid		UserId				{ get; set; }

	[Required]
	[MaxLength(400)]
	public string	Comment				{ get; set; }

	[Required]
	public DateTimeOffset Time				{ get; set; }

	[Required]
	public Uri		AvatarThumbnailUrl	{ get; set; }
}

