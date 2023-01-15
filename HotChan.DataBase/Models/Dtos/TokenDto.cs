
namespace HotChan.DataBase.Models.Dtos;

public class TokenDto
{
    public string token { get; set; }
    public long unixTimeExpiresAt { get; set; }
    public string message { get; set; }
}
