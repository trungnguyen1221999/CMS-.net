

using Microsoft.AspNetCore.Identity;

namespace CMS.Core.Domain.Identity
{
    public class AppRole : IdentityRole<Guid>
    {
        public required string DisplayName { get; set; }
    }
}
