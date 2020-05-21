using Microsoft.AspNetCore.Identity;
using System;

namespace RoulleteApi.Core
{
    public class UserRole : IdentityRole<Guid>
    {
        public UserRole() : base()
        {
            Id = Guid.NewGuid();
        }

        public UserRole(string roleName) : base(roleName)
        {
            Id = Guid.NewGuid();
        }
    }
}
