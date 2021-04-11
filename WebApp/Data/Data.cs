using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data
{
    public static class Data
    {
        private static Dictionary<string, Leaderboard> data;

        public static Dictionary<string, Leaderboard> returnData()
        {
            if (data != null)
            {
                if (data.Count > 0)
                {
                    return data;
                }
            }
            return new Dictionary<string, Leaderboard>();
        }

        public static void saveData(Dictionary<string, Leaderboard> Data)
        {
            data = Data;
        }
    }
}
