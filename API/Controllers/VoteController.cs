using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using AutoMapper;

using MyApi.Application.DTOs.UserDTOs;
using MyApi.Application.Services.Interfaces;
using MyApi.Domain.Entities;

namespace MyApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;
        private readonly IMapper _mapper;

        public VoteController(IVoteService voteService, IMapper mapper)
        {
            _voteService = voteService;
            _mapper = mapper;
        }

        [HttpGet]
        // [Authorize]
        public async Task<ActionResult<IEnumerable<VoteEntity>>> GetVotes()
        {
            var votes = await _voteService.GetVotesAsync();
            return Ok(votes);
        }

         [HttpPost]
        // [Authorize]
        public async Task<IActionResult> CreateVote(VoteEntity vote)
        {
            await _voteService.AddVoteAsync(vote);
           
            return Ok();
        }
    }
}