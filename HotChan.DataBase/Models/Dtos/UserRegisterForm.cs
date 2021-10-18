using System.ComponentModel.DataAnnotations;

namespace HotChan.DataBase.Models.Dtos;

public class UserRegisterForm
{
	[Required]
	public string Name { get; set; }
		
	[Required]
	public string EmailAddress { get; set; }

	[Required]
	public string Key { get; set; }

}

