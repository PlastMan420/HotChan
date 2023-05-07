using System;
using System.Collections.Generic;

namespace HotChan.DataBase.Models.Entities
{
    public class ReplyThread
    {
        public Guid Id { get; set; }
        public bool Locked { get; set; }
    }
}
