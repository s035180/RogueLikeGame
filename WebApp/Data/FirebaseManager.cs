using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data
{
    public class FirebaseManager
    {

        public FirebaseManager()
        {

            Debug.Write("FirebaseManager Started");

            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "TQHDCe8d9B8rQGOBAMPe9kKBHHDSmrRSIXyN5Kwm",
                BasePath = "https://roguelikegame-64f8f-default-rtdb.firebaseio.com/"
            };

            IFirebaseClient client = new FirebaseClient(config);

            if(client != null)
            {
                Debug.Write("Connected");
                getLeaderboardsAsync(client);
            }
        }

        private async void getLeaderboardsAsync(IFirebaseClient client)
        {
            FirebaseResponse response = await client.GetAsync("users");

            Dictionary<string, Leaderboard> data = JsonConvert.DeserializeObject<Dictionary<string, Leaderboard>>(response.Body.ToString());

            Data.saveData(data);

        }
    }
}
