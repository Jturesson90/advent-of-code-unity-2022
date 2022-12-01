using AdventOfCode.Days;
using NUnit.Framework;

namespace ExampleInputs
{
    public class Day08Tests
    {
        [TestCase("1234", "4321")]
        [TestCase("1234", "4321")]
        [TestCase("1234", "4321")]
        public void PuzzleA(string input, string expectedResult)
        {
            // Arrange
            var day = new Day08();

            // Act
            var result = day.PuzzleA(input);

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        [TestCase("1234", "4321")]
        [TestCase("1234", "4321")]
        [TestCase("1234", "4321")]
        public void PuzzleB(string input, string expectedResult)
        {
            // Arrange
            var day = new Day08();

            // Act
            var result = day.PuzzleB(input);

            // Assert
            Assert.AreEqual(result, expectedResult);
        }
    }
}