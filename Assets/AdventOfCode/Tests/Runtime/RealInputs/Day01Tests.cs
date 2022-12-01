using System.Threading.Tasks;
using AdventOfCode.Days;
using JTuresson.AdventOfCode.AOCClient;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace RealInputs
{
    public class Day01Tests
    {
        private int _day;
        private Day01 _daySolution;
        private AdventOfCodeClient _client;

        [SetUp]
        public void Setup()
        {
            var settings = AdventOfCodeSettings.Instance;
            var cache = settings.GetCache();
            _client = new AdventOfCodeClient(settings, cache);
            _daySolution = new Day01();
            _day = 1;
        }

        [Test]
        public async Task PuzzleA()
        {
            // Arrange
            var expectedResult = "66306";
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
            var expectedResult = "195292";
            var input = await _client.LoadDayInput(_day);

            // Act
            var result = _daySolution.PuzzleB(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}