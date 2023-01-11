using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Days;
using JTuresson.AdventOfCode.AOCClient;
using TMPro;
using UnityEngine;

public class Day05Controller : MonoBehaviour
{
    [SerializeField] private GameObject crate;
    [SerializeField] private string answer;
    [SerializeField] private ParticleSystem particles;
    private Day05.PuzzleACommandBuffer dayBuffer;
    private Board _board;

    async void Awake()
    {
        var client = new AdventOfCodeClient(AdventOfCodeSettings.Instance, AdventOfCodeSettings.Instance.GetCache());
        var input = await client.LoadDayInput(5);
        var day = new Day05();
        answer = day.PuzzleA(input);
        _board = new Board(day.PuzzleABuffer.Width, 50);
        dayBuffer = day.PuzzleABuffer;
        StartWithCommands(day.PuzzleABuffer);
    }

    private void StartWithCommands(Day05.PuzzleACommandBuffer buffer)
    {
        for (int x = 0; x < buffer.Start.Count; x++)
        {
            var s = buffer.Start[x];
            int y = 0;
            foreach (var c in s)
            {
                var cra = Instantiate(crate, new Vector3(x, y, 0), transform.rotation, transform);

                _board.Add(x, cra.transform);
                var text = cra.GetComponentInChildren<TMP_Text>();
                if (text)
                {
                    text.text = c.ToString();
                }

                y++;
            }
        }

        StartCoroutine(PlayGame());
    }

    private IEnumerator PlayGame()
    {
        for (var i = 0; i < dayBuffer.Commands.Count; i++)
        {
            var command = dayBuffer.Commands[i];
            for (int x = 0; x < command.Amount; x++)
            {
                yield return new WaitForEndOfFrame();
                var t = _board.GetTransform(command.From - 1);
                _board.Move(command.From - 1, command.To - 1);
                if (t)
                    t.localPosition = _board.GetStackTopPosition(command.To - 1);
                // posFrom.position = posTo;
            }
        }

        yield return new WaitForSeconds(1f);
        var crates = _board.GetAllExceptTop();
        for (var i = crates.Count - 1; i >= 0; i--)
        {
            var crate = crates[i];

            var newPos = crate.localPosition;
            newPos.x += UnityEngine.Random.value * 0.5f;
            newPos.y += UnityEngine.Random.value * 0.5f;
            var par = Instantiate(particles, newPos, Quaternion.identity);
            Destroy(crate.gameObject);
        }
    }

    private class Board
    {
        [SerializeField] private List<Stack<Transform>> board;
        private Dictionary<Vector2Int, Transform> _tileByPos;

        public Board(int width, int height)
        {
            board = new List<Stack<Transform>>(width);
            _tileByPos = new Dictionary<Vector2Int, Transform>();
            for (int i = 0; i < board.Capacity; i++)
            {
                board.Add(new Stack<Transform>());
            }
        }

        public void Add(int x, Transform t)
        {
            var newY = board[x].Count;
            var key = new Vector2Int(x, newY);
            if (!_tileByPos.ContainsKey(key))
            {
                board[x].Push(t);
                _tileByPos.Add(key, t);
                t.localPosition = new Vector3(x, newY, t.localPosition.z);
            }
        }

        public List<Transform> GetAllExceptTop()
        {
            var result = _tileByPos.Values.ToList();

            for (int i = 0; i < board.Count; i++)
            {
                Transform t = GetTransform(i);
                if (t != null)
                {
                    result.Remove(t);
                }
            }

            return result;
        }

        public Vector3 GetStackTopPosition(int x)
        {
            return new Vector3(x, board[x].Count - 1, 0);
        }

        public Vector2Int GetFirstAvailableKey(int x)
        {
            return new Vector2Int(x, board[x].Count);
        }

        public Vector2Int GetKey(int x)
        {
            return new Vector2Int(x, board[x].Count - 1);
        }

        public Transform GetTransform(int x)
        {
            var key = GetKey(x);
            if (_tileByPos.ContainsKey(key))
            {
                return _tileByPos[key];
            }
            else
            {
                Debug.LogError("HJÄLPE");
                return null;
            }
        }

        public void Move(int x, int xx)
        {
            var key = GetKey(x);
            var newKey = GetFirstAvailableKey(xx);
            if (_tileByPos.ContainsKey(key) && !_tileByPos.ContainsKey(newKey))
            {
                Transform t = board[x].Pop();
                board[xx].Push(t);
                Transform tt = _tileByPos[key];
                _tileByPos.Remove(key);
                _tileByPos.Add(newKey, tt);
                if (t != tt) Debug.LogError("Hjälpe");
            }
        }
    }
}