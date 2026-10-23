namespace GoalNET.Calculator
{
    /// <summary>
    /// Represents the result of a match between two teams, including team names and goals scored.
    /// </summary>
    /// <param name="HomeTeam">Name of the home team.</param>
    /// <param name="AwayTeam">Name of the away team.</param>
    /// <param name="HomeGoals">Number of goals scored by the home team.</param>
    /// <param name="AwayGoals">Number of goals scored by the away team.</param>
    public record MatchResult(string HomeTeam, string AwayTeam, int HomeGoals, int AwayGoals);
}
