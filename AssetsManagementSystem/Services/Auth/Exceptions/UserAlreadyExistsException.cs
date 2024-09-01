namespace AssetsManagementSystem.Services.Auth.Exceptions
{
    public class UserAlreadyExistsException : BaseException
    {
        public UserAlreadyExistsException(string message) 
            : base(message) { }

    }
}
