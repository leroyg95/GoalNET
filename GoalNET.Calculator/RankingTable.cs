using System;
using System.Collections.Generic;
using System.Text;

namespace GoalNET.Calculator
{
    public class RankingTable
    {
        public List<TableEntry> Entries { get; private set; } = [];
    }

    public class TableEntry(string teamName, int points, int goalDifference)
    {
        public string TeamName { get; set; } = teamName;
        public int Points { get; set; } = points;
        public int GoalDifference { get; set; } = goalDifference;
    }
}
