using System.Collections.Generic;

namespace HotChan.DataBase.Models.Results
{
    public class RegistrationResponse
    {
        public List<string> Errors { get; set; }
        public bool Success { get; set; }
    }
}
