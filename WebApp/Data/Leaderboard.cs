using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data
{
    public class Leaderboard
    {
        public int deaths { get; set; }
        public int kills { get; set; }
        public int score { get; set; }
        public string username { get; set; }
    }
}
