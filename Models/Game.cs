using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerLanParty.Models
{
    public class Game
    {
        public int GameID { get; set; }
        public string GameName { get; set; }

        public GameType GameType { get; set; }
        
    }

    public enum GameType
    {
        RPG, FPS, MOBA, COUCH_COOP
    };
}
