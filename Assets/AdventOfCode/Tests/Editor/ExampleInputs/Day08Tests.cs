using AdventOfCode.Days;
using NUnit.Framework;

namespace ExampleInputs
{
    public class Day08Tests
    {
        [TestCase("30373\n25512\n65332\n33549\n35390", "21")]
        [TestCase(
            "301201331333030024002151134115545441525510053410321024643223304253251423014345311431413433341211111\n210313031411030140245452501003514242514125366210551353634546632015232323540022212401412031113310333",
            "198")]
        [TestCase(
            "222\n212\n222",
            "8")]
        public void PuzzleA(string input, string expectedResult)
        {
            // Arrange
            var day = new Day08();

            // Act
            var result = day.PuzzleA(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("30373\n25512\n65332\n33549\n35390", "8")]
        public void PuzzleB(string input, string expectedResult)
        {
            // Arrange
            var day = new Day08();

            // Act
            var result = day.PuzzleB(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}