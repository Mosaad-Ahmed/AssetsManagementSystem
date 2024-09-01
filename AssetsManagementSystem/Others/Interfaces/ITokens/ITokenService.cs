namespace AssetsManagementSystem.Others.Interfaces.ITokens
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateToken(User user, IList<string> roles);

        string GenerateRefreshToken();

        ClaimsPrincipal? GetClaimsPrincipalFromExpiredToken(string? token);

    }
}
