using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 _startPos;
    private float repeatwidth;
    void Start()
    {
        _startPos = transform.position;
        repeatwidth = GetComponent<BoxCollider2D>().size.x / 2;
    }

    void Update()
    {
        if(transform.position.x < _startPos.x - repeatwidth)
        {
            transform.position = _startPos;
        }
    }
}
