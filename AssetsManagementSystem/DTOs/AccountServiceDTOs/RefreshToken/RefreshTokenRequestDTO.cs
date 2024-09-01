namespace AssetsManagementSystem.DTOs.AccountServiceDTOs.RefreshToken
{
    public class RefreshTokenRequestDTO
    {
        [Required(ErrorMessage = "AccessToken is required.")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "AccessToken must be between 10 and 100 characters.")]
        [DefaultValue("Enter AccessToken")]
        public string AccessToken { get; set; }

        [Required(ErrorMessage = "RefreshToken is required.")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "RefreshToken must be between 10 and 100 characters.")]
        [DefaultValue("Enter RefreshToken")]
        public string RefreshToken { get; set; }
    }
}
