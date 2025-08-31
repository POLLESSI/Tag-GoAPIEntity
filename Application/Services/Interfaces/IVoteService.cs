using MyApi.Domain.Entities;

namespace MyApi.Application.Services.Interfaces
{
    public interface IVoteService
    {
        Task<IEnumerable<VoteEntity>> GetVotesAsync();

        Task AddVoteAsync(VoteEntity vote);
    }
}
