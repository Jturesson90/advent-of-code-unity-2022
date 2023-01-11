using System.Threading.Tasks;
using AdventOfCode.Days;
using JTuresson.AdventOfCode.AOCClient;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace RealInputs
{
    public class Day10Tests
    {
        private int _day;
        private Day10 _daySolution;
        private AdventOfCodeClient _client;

        [SetUp]
        public void Setup()
        {
            var settings = AdventOfCodeSettings.Instance;
            var cache = settings.GetCache();
            _client = new AdventOfCodeClient(settings, cache);
            _daySolution = new Day10();
            _day = 10;
        }

        [Test]
        public async Task PuzzleA()
        {
            // Arrange
            var expectedResult = "13920";
            var input = await _client.LoadDayInput(_day);

            // Act
            var result = _daySolution.PuzzleA(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task PuzzleB()
        {
            // Arrange
            const string expectedResult =
                "####..##..#....#..#.###..#....####...##.\n#....#..#.#....#..#.#..#.#....#.......#.\n###..#....#....####.###..#....###.....#.\n#....#.##.#....#..#.#..#.#....#.......#.\n#....#..#.#....#..#.#..#.#....#....#..#.\n####..###.####.#..#.###..####.#.....##..";
            var input = await _client.LoadDayInput(_day);

            // Act
            var result = _daySolution.PuzzleB(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}