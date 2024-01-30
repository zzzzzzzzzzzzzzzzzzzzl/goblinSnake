using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    public GameObject applePrefab;
    public GameObject apple;
    void Start()
    {
        spawnApple(new GameObject(), new List<GameObject>());
    }

    public void spawnApple(GameObject snakeHead, List<GameObject> snakeBody)
    {
        // snakeBody.Add(snakeHead);
        Vector3 pos = randomPos();


        // while (isPosInsideSnake(pos, snakeBody))
        // {
        //     pos = randomPos();
        // }
        apple = Instantiate(applePrefab);
        apple.transform.position = pos;

    }

    Vector3 randomPos()
    {
        return new Vector3(Random.Range(-11, 11), Random.Range(-11, 11), 0);
    }
    bool isPosInsideSnake(Vector3 pos, List<GameObject> snakeBody)
    {
        foreach (GameObject i in snakeBody)
        {
            if (i.transform.position == pos)
            {
                return true;
            }
        }
        return false;
    }
}
