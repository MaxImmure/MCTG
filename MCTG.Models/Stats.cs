using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTG.Models
{
    public struct Stats
    {
        public int elo;
        public int played { get { return wins + looeses; } }
        public int wins;
        public int looeses;

        public Stats()
        {
            wins = 0;
            looeses = 0;
            elo = 100;
        }
    }
}