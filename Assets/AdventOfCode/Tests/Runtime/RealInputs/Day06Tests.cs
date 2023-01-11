using System.Threading.Tasks;
using AdventOfCode.Days;
using JTuresson.AdventOfCode.AOCClient;
using NUnit.Framework;
using UnityEngine;

namespace RealInputs
{
    public class Day06Tests
    {
        private int _day;
        private Day06 _daySolution;
        private AdventOfCodeClient _client;

        [SetUp]
        public void Setup()
        {
            var settings = AdventOfCodeSettings.Instance;
            var cache = settings.GetCache();
            _client = new AdventOfCodeClient(settings, cache);
            _daySolution = new Day06();
            _day = 6;
        }

        [Test]
        public async Task PuzzleA()
        {
            // Arrange
            var expectedResult = "1794";
            var input = await _client.LoadDayInput(_day);

            // Act
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            var result = _daySolution.PuzzleA(input);
            watch.Stop();
            Debug.Log($"Day06Tests A - {watch.ElapsedMilliseconds}ms");

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task PuzzleB()
        {
            // Arrange
            var expectedResult = "2851";
            var input = await _client.LoadDayInput(_day);

            // Act
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            var result = _daySolution.PuzzleB(input);
            watch.Stop();
            Debug.Log($"Day06Tests B - {watch.ElapsedMilliseconds}ms");

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}