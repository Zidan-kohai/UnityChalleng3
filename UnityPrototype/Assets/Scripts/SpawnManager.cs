using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] Obstacles;
    private PlayerController playerController;
    void Start()
    {
        InvokeRepeating("GreateObstacle", 2, 2);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void GreateObstacle()
    {
        if (playerController.GameOver == false)
        {
            int rnd = Random.Range(0, Obstacles.Length);
            Instantiate(Obstacles[rnd], new Vector3(30, 0, 0), Quaternion.identity);
        }

    }
}
