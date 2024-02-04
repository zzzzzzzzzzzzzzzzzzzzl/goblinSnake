using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class env : MonoBehaviour
{
    // Start is called before the first frame update

    public static int speed = 200;
    public GameObject appleSpawner;
    public GameObject snake;


    void Start()
    {
        appleSpawner = Instantiate(appleSpawner);
        snake = Instantiate(snake);
        snake.GetComponent<snake>().init();
        appleSpawner.transform.SetParent(transform);
        snake.transform.SetParent(transform);
    }
    int frame = 0;
    void Update()
    {
    }
}
