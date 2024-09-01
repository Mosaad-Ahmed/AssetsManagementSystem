namespace AssetsManagementSystem.Services.Auth.Exceptions
{
    public class EmailOrPasswordShouldnotbeInvalidException : BaseException
    {
        public EmailOrPasswordShouldnotbeInvalidException(string message)
            : base(message) { }

    }
}
