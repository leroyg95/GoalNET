namespace GoalNET.Calculator
{
    public class MatchCalculator
    {
        /// <summary>
        /// Determines the match outcome based on the number of goals scored by the home and away teams.
        /// </summary>
        /// <param name="homeGoals">The number of goals scored by the home team.</param>
        /// <param name="awayGoals">The number of goals scored by the away team.</param>
        /// <returns>A value indicating whether the home team won, the away team won, or the match was a draw.</returns>
        /// <exception cref="ArgumentException">Thrown when either homeGoals or awayGoals is negative.</exception>
        public static MatchOutcome CalculateMatchScore(int homeGoals, int awayGoals)
        {
            if (homeGoals < 0 || awayGoals < 0)
                throw new ArgumentException("Goals cannot be negative.");

            if (homeGoals > awayGoals)
                return MatchOutcome.HomeWin;
            else if (awayGoals > homeGoals)
                return MatchOutcome.AwayWin;
            else
                return MatchOutcome.Draw;
        }

        /// <summary>
        /// Generates a simple form string from a collection of recent matches (e.g., "W-D-L-W-W").
        /// </summary>
        public static string GetFormGuide(IEnumerable<MatchResult> recentMatches, string teamName)
        {
            var results = recentMatches.Select(m =>
            {
                bool isHome = m.HomeTeam.Equals(teamName, StringComparison.OrdinalIgnoreCase);
                var matchResult = CalculateMatchScore(m.HomeGoals, m.AwayGoals);

                if (matchResult == MatchOutcome.Draw) return "D";
                if (isHome && matchResult == MatchOutcome.HomeWin) return "W";
                if (!isHome && matchResult == MatchOutcome.AwayWin) return "W";
                return "L";
            });

            return string.Join("-", results);
        }

        /// <summary>
        /// Generates a ranking table based on a collection of match results.
        /// </summary>
        /// <param name="matchResults">A collection of match results to process.</param>
        /// <returns>A ranking table reflecting the aggregated results of the provided matches.</returns>
        public static RankingTable CalculateRankingTable(IEnumerable<MatchResult> matchResults)
        {
            var rankingTable = new RankingTable();
            foreach (var match in matchResults)
            {
                UpdateTeamStats(rankingTable, match.HomeTeam, match.HomeGoals, match.AwayGoals);
                UpdateTeamStats(rankingTable, match.AwayTeam, match.AwayGoals, match.HomeGoals);
            }

            rankingTable.Entries.Sort((a, b) =>
            {
                int pointComparison = b.Points.CompareTo(a.Points);
                if (pointComparison != 0) return pointComparison;
                int goalDiffComparison = b.GoalDifference.CompareTo(a.GoalDifference);
                if (goalDiffComparison != 0) return goalDiffComparison;
                return string.Compare(a.TeamName, b.TeamName, StringComparison.OrdinalIgnoreCase);
            });

            return rankingTable;
        }

        /// <summary>
        /// Updates the specified team's points and goal difference in the ranking table based on the match result.
        /// </summary>
        /// <param name="rankingTable">The ranking table containing team entries.</param>
        /// <param name="homeTeam">The name of the home team to update.</param>
        /// <param name="homeGoals">The number of goals scored by the home team.</param>
        /// <param name="awayGoals">The number of goals scored by the away team.</param>
        private static void UpdateTeamStats(RankingTable rankingTable, string homeTeam, int homeGoals, int awayGoals)
        {
            var teamEntry = rankingTable.Entries.FirstOrDefault(e => e.TeamName == homeTeam);
            if (teamEntry == null)
            {
                teamEntry = new TableEntry(homeTeam, 0, 0);
                rankingTable.Entries.Add(teamEntry);
            }
            teamEntry.GoalDifference += (homeGoals - awayGoals);
            if (homeGoals > awayGoals)
                teamEntry.Points += 3; // Win
            else if (homeGoals == awayGoals)
                teamEntry.Points += 1; // Draw
        }
    }
}