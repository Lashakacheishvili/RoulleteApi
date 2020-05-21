using RoulleteApi.Core;
using RoulleteApi.Repository.Ef;
using RoulleteApi.Repository.Interfaces;

namespace RoulleteApi.Repository.Implementation
{
    public class JackpotRepository : BaseRepository<Jackpot, int>, IJackpotRepository
    {
        public JackpotRepository(RoulleteDbContext context) : base(context) { }
    }
}
