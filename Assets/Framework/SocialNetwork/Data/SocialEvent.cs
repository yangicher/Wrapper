namespace Assets.Framework.SocialNetwork.Data
{
    public enum SocialEvent
    {
        INIT,
        LOGIN,
        LOGIN_SUCCESS,
        LOGIN_CANCEL,
        LOGIN_ERROR,
        LOGOUT,
        LOGOUT_SUCCESS,
        GET_USER_AND_FRIENDS,
        ON_GET_USER_AND_FRIENDS,
        SEND_REQUEST,
        REQUEST_SUCCESS,
        REQUEST_CANCEL,
        REQUEST_ERROR,
        GET_USERS,
        ON_GET_USERS
    }

//    public class InitSignal : Signal<string> { }
//    public class LoginSignal : Signal { }
//    public class LogoutSignal : Signal { }
//    public class GetUserAndFriendsSignal : Signal { }
//    public class RequestSignal : Signal { }
}
