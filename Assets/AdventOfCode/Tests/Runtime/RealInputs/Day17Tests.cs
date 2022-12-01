using System.Threading.Tasks;
using AdventOfCode.Days;
using JTuresson.AdventOfCode.AOCClient;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace RealInputs
{
    public class Day17Tests
    {
        private int _day;
        private Day17 _daySolution;
        private AdventOfCodeClient _client;

        [SetUp]
        public void Setup()
        {
            var settings = AdventOfCodeSettings.Instance;
            var cache = settings.GetCache();
            _client = new AdventOfCodeClient(settings, cache);
            _daySolution = new Day17();
            _day = 17;
        }

        [Test]
        public async Task PuzzleA()
        {
            // Arrange
            var expectedResult = "";
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
            var expectedResult = "";
            var input = await _client.LoadDayInput(_day);

            // Act
            var result = _daySolution.PuzzleB(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}