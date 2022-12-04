using AdventOfCode.Days;
using NUnit.Framework;

namespace ExampleInputs
{
    public class Day03Tests
    {
        [TestCase(@"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw", "157")]
        [TestCase("vJrwpWtwJgWrhcsFMMfFFhFp", "16")]
        [TestCase("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", "38")]
        public void PuzzleA(string input, string expectedResult)
        {
            // Arrange
            var day = new Day03();

            // Act
            var result = day.PuzzleA(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("vJrwpWtwJgWrhcsFMMfFFhFp\njqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL\nPmmdzqPrVvPwwTWBwg", "18")]
        [TestCase("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn\nttgJtRGJQctTZtZT\nCrZsJsPPZsGzwwsLwLmpwMDw", "52")]
        [TestCase(
            "vJrwpWtwJgWrhcsFMMfFFhFp\njqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL\nPmmdzqPrVvPwwTWBwg\nwMqvLMZHhHMvwLHjbvcjnnSBnvTQFn\nttgJtRGJQctTZtZT\nCrZsJsPPZsGzwwsLwLmpwMDw",
            "70")]
        public void PuzzleB(string input, string expectedResult)
        {
            // Arrange
            var day = new Day03();

            // Act
            var result = day.PuzzleB(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}