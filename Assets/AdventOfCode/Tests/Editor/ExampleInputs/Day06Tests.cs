using AdventOfCode.Days;
using NUnit.Framework;

namespace ExampleInputs
{
    public class Day06Tests
    {
        [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", "7")]
        [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", "5")]
        [TestCase("nppdvjthqldpwncqszvftbrmjlhg", "6")]
        [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", "10")]
        [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", "11")]
        public void PuzzleA(string input, string expectedResult)
        {
            // Arrange
            var day = new Day06();

            // Act
            var result = day.PuzzleA(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }


        public void PuzzleB(string input, string expectedResult)
        {
            // Arrange
            var day = new Day06();

            // Act
            var result = day.PuzzleB(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}