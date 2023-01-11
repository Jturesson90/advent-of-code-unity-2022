using AdventOfCode.Days;
using NUnit.Framework;

namespace ExampleInputs
{
    public class Day14Tests
    {
        [TestCase("498,4 -> 498,6 -> 496,6\n503,4 -> 502,4 -> 502,9 -> 494,9", "24")]
        public void PuzzleA(string input, string expectedResult)
        {
            // Arrange
            var day = new Day14();

            // Act
            var result = day.PuzzleA(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("498,4 -> 498,6 -> 496,6\n503,4 -> 502,4 -> 502,9 -> 494,9", "93")]
        public void PuzzleB(string input, string expectedResult)
        {
            // Arrange
            var day = new Day14();

            // Act
            var result = day.PuzzleB(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}