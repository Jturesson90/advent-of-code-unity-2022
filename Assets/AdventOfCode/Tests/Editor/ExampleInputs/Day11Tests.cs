using AdventOfCode.Days;
using NUnit.Framework;

namespace ExampleInputs
{
    public class Day11Tests
    {
        [TestCase(
            "Monkey 0:\nStarting items: 79, 98\nOperation: new = old * 19\nTest: divisible by 23\n  If true: throw to monkey 2\n  If false: throw to monkey 3\n\nMonkey 1:\nStarting items: 54, 65, 75, 74\nOperation: new = old + 6\nTest: divisible by 19\n  If true: throw to monkey 2\n  If false: throw to monkey 0\n\nMonkey 2:\nStarting items: 79, 60, 97\nOperation: new = old * old\nTest: divisible by 13\n  If true: throw to monkey 1\n  If false: throw to monkey 3\n\nMonkey 3:\nStarting items: 74\nOperation: new = old + 3\nTest: divisible by 17\n  If true: throw to monkey 0\n  If false: throw to monkey 1",
            "10605")]
        public void PuzzleA(string input, string expectedResult)
        {
            // Arrange
            var day = new Day11();

            // Act
            var result = day.PuzzleA(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(
            "Monkey 0:\nStarting items: 79, 98\nOperation: new = old * 19\nTest: divisible by 23\n  If true: throw to monkey 2\n  If false: throw to monkey 3\n\nMonkey 1:\nStarting items: 54, 65, 75, 74\nOperation: new = old + 6\nTest: divisible by 19\n  If true: throw to monkey 2\n  If false: throw to monkey 0\n\nMonkey 2:\nStarting items: 79, 60, 97\nOperation: new = old * old\nTest: divisible by 13\n  If true: throw to monkey 1\n  If false: throw to monkey 3\n\nMonkey 3:\nStarting items: 74\nOperation: new = old + 3\nTest: divisible by 17\n  If true: throw to monkey 0\n  If false: throw to monkey 1",
            "2713310158")]
        public void PuzzleB(string input, string expectedResult)
        {
            // Arrange
            var day = new Day11();

            // Act
            var result = day.PuzzleB(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}