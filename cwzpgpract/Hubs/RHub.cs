using cwzpgpract.Data;
using cwzpgpract.DB;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;


namespace cwzpgpract.Hubs
{
    public class RHub : Hub
    {
        private readonly ILogger<RHub> _logger;
        private readonly MyDbContext _dbContext;
        private static IHubContext<RHub> _hubContext;

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation($"Client connected: {Context.ConnectionId}");
            List<string> lastFourEntries = await _dbContext.Journals
                    .OrderByDescending(x => x.Id)
                    .Take(4)
                    .Select(x => x.Happening ?? "")
                    .ToListAsync();

            await Clients.Caller.SendAsync("ReceiveLastFourEntries", lastFourEntries);
            await base.OnConnectedAsync();
        }

        public RHub(ILogger<RHub> logger, MyDbContext dbContext, IHubContext<RHub> hubContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _hubContext = hubContext;
        }

        public async Task<Task> GetLastFourEntries()
        {
            _logger.LogInformation("GetLastFourEntries called");
            List<string> lastFourEntries = await _dbContext.Journals
            .OrderByDescending(x => x.Id)
            .Take(4)
            .Select(x => x.Happening ?? "")
            .ToListAsync();
            return Clients.All.SendAsync("ReceiveLastFourEntries", lastFourEntries);
        }
    }

}
