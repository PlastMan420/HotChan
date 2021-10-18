using System.ComponentModel.DataAnnotations;

namespace HotChan.DataBase.Models.Dtos;

public class UserAuth
{
	public string UserName { get; set; }

	[EmailAddress]
	public string UserMail { get; set; }

	[Required]
	public string Key { get; set; }
}

