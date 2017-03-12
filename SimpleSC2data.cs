using System;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Timers;
using System.IO;

namespace SimpleSC2data
{
    public partial class simplesc2data : Form
    {
        public simplesc2data()
        {
            InitializeComponent();
            System.Timers.Timer updateTimer = new System.Timers.Timer();
            updateTimer.Elapsed += new System.Timers.ElapsedEventHandler(updateData);
            updateTimer.Interval = 1000;
            updateTimer.Enabled = true;
            l_filepath.Text = Environment.CurrentDirectory + "\\Players.txt";
        }

        private void updateData(object sender, ElapsedEventArgs e)
        {
            SC2API sc2API = new SC2API();

            //SC2 Game State
            HttpResponseMessage sc2response = sc2API.runRequest("game");
            if (sc2response.IsSuccessStatusCode)
            {
                string sc2result = sc2response.Content.ReadAsStringAsync().Result;
                SC2API.gameReq gameResult = JsonConvert.DeserializeObject<SC2API.gameReq>(sc2result);
                if (gameResult.players.Count == 2)
                {
                    File.WriteAllText(l_filepath.Text, "(" + gameResult.players[0].race[0] + ")" + gameResult.players[0].name + " vs. (" + gameResult.players[1].race[0] + ")" + gameResult.players[1].name);
                }
                else if (gameResult.players.Count == 4)
                {
                    File.WriteAllText(l_filepath.Text, gameResult.players[0].name + "/" + gameResult.players[1].name + " vs. " + gameResult.players[2].name + "/" + gameResult.players[3].name);
                }
            }
        }
    }
}
