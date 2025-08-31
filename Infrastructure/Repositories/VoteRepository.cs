using Microsoft.EntityFrameworkCore;
using MyApi.Domain.Interfaces;
using MyApi.Domain.Entities;
using MyApi.Infrastructure.Data;

namespace MyApi.Infrastructure.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly ApplicationDbContext _context;

        public VoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VoteEntity>> GetVotesAsync()
        {
            return await _context.Votes
                .ToListAsync();
        }

        public async Task AddVoteAsync(VoteEntity vote)
        {
            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();
        }
    }
}