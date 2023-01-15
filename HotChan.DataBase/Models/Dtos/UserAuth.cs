using System.ComponentModel.DataAnnotations;
using static HotChan.DataBase.Enums;

namespace HotChan.DataBase.Models.Dtos;

public class UserAuth
{
	[Required]
	public string UserName { get; set; }

	[EmailAddress]
	[Required]
	public string UserMail { get; set; }

	[Required]
	public string Key { get; set; }

	public eRole Role { get; set; }
}

