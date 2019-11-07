using Dapper;
using DapperEFCorePerformanceBenchmarks.DTOs;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace DapperEFCorePerformanceBenchmarks.DataAccess
{
    public class Dapper : ITestSignature
    {
        public long GetPlayerByID(int id)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SqlConnection conn = new SqlConnection(Constants.SportsConnectionString))
            {
                conn.Open();
                var player = conn.QuerySingle<PlayerDTO>("SELECT Id, FirstName, LastName, DateOfBirth, TeamId FROM Player WHERE Id = @ID", new { ID = id });
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long GetRosterByTeamID(int teamId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SqlConnection conn = new SqlConnection(Constants.SportsConnectionString))
            {
                conn.Open();
                var team = conn.QuerySingle<TeamDTO>("SELECT Id, Name, SportID, FoundingDate FROM Team WHERE ID = @id", new { id = teamId });

                team.Players = conn.Query<PlayerDTO>("SELECT Id, FirstName, LastName, DateOfBirth, TeamId FROM Player WHERE TeamId = @ID", new { ID = teamId }).ToList();
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long GetTeamRostersForSport(int sportId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SqlConnection conn = new SqlConnection(Constants.SportsConnectionString))
            {
                conn.Open();
                var teams = conn.Query<TeamDTO>("SELECT ID, Name, SportID, FoundingDate FROM Team WHERE SportID = @ID", new { ID = sportId });

                var teamIDs = teams.Select(x => x.Id).ToList();

                var players = conn.Query<PlayerDTO>("SELECT ID, FirstName, LastName, DateOfBirth, TeamID FROM Player WHERE TeamID IN @IDs", new { IDs = teamIDs });

                foreach (var team in teams)
                {
                    team.Players = players.Where(x => x.TeamId == team.Id).ToList();
                }
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}