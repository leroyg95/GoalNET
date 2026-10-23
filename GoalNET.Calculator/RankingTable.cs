using System;
using System.Collections.Generic;
using System.Text;

namespace GoalNET.Calculator
{
    public class RankingTable
    {
        public List<TableEntry> Entries { get; private set; } = [];
    }

    public class TableEntry
    {
        public TableEntry(string teamName, int points, int goalDifference)
        {
            TeamName = teamName;
            Points = points;
            GoalDifference = goalDifference;
        }

        public string TeamName { get; set; }
        public int Points { get; set; }
        public int GoalDifference { get; set; }
    }
}
