using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTG.Models
{
    public struct Stats
    {
        public int elo;
        public int played { get { return wins + looeses + draws; } }
        public int wins;
        public int looeses;
        public int draws;

        public Stats()
        {
            wins = 0;
            looeses = 0;
            draws = 0;
            elo = 100;
        }
    }
}