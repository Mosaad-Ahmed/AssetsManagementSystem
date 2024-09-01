namespace AssetsManagementSystem.Services.Auth.Exceptions
{
    public class RefreshTokenExpiryTimeShouldnotbeExpiredException : BaseException
    {
        public RefreshTokenExpiryTimeShouldnotbeExpiredException(string message) : base(message) { }
     }
}
