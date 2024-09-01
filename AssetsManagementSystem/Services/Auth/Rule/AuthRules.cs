namespace AssetsManagementSystem.Services.Auth.Rule
{
    public class AuthRules:BaseRule
    {
        public Task UserShouldnotBeExistsAsync(User? user)
        {
            if (user is not null)
                throw new UserAlreadyExistsException("This User has registered before, use another Values");
            return Task.CompletedTask;

        }

        public Task RoleShouldBeExistsAsync(bool isExist)
        {
            if (!isExist)
                throw new RoleShouldBeExistsException("Choosen Role Is not Exist");
            return Task.CompletedTask;
        }
        public Task<bool> OperationSucceed(bool isCreaded)
        {
            if (!isCreaded)
                throw new OperationSucceedException("Operation failed");
            return  Task.FromResult(true);
        }

        public Task EmailOrPasswordShouldnotbeInvalidAsync(User user, bool checkPassword)
        {
            if (user is null || !checkPassword)
                throw new EmailOrPasswordShouldnotbeInvalidException
                    ("Email,or Password is Mistake,Try With Other Correct Values ");
            return Task.CompletedTask;
        }

        public Task RefreshTokenExpiryTimeShouldnotbeExpiredAsync(DateTime? RefreshTokenExpiryTime)
        {
            if (RefreshTokenExpiryTime < DateTime.Now)
                throw new RefreshTokenExpiryTimeShouldnotbeExpiredException("Session has Expired");
            return Task.CompletedTask;
        }

        public Task UserShouldBeFoundAsync(User user)
        {
            if (user is null)
                throw new UserShouldBeFoundException("User isn`t exist");
            return Task.CompletedTask;
        }
    }
}
