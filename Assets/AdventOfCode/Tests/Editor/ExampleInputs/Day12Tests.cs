using AdventOfCode.Days;
using NUnit.Framework;

namespace ExampleInputs
{
    public class Day12Tests
    {
        [TestCase("Sabqponm\nabcryxxl\naccszExk\nacctuvwj\nabdefghi", "31")]
        public void PuzzleA(string input, string expectedResult)
        {
            // Arrange
            var day = new Day12();

            // Act
            var result = day.PuzzleA(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("1234", "4321")]
        [TestCase("1234", "4321")]
        [TestCase("1234", "4321")]
        public void PuzzleB(string input, string expectedResult)
        {
            // Arrange
            var day = new Day12();

            // Act
            var result = day.PuzzleB(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}