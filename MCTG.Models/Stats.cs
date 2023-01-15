using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTG.Models
{
    public record Stats
    {
        public Guid PlayerId;
        public int elo = 100;
        public String username;
        public int played { get { return wins + looeses + draws; } }
        public int wins = 0;
        public int looeses = 0;
        public int draws = 0;
    }
}