using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class snake : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Segment;
    public int snakeLength;
    public GameObject head;
    private GameObject beheaded;
    public List<GameObject> body = new List<GameObject>();

    public Vector3 dir = new Vector3(-1, 0, 0);

    public Vector3 lastDir = new Vector3(0, 0, 0);

    public Agent agent;

    void Start()
    {
        agent = new Agent();
        beheaded = new GameObject("beheaded");
        beheaded.transform.SetParent(transform);
    }

    public void init()
    {
        head = Instantiate(Segment);
        head.transform.SetParent(transform);
        for (int i = 0; i < snakeLength; i++)
        {
            body.Add(Instantiate(Segment));
            body[i].transform.SetParent(transform);
            body[i].transform.position = head.transform.position + new Vector3(i + 1, 0, 0);
        }
    }
    void wasdInputs()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (lastDir != new Vector3(-1, 0, 0))
            {
                dir = new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (lastDir != new Vector3(1, 0, 0))
            {
                dir = new Vector3(-1, 0, 0);
            }

        }

        if (Input.GetKeyDown(KeyCode.W))

        {
            if (lastDir != new Vector3(0, -1, 0))
            {
                dir = new Vector3(0, 1, 0);
            }

        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (lastDir != new Vector3(0, 1, 0))
            {
                dir = new Vector3(0, -1, 0);
            }

        }
    }
    void updateBody()
    {
        Vector3 pos = head.transform.position;
        foreach (GameObject i in body)
        {
            Vector3 newpos = i.transform.position;
            i.transform.position = pos;
            pos = newpos;
        }
    }
    void updateSnake()
    {
        updateBody();
        head.transform.position += dir;
        lastDir = dir;
        eatSelf();
        eatApple();
    }
    void grow()
    {
        GameObject newSegment = Instantiate(Segment);
        newSegment.transform.SetParent(transform);
        newSegment.transform.position = head.transform.position;
        body.Add(newSegment);
    }
    void eatApple()
    {
        if (transform.parent.GetComponent<env>().appleSpawner.GetComponent<AppleSpawner>().apple.transform.position == head.transform.position)
        {
            Destroy(transform.parent.GetComponent<env>().appleSpawner.GetComponent<AppleSpawner>().apple);
            transform.parent.GetComponent<env>().appleSpawner.GetComponent<AppleSpawner>().spawnApple(head, body);
            grow();
        }
    }
    void eatSelf()
    {
        int cut = 0;
        for (int i = 0; i < body.Count; i++)
        {
            if (body[i].transform.position == head.transform.position)
            {
                cut = i;
            }
        }
        if (cut > 0)
        {
            cutSnake(cut);
        }
    }
    void cutSnake(int cut)
    {
        Debug.Log("cut");
        List<GameObject> newBody = new List<GameObject>();
        for (int i = 0; i < body.Count; i++)
        {
            if (i < cut)
            {
                newBody.Add(body[i]);
            }
            else if (i == cut)
            {
                Destroy(body[i]);
            }
            else
            {
                body[i].transform.SetParent(beheaded.transform);
            }
        }

        body = newBody;
        // spawnBaby();
    }
    void spawnBaby()
    {
        GameObject newSnake = Instantiate(transform.parent.GetComponent<env>().snake);
        newSnake.transform.SetParent(transform.parent);

        newSnake.GetComponent<snake>().head = beheaded.transform.GetChild(0).gameObject;
        newSnake.GetComponent<snake>().dir = -dir;
        beheaded.transform.GetChild(0).transform.SetParent(newSnake.transform);

        foreach (Transform child in beheaded.transform)
        {
            newSnake.GetComponent<snake>().body.Add(child.gameObject);
            child.transform.SetParent(newSnake.transform);
        }
    }
    void changeColour(Color c, List<GameObject> body)
    {
        head.GetComponent<SpriteRenderer>().color = c;
        foreach (GameObject i in body)
        {
            i.GetComponent<SpriteRenderer>().color = c;
        }
    }
    void ai()
    {
        dir = agent.agent(transform.parent.GetComponent<env>().appleSpawner.GetComponent<AppleSpawner>().apple.transform.position, this);

    }

    int frame = 0;
    void Update()
    {

        wasdInputs();
        frame++;
        if (frame == env.speed)
        {
            ai();
            frame = 0;
            updateSnake();
        }
    }
}
