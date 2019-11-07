using System;
using System.Collections.Generic;
using System.Text;

namespace DapperEFCorePerformanceBenchmarks.DataAccess
{
    public interface ITestSignature
    {
        long GetPlayerByID(int id);
        long GetRosterByTeamID(int teamID);
        long GetTeamRostersForSport(int sportID);
    }
}
