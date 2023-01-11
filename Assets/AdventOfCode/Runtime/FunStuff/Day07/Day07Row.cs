using System;
using System.Collections;
using System.Collections.Generic;
using AdventOfCode.Days;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Day07Row : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text sizeText;

    [SerializeField] private Image highlighter;

    [SerializeField] public Color hoverColor;
    [SerializeField] public Color selectedColor;

    [SerializeField] public Color fileColor;
    [SerializeField] public Color folderColor;
    private int _depth;
    private RowState _currentState = RowState.None;

    public enum RowState
    {
        Hover,
        Selected,
        None
    }

    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private bool isFolder = false;
    private Day07.Folder folder;
    private string space = "";

    public void Initialize(Day07.Folder folder)
    {
        this.folder = folder;
        isFolder = true;
        for (int i = 0; i < folder.depth; i++)
        {
            space += "   ";
        }

        nameText.color = folderColor;
        sizeText.color = Color.white;
        nameText.text = space + "> " + folder.name;

        sizeText.text = $"Total: {folder.GetSize()}";
        highlighter.enabled = false;
    }

    public void Open()
    {
        if (gameObject.activeInHierarchy) return;
        gameObject.SetActive(true);
        if (isFolder)
        {
            nameText.text = space + "v " + folder.name;
        }
    }

    public void Initialize(Day07.Files file)
    {
        for (int i = 0; i < file.parent.depth + 1; i++)
        {
            space += "   ";
        }

        nameText.color = fileColor;
        sizeText.color = fileColor;
        nameText.text = space + file.name;
        sizeText.text = file.size.ToString();
        highlighter.enabled = false;
    }


    public void Close()
    {
        if (_currentState == RowState.Selected)
        {
            nameText.text = folder.name;
            return;
        }

        if (!gameObject.activeInHierarchy) return;
        gameObject.SetActive(false);
        if (isFolder)
        {
            nameText.text = space + "> " + folder.name;
        }
    }

    public void SetRowState(RowState state)
    {
        if (_currentState == RowState.Selected) return;
        switch (state)
        {
            case RowState.Hover:
                highlighter.enabled = true;
                highlighter.color = hoverColor;
                break;
            case RowState.None:
                highlighter.enabled = false;
                highlighter.color = hoverColor;
                break;
            case RowState.Selected:
                highlighter.enabled = true;
                highlighter.color = selectedColor;
                break;
        }

        _currentState = state;
    }
}