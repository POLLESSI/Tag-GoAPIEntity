using MyApi.Domain.Entities;

namespace MyApi.Domain.Interfaces
{
    public interface IVoteRepository
    {
        Task<IEnumerable<VoteEntity>> GetVotesAsync();

        Task AddVoteAsync(VoteEntity vote);
    }
}
