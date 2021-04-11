using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data
{
    public class LeaderboardService
    {
        private Dictionary<string, Leaderboard> data = new Dictionary<string, Leaderboard>();

        public void getData()
        {
            while(data.Count < 1)
            {
                data = Data.returnData();
            }
        }

        public Task<List<Leaderboard>> GetLeaderboardAsync()
        {
            getData();
            List<Leaderboard> lb = data.Values.ToList();

            lb = lb.OrderByDescending(item => item.score).ToList();

            return Task.FromResult(lb);
        }

       
    }
}
