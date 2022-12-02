using AdventOfCode.Days;
using NUnit.Framework;

namespace ExampleInputs
{
    public class Day02Tests
    {
        [TestCase("A Y\nB X\nC Z", "15")]
        public void PuzzleA(string input, string expectedResult)
        {
            // Arrange
            var day = new Day02();

            // Act
            var result = day.PuzzleA(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("A Y\nB X\nC Z", "12")]
        public void PuzzleB(string input, string expectedResult)
        {
            // Arrange
            var day = new Day02();

            // Act
            var result = day.PuzzleB(input);

            // Assert
            Assert.AreEqual(result, expectedResult);
        }
    }
}