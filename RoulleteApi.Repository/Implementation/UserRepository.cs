using RoulleteApi.Core;
using RoulleteApi.Repository.Ef;
using RoulleteApi.Repository.Interfaces;
using System;

namespace RoulleteApi.Repository.Implementation
{
    public class UserRepository : BaseRepository<User, Guid>, IUserRepository
    {
        public UserRepository(RoulleteDbContext context) : base(context) { }
    }
}
