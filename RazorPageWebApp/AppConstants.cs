namespace RazorPageWebApp;

public class AppConstants
{
    public static string CURRENT_USER = "CurrentUser";
    public static string UNIT_OF_WORK_OBJ = "UnitOfWorkObj";
    public static string USER_ID = "UserId";
    public static string USER_NAME = "UserName";
    public static string USER_AVATAR = "UserAvatar";
    public static int GROUP_PAGE_SIZE = 8;
    public static int POST_PAGE_SIZE = 10;
    public static int TOP_GROUP_NUM = 4;
    public static int UNREAD_NOTIFICATION_NUM = 5;
    public static string LiveChatMSG(Guid groupId) => $"LiveChat_Messages_{groupId}";

    public static string GetNotifySession(Guid userId)
    {
        return $"NOTIFY-{userId}";
    }
}
