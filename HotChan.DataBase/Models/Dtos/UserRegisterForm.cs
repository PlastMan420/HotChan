using System.ComponentModel.DataAnnotations;

namespace HotChan.DataBase.Models.Dtos;

public class UserRegisterFormDto
{
	[Required]
	public string UserName { get; set; }
		
	[Required]
	public string UserMail { get; set; }

	[Required]
	public string Key { get; set; }

}

public class UserLoginDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Key { get; set; }
}
