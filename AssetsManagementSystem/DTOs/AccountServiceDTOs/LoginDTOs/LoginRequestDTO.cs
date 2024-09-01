namespace AssetsManagementSystem.DTOs.AccountServiceDTOs.LoginDTOs
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(255, ErrorMessage = "Email can't be longer than 255 characters.")]
        [DefaultValue("Enter Your Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [DefaultValue("Enter Your Password")]
        public string Password { get; set; }
    }
}
