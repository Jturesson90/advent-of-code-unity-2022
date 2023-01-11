using AdventOfCode.Days;
using NUnit.Framework;

namespace ExampleInputs
{
    public class Day05Tests
    {
        [TestCase(
            "    [D]    \n[N] [C]    \n[Z] [M] [P]\n1   2   3 \n\nmove 1 from 2 to 1\nmove 3 from 1 to 3\nmove 2 from 2 to 1\nmove 1 from 1 to 2",
            "CMZ")]
        public void PuzzleA(string input, string expectedResult)
        {
            // Arrange
            var day = new Day05();

            // Act
            var result = day.PuzzleA(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }


        public void PuzzleB(string input, string expectedResult)
        {
            // Arrange
            var day = new Day05();

            // Act
            var result = day.PuzzleB(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}