using AdventOfCode.Days;
using NUnit.Framework;

namespace ExampleInputs
{
    public class Day17Tests
    {
        [TestCase(">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>", "3068")]
        public void PuzzleA(string input, string expectedResult)
        {
            // Arrange
            var day = new Day17();

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
            var day = new Day17();

            // Act
            var result = day.PuzzleB(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}