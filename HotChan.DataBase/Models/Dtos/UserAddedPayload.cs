using System;

namespace HotChan.DataBase.Models.Dtos;

public class UserAddedPayload
{
	public DateTimeOffset CreationTimeOffset{ get; set; }
	public string UserName { get; set; }
}

