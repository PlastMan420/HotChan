namespace HotChan.DataBase;

public class Enums
{
    public enum eRole
    {
        Banned = 0,
        Admin = 1,
        User = 2
    }

    public enum ePostState
    {
        Deleted = 0,
        Active,
        Hidden,
        UnderReview
    }
}
