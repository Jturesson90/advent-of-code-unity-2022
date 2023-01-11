using System.Collections;
using System.Collections.Generic;
using AdventOfCode.Days;
using JTuresson.AdventOfCode.AOCClient;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Day07Controller : MonoBehaviour
{
    [SerializeField] private ScrollRect scroll;
    [SerializeField] private RectTransform canvasParent;
    [SerializeField] private TMP_Text selectedText;
    [SerializeField] private Day07Row day07Row;
    [SerializeField] private string answer;
    [SerializeField] private float waitForSeconds = 1f;
    private Dictionary<Day07.Folder, Day07Row> _folderByRectTransform;
    private Dictionary<Day07.Files, Day07Row> _fileByRectTransform;
    [SerializeField] private GameObject console;

    private int count = 0;
    private long totalsize = 0;

    async void Awake()
    {
        var client = new AdventOfCodeClient(AdventOfCodeSettings.Instance, AdventOfCodeSettings.Instance.GetCache());
        var input = await client.LoadDayInput(7);
        var day = new Day07();
        answer = day.PuzzleA(input);
        _folderByRectTransform = new Dictionary<Day07.Folder, Day07Row>();
        _fileByRectTransform = new Dictionary<Day07.Files, Day07Row>();
        StartWithCommands(day.PuzzleACommandsBuffer);
    }

    private WaitForSeconds _wait;

    private void StartWithCommands(Day07.Puzzle7ABuffer buffer)
    {
        var start = buffer.Commands.Commands[0].SelectedFolder;

        AddFolderRecursive(start);
        _wait = new WaitForSeconds(waitForSeconds);
        StartCoroutine(DoCommands(buffer.Commands.Commands));
    }

    private IEnumerator DoCommands(List<Day07.DirCommand> commandsCommands)
    {
        Day07Row current = null;
        foreach (var co in commandsCommands)
        {
            yield return _wait;
            if (_folderByRectTransform.ContainsKey(co.SelectedFolder))
            {
                // scroll.velocity
                var f = _folderByRectTransform[co.SelectedFolder];
                if (current != null)
                    current.SetRowState(Day07Row.RowState.None);
                f.SetRowState(Day07Row.RowState.Hover);
                var s = co.SelectedFolder.GetSize();
                if (s <= 100000)
                {
                    yield return _wait;
                    f.SetRowState(Day07Row.RowState.Selected);
                    count++;
                    totalsize += s;
                    selectedText.text = $"{count} directories selected. Total size: {totalsize}";
                }

                foreach (var v in _folderByRectTransform)
                {
                    v.Value.Close();
                }

                foreach (var v in _fileByRectTransform)
                {
                    v.Value.Close();
                }

                var d = co.SelectedFolder;
                while (d != null)
                {
                    _folderByRectTransform[d].Open();
                    foreach (var v in d.children)
                    {
                        var sa = _folderByRectTransform[v];
                        sa.Open();
                    }

                    foreach (var va in d.files)
                    {
                        var sa = _fileByRectTransform[va];
                        sa.Open();
                    }

                    d = d.parent;
                }

                Canvas.ForceUpdateCanvases();
                current = f;
                scroll.FocusOnItem(current.rectTransform);


/*      
      Canvas.ForceUpdateCanvases();
      var calc = (Vector2) scroll.transform.InverseTransformPoint(canvasParent.position)
                 - (Vector2) scroll.transform.InverseTransformPoint(f.rectTransform.position);
      calc.x = canvasParent.anchoredPosition.x;
      canvasParent.anchoredPosition = calc;*/
            }
        }

        yield return new WaitForSeconds(3);
        console.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void AddFolderRecursive(Day07.Folder folder)
    {
        var ft = Instantiate(day07Row, canvasParent);
        ft.Initialize(folder);

        _folderByRectTransform.Add(folder, ft);
        foreach (var file in folder.files)
        {
            var t = Instantiate(day07Row, canvasParent);
            _fileByRectTransform.Add(file, t);
            t.Initialize(file);
        }

        foreach (var f in folder.children)
        {
            AddFolderRecursive(f);
        }
    }
}