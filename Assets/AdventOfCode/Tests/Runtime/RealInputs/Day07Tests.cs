using System.Threading.Tasks;
using AdventOfCode.Days;
using JTuresson.AdventOfCode.AOCClient;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;

namespace RealInputs
{
    public class Day07Tests
    {
        private int _day;
        private Day07 _daySolution;
        private AdventOfCodeClient _client;

        [SetUp]
        public void Setup()
        {
            var settings = AdventOfCodeSettings.Instance;
            var cache = settings.GetCache();
            _client = new AdventOfCodeClient(settings, cache);
            _daySolution = new Day07();
            _day = 7;
        }

        [Test]
        public async Task PuzzleA()
        {
            // Arrange
            var expectedResult = "1427048";
            var input = await _client.LoadDayInput(_day);
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            // Act
            var result = _daySolution.PuzzleA(input);
            watch.Stop();
            Debug.Log($"Day07Tests A - {watch.ElapsedMilliseconds}ms");
            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task PuzzleB()
        {
            // Arrange
            var expectedResult = "2940614";
            var input = await _client.LoadDayInput(_day);

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            // Act
            var result = _daySolution.PuzzleB(input);
            watch.Stop();
            Debug.Log($"Day07Tests B - {watch.ElapsedMilliseconds}ms");
            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}