using System;
using System.Collections.Generic;

namespace HotChan.DataBase.Models.Entities
{
    public class bookmarks
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
