using DapperEFCorePerformanceBenchmarks.DataAccess;
using DapperEFCorePerformanceBenchmarks.Models;
using DapperEFCorePerformanceBenchmarks.TestData;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

public class EntityFrameworkCoreWithTracking : ITestSignature
{
    public long GetPlayerByID(int id)
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();
        using (SportContextEfCore context = new SportContextEfCore(Database.GetOptions()))
        {
            var player = context.Players.First(x=>x.Id == id);
        }
        watch.Stop();
        return watch.ElapsedMilliseconds;
    }

public long GetRosterByTeamID(int teamId)
{
    Stopwatch watch = new Stopwatch();
    watch.Start();
    using (SportContextEfCore context = new SportContextEfCore(Database.GetOptions()))
    {
        var teamRoster = context.Teams.Include(x => x.Players).Single(x => x.Id == teamId);
    }
    watch.Stop();
    return watch.ElapsedMilliseconds;
}

public long GetTeamRostersForSport(int sportId)
{
    Stopwatch watch = new Stopwatch();
    watch.Start();
    using (SportContextEfCore context = new SportContextEfCore(Database.GetOptions()))
    {
        var players = context.Teams.Include(x => x.Players).Where(x => x.SportId == sportId).ToList();
    }
    watch.Stop();
    return watch.ElapsedMilliseconds;
}
}