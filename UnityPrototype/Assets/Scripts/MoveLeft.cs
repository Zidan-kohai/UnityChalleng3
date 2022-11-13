using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _leftBound;
    private PlayerController _controller;
    void Start()
    {
        _controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (_controller.GameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
        }
        if (transform.position.x < _leftBound && gameObject.CompareTag("Obstacle")){
            Destroy(gameObject);
        }
    }
}
