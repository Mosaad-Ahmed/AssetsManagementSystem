namespace AssetsManagementSystem.Services.Auth.Exceptions
{
    public class UserShouldBeFoundException : BaseException
    {
        public UserShouldBeFoundException(string message) : base(message) { }
    }
}
