namespace cwzpgpract.Data;
using cwzpgpract.DB;
using cwzpgpract.Hubs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;


public class BGCreateService : IHostedService, IDisposable
{
    [Inject]
    protected IHubContext<RHub> MyHubContext { get; set; }
    protected IHubContext<CHub> MyHubContext2 { get; set; }
    private Timer _timer;

    public BGCreateService(IHubContext<RHub> myHubContext, IHubContext<CHub> myHubContext2)
    {
        MyHubContext = myHubContext;
        MyHubContext2 = myHubContext2;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        return Task.CompletedTask;
    }
    private async void DoWork(object state)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
        optionsBuilder.UseNpgsql("Postgres");
        var dbContext = new MyDbContext(optionsBuilder.Options);
        Random rnd = new Random();
        string strr = "";
        int temp = 0;
        var options = dbContext.Eventeds.OrderBy(x => x.Id).ToList();
        PlayableChar player_ = dbContext.PlayableChars.FirstOrDefault(e => e.Id == 1);
        Location curloc = dbContext.Locations.FirstOrDefault(c => c.Id == player_.CurLoc);
        temp = rnd.Next(0, 9);
        strr = temp.ToString();
        var itemstemp = dbContext.Items.GroupBy(c => c.Location_ == curloc).ToList();
        var npctemp = dbContext.NPCS.GroupBy(c => c.Location_ == curloc).ToList();
        var randomitem = itemstemp[new Random().Next(itemstemp.Count)];
        var randomnpc = npctemp[new Random().Next(npctemp.Count)];
        switch (temp)
        {
            case 1:

                Evented fup = dbContext.Eventeds.FirstOrDefault(x => x.Id == 1);
                Item curone = (Item)randomitem.ElementAt(rnd.Next(randomitem.Count()));
                NPC curone2 = (NPC)randomnpc.ElementAt(rnd.Next(randomnpc.Count()));
                Journal happening = new Journal() { event_ = fup, name_ = curone2, item_ = curone, player = player_ };
                happening.Happening = $"{fup.Followup} {curone.Name} он/a {curone.Description}{fup.Ending}";
                dbContext.Journals.Add(happening);
                dbContext.SaveChanges();
                break;
            case 2:
                Evented fup2 = dbContext.Eventeds.FirstOrDefault(x => x.Id == 2);
                Item curtwo = (Item)randomitem.ElementAt(rnd.Next(randomitem.Count()));
                NPC curtwo2 = (NPC)randomnpc.ElementAt(rnd.Next(randomnpc.Count()));
                Journal happening2 = new Journal() { event_ = fup2, name_ = curtwo2, item_ = curtwo, player = player_ };
                happening2.Happening = $"{fup2.Followup} {curtwo2.Name} {curtwo.Name}, глядя на него могу сказать {curtwo.Description}{fup2.Ending}";
                dbContext.Journals.Add(happening2);
                dbContext.SaveChanges();
                break;
            case 3:
                Evented fup3 = dbContext.Eventeds.FirstOrDefault(x => x.Id == 3);
                Item curthree = (Item)randomitem.ElementAt(rnd.Next(randomitem.Count()));
                NPC curthree2 = (NPC)randomnpc.ElementAt(rnd.Next(randomnpc.Count()));
                Journal happening3 = new Journal() { event_ = fup3, name_ = curthree2, item_ = curthree, player = player_ };
                happening3.Happening = $"{fup3.Followup} {curthree.Name}, {fup3.Ending}";
                dbContext.Journals.Add(happening3);
                break;
            case 4:
                Evented fup4 = dbContext.Eventeds.FirstOrDefault(x => x.Id == 4);
                Item curfour = (Item)randomitem.ElementAt(rnd.Next(randomitem.Count()));
                NPC curfour2 = (NPC)randomnpc.ElementAt(rnd.Next(randomnpc.Count()));
                Journal happening4 = new Journal() { event_ = fup4, name_ = curfour2, item_ = curfour, player = player_ };
                happening4.Happening = $"{fup4.Followup} {curfour.Name}, оно {curfour.Description}{fup4.Ending}";
                dbContext.Journals.Add(happening4);
                dbContext.SaveChanges();
                break;
            case 5:
                int randomloc = rnd.Next(0, dbContext.Locations.Count());
                while (randomloc == player_.CurLoc && dbContext.Locations.FirstOrDefault(x => x.Id == randomloc).Items_.Count() < 1 && dbContext.Locations.FirstOrDefault(x => x.Id == randomloc).NPC_.Count() < 1)
                {
                    randomloc = rnd.Next(0, dbContext.Locations.Count());
                }
                player_.CurLoc = randomloc;
                Evented fup5 = dbContext.Eventeds.FirstOrDefault(x => x.Id == 5);
                Item curfive = (Item)randomitem.ElementAt(rnd.Next(randomitem.Count()));
                NPC curfive2 = (NPC)randomnpc.ElementAt(rnd.Next(randomnpc.Count()));
                Location locnum = dbContext.Locations.FirstOrDefault(x => x.Id == player_.CurLoc + 1);
                Journal happening5 = new Journal() { event_ = fup5, name_ = curfive2, item_ = curfive, player = player_ };
                happening5.Happening = $"{fup5.Followup} {locnum.Name}, {fup5.Ending}";
                dbContext.Journals.Add(happening5);
                dbContext.SaveChanges();
                break;
            case 6:
                Evented fup6 = dbContext.Eventeds.FirstOrDefault(x => x.Id == 6);
                Item cursix = (Item)randomitem.ElementAt(rnd.Next(randomitem.Count()));
                NPC cursix2 = (NPC)randomnpc.ElementAt(rnd.Next(randomnpc.Count()));
                Journal happening6 = new Journal() { event_ = fup6, name_ = cursix2, item_ = cursix, player = player_ };
                happening6.Happening = $"{fup6.Followup} {cursix2.Name} {cursix.Name}, {cursix.Description}{fup6.Ending}";
                dbContext.Journals.Add(happening6);
                dbContext.SaveChanges();
                break;
            case 7:
                Evented fup7 = dbContext.Eventeds.FirstOrDefault(x => x.Id == 7);
                Item curseven = (Item)randomitem.ElementAt(rnd.Next(randomitem.Count()));
                NPC curseven2 = (NPC)randomnpc.ElementAt(rnd.Next(randomnpc.Count()));
                Journal happening7 = new Journal() { event_ = fup7, name_ = curseven2, item_ = curseven, player = player_ };
                happening7.Happening = $"{fup7.Followup} {curseven2.Name},{fup7.Ending}";
                dbContext.Journals.Add(happening7);
                dbContext.SaveChanges();
                break;
            case 8:
                Evented fup8 = dbContext.Eventeds.FirstOrDefault(x => x.Id == 8);
                Item cureight = (Item)randomitem.ElementAt(rnd.Next(randomitem.Count()));
                NPC cureight2 = (NPC)randomnpc.ElementAt(rnd.Next(randomnpc.Count()));
                Journal happening8 = new Journal() { event_ = fup8, name_ = cureight2, item_ = cureight, player = player_ };
                happening8.Happening = $"{cureight2.Name} {fup8.Followup} {cureight.Name} {fup8.Ending}";
                dbContext.Journals.Add(happening8);
                dbContext.SaveChanges();
                break;
        };
        dbContext.SaveChanges();
        List<string> lastFourEntries = await dbContext.Journals
        .OrderByDescending(x => x.Id)
        .Take(4)
        .Select(x => x.Happening ?? "")
        .ToListAsync();

