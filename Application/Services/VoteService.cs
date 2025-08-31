using MyApi.Application.Services.Interfaces;
using MyApi.Domain.Entities;
using MyApi.Domain.Interfaces;

namespace MyApi.Application.Services
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;

        public VoteService(IVoteRepository userRepository)
        {
            _voteRepository = userRepository;
        }

        public async Task<IEnumerable<VoteEntity>> GetVotesAsync()
        {
            return await _voteRepository.GetVotesAsync();
        }

        public async Task AddVoteAsync(VoteEntity vote)
        {
            await _voteRepository.AddVoteAsync(vote);
        }
    }
}
