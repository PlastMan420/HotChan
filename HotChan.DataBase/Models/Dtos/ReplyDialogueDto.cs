using System;
using System.ComponentModel.DataAnnotations;

namespace HotChan.DataBase.Models.Dtos;

public class ReplyDialogueDto
{
	[Required]
	public Guid PostId { get; set; }
	[Required]
	public Guid UserId { get; set; }
	[Required]
	public string Comment { get; set; }
}

