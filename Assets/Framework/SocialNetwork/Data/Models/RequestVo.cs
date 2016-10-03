namespace Assets.Framework.SocialNetwork.Data.Models
{
    public class RequestVo
    {
        public string[] To;
        public string Title;
        public string ActionType;
        public string Message;
        public string[] Filters; //Parameters to, filters and suggestions are mutually exclusive
        public string Argument;
        public string ImageUrl;
        public string RequestName;
    }
}