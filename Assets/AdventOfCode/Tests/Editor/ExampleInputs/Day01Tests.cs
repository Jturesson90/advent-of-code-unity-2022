using AdventOfCode.Days;
using NUnit.Framework;

namespace ExampleInputs
{
    public class Day01Tests
    {
        [TestCase("1000\n2000\n3000\n\n4000\n\n5000\n6000\n\n7000\n8000\n9000\n\n10000", "24000")]
        public void PuzzleA(string input, string expectedResult)
        {
            // Arrange
            var day = new Day01();

            // Act
            var result = day.PuzzleA(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("1000\n2000\n3000\n\n4000\n\n5000\n6000\n\n7000\n8000\n9000\n\n10000", "45000")]
        public void PuzzleB(string input, string expectedResult)
        {
            // Arrange
            var day = new Day01();

            // Act
            var result = day.PuzzleB(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}