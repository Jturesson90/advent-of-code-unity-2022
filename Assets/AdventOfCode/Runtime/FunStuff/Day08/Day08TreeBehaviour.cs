using System.Collections;
using System.Collections.Generic;
using AdventOfCode.Days;
using UnityEngine;

public class Day08TreeBehaviour : MonoBehaviour
{
    public float scale = 1;

    public void Initialize(Day08.Day08Tree tree)
    {
        var s = 0.5f + (scale * tree.Height);
        transform.localScale *= s;
        if (tree.VisibleFromSide)
            GetComponent<Renderer>().materials[1].color = Color.yellow;
        if (tree.IsBestScore)
            GetComponent<Renderer>().materials[1].color = Color.red;
    }
}