        // Send the last four entries to the connected clients
        await MyHubContext.Clients.All.SendAsync("ReceiveLastFourEntries", lastFourEntries);

        List<string> allEntries = await dbContext.Journals
            .OrderByDescending(x => x.Id)
            .Select(x => x.Happening ?? "")
            .ToListAsync();

        await MyHubContext.Clients.All.SendAsync("RecieveAllEntries", allEntries);

        List<Location> AllGivenLocs = await dbContext.Locations
                .Where(x => x.Id > 3)
                .ToListAsync();

        List<Item> AllGivenItems = await dbContext.Items
                        .Where(x => x.Id > 23)
                        .ToListAsync();

        List<NPC> AllGivenNpcs = await dbContext.NPCS
                        .Where(x => x.Id > 8)
                        .ToListAsync();

        List<Location> AllLoc = await dbContext.Locations
                        .ToListAsync();

        List<Item> AllItems = await dbContext.Items
                        .ToListAsync();

        await MyHubContext2.Clients.All.SendAsync("RecieveAllI", AllItems);
        await MyHubContext2.Clients.All.SendAsync("RecieveAllL", AllLoc);
        await MyHubContext2.Clients.All.SendAsync("RecieveDataN", AllGivenNpcs);
        await MyHubContext2.Clients.All.SendAsync("RecieveDataI", AllGivenItems);
        await MyHubContext2.Clients.All.SendAsync("RecieveDataL", AllGivenLocs);

        Console.WriteLine("BGS is running...");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
