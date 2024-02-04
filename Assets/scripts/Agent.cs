using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class Agent : MonoBehaviour
{
    public Vector3 agent(Vector3 apple, snake snake)
    {
        Vector3 head = snake.head.transform.position;
        List<GameObject> body = snake.body;
        Vector3 dir = apple - head;
        dir = greaterThan(dir);
        if (dir.x != 0 && dir.y != 0)
        {
            dir.y = 0;
        }
        return dir;
    }
    Vector3 greaterThan(Vector3 dir)
    {
        return new Vector3(reduceTo1(dir.x), reduceTo1(dir.y), reduceTo1(dir.z));
    }
    float reduceTo1(float n)
    {
        if (n < 0)
        {
            n = -1;
        }
        if (n > 0)
        {
            n = 1;
        }
        if (n == 0)
        {
            n = 0;
        }
        return n;
    }
    (float, float, float, float) getBounds(List<GameObject> body)
    {
        Vector3 min = body[0].transform.position;
        Vector3 max = body[0].transform.position;

        foreach (GameObject i in body)
        {
            if (i.transform.position.x < min.x)
            {
                min.x = i.transform.position.x;
            }
            if (i.transform.position.y < min.y)
            {
                min.y = i.transform.position.y;
            }
            if (i.transform.position.x > max.x)
            {
                max.x = i.transform.position.x;
            }
            if (i.transform.position.y > max.y)
            {
                max.y = i.transform.position.y;
            }
        }
        return (0, 0, 0, 0);
    }
}
