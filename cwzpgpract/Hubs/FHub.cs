using cwzpgpract.Data;
using cwzpgpract.DB;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace cwzpgpract.Hubs
{
    public class FHub : Hub
    {
        private readonly ILogger<FHub> _logger;
        private readonly MyDbContext _dbContext;
        private static IHubContext<FHub> _hubContext;

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation($"Client connected: {Context.ConnectionId}");
            List<string> allEntries = await _dbContext.Journals
                .OrderByDescending(x => x.Id)
                .Select(x => x.Happening ?? "")
                .ToListAsync();

            await Clients.Caller.SendAsync("RecieveAllEntries", allEntries);
            await base.OnConnectedAsync();
        }
        public FHub(ILogger<FHub> logger, MyDbContext dbContext, IHubContext<FHub> hubContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _hubContext = hubContext;
        }
        public async Task<Task> SendEntityUpdate()
        {
            _logger.LogInformation("SendEntityUpdate called");

            List<string> allEntries = await _dbContext.Journals
                .OrderByDescending(x => x.Id)
                .Select(x => x.Happening ?? "")
                .ToListAsync();

            return Clients.All.SendAsync("RecieveAllEntries", allEntries);
        }
    }
}
