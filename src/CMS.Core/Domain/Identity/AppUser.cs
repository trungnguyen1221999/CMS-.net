using Microsoft.AspNetCore.Identity;


namespace CMS.Core.Domain.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public bool IsActive { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? Dob { get; set; }


        public string? Avatar { get; set; }
        public DateTime? VipStartDate { get; set; }
        public DateTime? VipExpireDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public decimal Balance { get; set; }
        public decimal RoyaltyAmountPerPost { get; set; }

    }
}
