using System.Collections.Generic;
using System.Net.Http;

namespace SimpleSC2data
{
    public class SC2API
    {
        public HttpResponseMessage runRequest(string request)
        {
            HttpClient client = new HttpClient();
            return client.GetAsync("http://localhost:6119/" + request).Result;
        }

        public class uiReq
        {
            public List<string> activeScreens { get; set; }
        }
        public class gameReq
        {
            public class Player
            {
                public int id { get; set; }
                public string name { get; set; }
                public string type { get; set; }
                public string race { get; set; }
                public string result { get; set; }
            }
            public bool isReplay { get; set; }
            public double displayTime { get; set; }
            public List<Player> players { get; set; }
        }
    }
}
