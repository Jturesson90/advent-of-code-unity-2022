using AdventOfCode.Days;
using NUnit.Framework;

namespace ExampleInputs
{
    public class Day13Tests
    {
        [TestCase("[1,1,3,1,1]\n[1,1,5,1,1]", "1")]
        [TestCase("[[1],[2,3,4]]\n[[1],4]", "1")]
        [TestCase("[9]\n[[8,7,6]]", "0")]
        [TestCase("[[4,4],4,4]\n[[4,4],4,4,4]", "1")]
        [TestCase("[7,7,7,7]\n[7,7,7]", "0")]
        [TestCase("[]\n[3]", "1")]
        [TestCase("[[[]]]\n[[]]", "0")]
        [TestCase("[1,[2,[3,[4,[5,6,7]]]],8,9]\n[1,[2,[3,[4,[5,6,0]]]],8,9]", "0")]
        [TestCase(
            "[1,1,3,1,1]\n[1,1,5,1,1]\n\n[[1],[2,3,4]]\n[[1],4]\n\n[9]\n[[8,7,6]]\n\n[[4,4],4,4]\n[[4,4],4,4,4]\n\n[7,7,7,7]\n[7,7,7]\n\n[]\n[3]\n\n[[[]]]\n[[]]\n\n[1,[2,[3,[4,[5,6,7]]]],8,9]\n[1,[2,[3,[4,[5,6,0]]]],8,9]",
            "13")]
        public void PuzzleA(string input, string expectedResult)
        {
            // Arrange
            var day = new Day13();

            // Act
            var result = day.PuzzleA(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(
            "[1,1,3,1,1]\n[1,1,5,1,1]\n\n[[1],[2,3,4]]\n[[1],4]\n\n[9]\n[[8,7,6]]\n\n[[4,4],4,4]\n[[4,4],4,4,4]\n\n[7,7,7,7]\n[7,7,7]\n\n[]\n[3]\n\n[[[]]]\n[[]]\n\n[1,[2,[3,[4,[5,6,7]]]],8,9]\n[1,[2,[3,[4,[5,6,0]]]],8,9]",
            "140")]
        public void PuzzleB(string input, string expectedResult)
        {
            // Arrange
            var day = new Day13();

            // Act
            var result = day.PuzzleB(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}