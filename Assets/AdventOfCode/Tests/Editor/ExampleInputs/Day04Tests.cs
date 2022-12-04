using AdventOfCode.Days;
using NUnit.Framework;

namespace ExampleInputs
{
    public class Day04Tests
    {
        [TestCase("2-4,6-8\n2-3,4-5\n5-7,7-9\n2-8,3-7\n6-6,4-6\n2-6,4-8", "2")]
        public void PuzzleA(string input, string expectedResult)
        {
            // Arrange
            var day = new Day04();

            // Act
            var result = day.PuzzleA(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("2-4,6-8\n2-3,4-5\n5-7,7-9\n2-8,3-7\n6-6,4-6\n2-6,4-8", "4")]
        public void PuzzleB(string input, string expectedResult)
        {
            // Arrange
            var day = new Day04();

            // Act
            var result = day.PuzzleB(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}