using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class snake : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Segment;

    public GameObject applePrefab;
    public GameObject appleSpawner;
    public int snakeLength;
    bool alive = true;
    private GameObject head;
    private List<GameObject> body = new List<GameObject>();

    Vector3 dir = new Vector3(-1, 0, 0);

    void Start()
    {
        appleSpawner = Instantiate(appleSpawner);


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
            dir = new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            dir = new Vector3(-1, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            dir = new Vector3(0, 1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            dir = new Vector3(0, -1, 0);
        }
    }
    // Update is called once per frame
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
        eatSelf();
        eatApple();
        // eatSelf();

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
        if (appleSpawner.GetComponent<AppleSpawner>().apple.transform.position == head.transform.position)
        {
            Destroy(

             appleSpawner.GetComponent<AppleSpawner>().apple
            );
            appleSpawner.GetComponent<AppleSpawner>().spawnApple(head, body);
            grow();


        }
    }
    void eatSelf()
    {
        foreach (GameObject i in body)
        {
            if (i.transform.position == head.transform.position)
            {
                changeColour(new Color(0, 1f, 0, .5f));
            }
        }
    }
    void changeColour(Color c)
    {
        head.GetComponent<SpriteRenderer>().color = c;

        foreach (GameObject i in body)
        {
            i.GetComponent<SpriteRenderer>().color = c;
        }
    }

    int frame = 0;
    void Update()
    {
        wasdInputs();
        frame++;
        if (frame == 120)
        {
            Debug.Log("here");
            frame = 0;
            updateSnake();
        }
    }
}
