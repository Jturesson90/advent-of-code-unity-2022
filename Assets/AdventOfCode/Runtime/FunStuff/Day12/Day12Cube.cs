using System;
using System.Collections;
using System.Collections.Generic;
using AdventOfCode.Days;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Day12Cube : MonoBehaviour
{
    [SerializeField] private TMP_Text weight;
    [SerializeField] private TMP_Text character;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Initialized(Day12.Day12Node node)
    {
        weight.text = node.TentativeDistance == int.MaxValue ? "X" : node.TentativeDistance.ToString();
        character.text = node.Character;
    }

    public void HighLight()
    {
        _renderer.material.color = Color.blue;
    }
}