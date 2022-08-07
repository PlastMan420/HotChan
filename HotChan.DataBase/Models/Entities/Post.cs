using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotChan.DataBase.Models.Entities;

public class Post
{
	[Key]
	public Guid PostId { get; set; }
		
	[Required]
	[MaxLength(20)]
	public string PostTitle { get; set; }

	public string Description { get; set; }

	public DateTimeOffset CreatedOn
	{ 
		get => CreatedOn; 
		set 
		{
			CreatedOn = DateTime.UtcNow; 
		} 
	}

	public Uri MediaUrl { get; set; }

	public Uri ThumbnailUrl { get; set; }

	public string[] Tags { get; set; }

	[ForeignKey("User")]
	public Guid UserId { get; set; }
	public User User { get; set; }

}
