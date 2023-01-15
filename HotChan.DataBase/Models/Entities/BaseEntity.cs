using System;

namespace HotChan.DataBase.Models.Entities;
public abstract class BaseEntity
{
    public DateTimeOffset CreatedOn
    {
        get; set;
    }
}
