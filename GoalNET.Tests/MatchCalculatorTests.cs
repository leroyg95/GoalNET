using GoalNET.Calculator;

namespace GoalNET.Tests
{
    public class MatchCalculatorTests
    {
        [Fact]
        public void CalculateMatchScore_HomeWin()
        {
            var result = MatchCalculator.CalculateMatchScore(1, 0);
            Assert.Equal(MatchOutcome.HomeWin, result);
        }

        [Fact]
        public void CalculateMatchScore_AwayWin()
        {
            var result = MatchCalculator.CalculateMatchScore(0, 1);
            Assert.Equal(MatchOutcome.AwayWin, result);
        }

        [Fact]
        public void CalculateMatchScore_Draw()
        {
            var result = MatchCalculator.CalculateMatchScore(1, 1);
            Assert.Equal(MatchOutcome.Draw, result);
        }

        [Fact]
        public void Not_Allow_Negative_Goals()
        {
            Assert.Throws<ArgumentException>(() => MatchCalculator.CalculateMatchScore(-1, 0));
            Assert.Throws<ArgumentException>(() => MatchCalculator.CalculateMatchScore(0, -1));
            Assert.Throws<ArgumentException>(() => MatchCalculator.CalculateMatchScore(-1, -1));
        }

        [Fact]
        public void GetFormGuide_Returns_Correct_Form()
        {
            var recentMatches = new List<MatchResult>
            {
                new("TeamA", "TeamB", 2, 1), // W
                new("TeamA", "TeamC", 1, 1), // D
                new("TeamD", "TeamA", 3, 0), // L
                new("TeamA", "TeamE", 4, 2), // W
                new("TeamF", "TeamA", 0, 1)  // W
            };
            var formGuide = MatchCalculator.GetFormGuide(recentMatches, "TeamA");
            Assert.Equal("W-D-L-W-W", formGuide);
        }

        [Fact]
        public void CalculateRankingTable_Returns_Correct_Ranking()
        {
            var matchResults = new List<MatchResult>
            {
                new("TeamA", "TeamB", 2, 1),
                new("TeamA", "TeamC", 1, 1),
                new("TeamD", "TeamA", 3, 0),
                new("TeamA", "TeamE", 4, 2),
                new("TeamF", "TeamA", 0, 1)
            };
            var rankingTable = MatchCalculator.CalculateRankingTable(matchResults);
            Assert.NotNull(rankingTable);
            Assert.Equal(6, rankingTable.Entries.Count);
            Assert.Equal("TeamA", rankingTable.Entries.First().TeamName);
        }
    }
}
