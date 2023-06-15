using cwzpgpract.Data;
using cwzpgpract.DB;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;


namespace cwzpgpract.Hubs
{
    public class CHub : Hub
    {
        private readonly ILogger<CHub> _logger;
        private readonly MyDbContext _dbContext;
        private static IHubContext<CHub> _hubContext;

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation($"Client connected: {Context.ConnectionId}");
            List<Location> AllGivenLocs = await _dbContext.Locations
                .Where(x => x.Id > 3)
                .ToListAsync();
            List<Item> AllGivenItems = await _dbContext.Items
                .Where(x => x.Id > 23)
                .ToListAsync();
            List<NPC> AllGivenNpcs = await _dbContext.NPCS
                .Where(x => x.Id > 8)
                .ToListAsync();
            List<Location> AllLoc = await _dbContext.Locations
                .ToListAsync();
            List<Item> AllItems = await _dbContext.Items
                .ToListAsync();
            await Clients.Caller.SendAsync("RecieveDataL", AllGivenLocs);
            await Clients.Caller.SendAsync("RecieveDataI", AllGivenItems);
            await Clients.Caller.SendAsync("RecieveDataN", AllGivenNpcs);
            await Clients.Caller.SendAsync("RecieveAllL", AllLoc);
            await Clients.Caller.SendAsync("RecieveAllI", AllItems);
            await base.OnConnectedAsync();
        }

        public CHub(ILogger<CHub> logger, MyDbContext dbContext, IHubContext<CHub> hubContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _hubContext = hubContext;
        }

        public async Task<Task> SendDataL()
        {
            _logger.LogInformation("SendDataL called");
            List<Location> AllGivenLocs = await _dbContext.Locations
                .Where(x => x.Id > 3)
                .ToListAsync();
            return Clients.All.SendAsync("RecieveDataL", AllGivenLocs);
        }
        public async Task<Task> SendDataI()
        {
            _logger.LogInformation("SendDataI called");
            List<Item> AllGivenItems = await _dbContext.Items
                .Where(x => x.Id > 23)
                .ToListAsync();
            return Clients.All.SendAsync("RecieveDataI", AllGivenItems);
        }
        public async Task<Task> SendDataN()
        {
            _logger.LogInformation("SendDataN called");
            List<NPC> AllGivenNpcs = await _dbContext.NPCS
                .Where(x => x.Id > 8)
                .ToListAsync();
            return Clients.All.SendAsync("RecieveDataN", AllGivenNpcs);
        }
        public async Task<Task> SendAllL()
        {
            _logger.LogInformation("SendAllL called");
            List<Location> AllLoc = await _dbContext.Locations
                .ToListAsync();
            return Clients.All.SendAsync("RecieveAllL", AllLoc);
        }
        public async Task<Task> SendAllI()
        {
            _logger.LogInformation("SendAllI called");
            List<Item> AllItems = await _dbContext.Items
                .ToListAsync();
            return Clients.All.SendAsync("RecieveAllI", AllItems);
        }
    }
}
