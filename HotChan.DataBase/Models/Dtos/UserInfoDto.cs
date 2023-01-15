using System;

namespace HotChan.DataBase.Models.Dtos
{
    public  class UserInfoDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DOB { get; set; }
        public DateTime LastActivityOn { get; set;}
    }
}
