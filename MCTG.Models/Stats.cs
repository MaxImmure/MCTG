using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTG.Models
{
    public struct Stats
    {
        public int elo = 100;
        public int played { get { return wins + looeses; } }
        public int wins = 0;
        public int looeses = 0;

        public Stats() { }
    }
}