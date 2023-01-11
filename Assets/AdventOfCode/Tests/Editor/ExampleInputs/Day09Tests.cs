using AdventOfCode.Days;
using NUnit.Framework;

namespace ExampleInputs
{
    public class Day09Tests
    {
        [TestCase("R 4\nU 4\nL 3\nD 1\nR 4\nD 1\nL 5\nR 2", "13")]
        public void PuzzleA(string input, string expectedResult)
        {
            // Arrange
            var day = new Day09();

            // Act
            var result = day.PuzzleA(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("R 4\nU 4\nL 3\nD 1\nR 4\nD 1\nL 5\nR 2", "1")]
        public void PuzzleB(string input, string expectedResult)
        {
            // Arrange
            var day = new Day09();

            // Act
            var result = day.PuzzleB(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}