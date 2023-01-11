using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day17Shape : MonoBehaviour
{
    [SerializeField] private int width = 0;
    [SerializeField] private int height = 0;
    public int Width => width;
    public int Height => height;
    public int Top => (int)(height + transform.position.y);
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}