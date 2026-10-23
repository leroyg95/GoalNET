namespace GoalNET.Calculator
{
    public class MatchCalculator
    {
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
    }
}