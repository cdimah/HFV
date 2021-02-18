using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieLeaderMovement : MonoBehaviour
{
    bool canMove = false;           //Indicator of destination set.
    bool onUBorder = false;         //Used to detect if in Upper boarder.
    bool onRBorder = false;         //Used to detect if in Right boarder.
    bool onDBorder = false;         //Used to detect if in Lower boarder.
    bool onLBorder = false;         //Used to detect if in Upper boarder.
    public GameObject head;
    public GameObject body;
    public GameObject legs;

    float mansionSizeX;             //Float to know the size of the mansion.
    float height;                   //Float made to save the height of the movement space.
    float width;                    //Float made to save the width of the movement space.
    float minX;                     //Float to determinate the minimum value of X for a destination.
    float maxX;                     //Float to determinate the maximum value of X for a destination.
    float minY;                     //Float to determinate the minimum value of Y for a destination.
    float maxY;                     //Float to determinate the maximum value of Y for a destination.
    float zRef;                     //Variable used to assign y axis value to z axis.
    float speed;                    //Initial speed of the player.
    Vector3 targetPosition;         //Destination of the movement.
    Collider2D coll;                //Colider for interactions.
    SpriteRenderer headSR;
    SpriteRenderer bodySR;
    SpriteRenderer legsSR;

    void Awake()
    {
        mansionSizeX = GameController.mansionSize;
        height = 2 * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        minX = Camera.main.transform.position.x - (width / 2);
        maxX = Camera.main.transform.position.x + mansionSizeX - width / 2;
        minY = 0f;
        maxY = -(height / 2);
    }

    void Start()
    {
        coll = GetComponent<Collider2D>();
        targetPosition = transform.position;
        zRef = targetPosition.y;
        targetPosition.z = zRef;
        transform.position = targetPosition;
        StartCoroutine("Movement");
        gameObject.name = "ZombieLeader";
        headSR = head.GetComponent<SpriteRenderer>();
        bodySR = body.GetComponent<SpriteRenderer>();
        legsSR = legs.GetComponent<SpriteRenderer>();
        LookUpdate();
    }

    void Update()
    {
        if (canMove == true)
        {
            Moving();
            Turn();
        }
    }

    public void LookUpdate()
    {
        if(GameController.head)
        {
            headSR.sprite = GameController.head.GetComponent<SpriteRenderer>().sprite;
        }
        if (GameController.body)
        {
            bodySR.sprite = GameController.body.GetComponent<SpriteRenderer>().sprite;
        }
        if (GameController.legs)
        {
            legsSR.sprite = GameController.legs.GetComponent<SpriteRenderer>().sprite;
        }
    }

    void Moving()
    {
        CheckBorders();
        zRef = targetPosition.y;
        targetPosition.z = zRef;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            canMove = false;
        }
    }

    void Turn()
    {
        if (transform.position.x != targetPosition.x)
        {
            if (transform.position.x < targetPosition.x)
            {
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else
            {
                transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            }
        }
    }

    void Stop()
    {
        targetPosition = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void CheckBorders()
    {
        if (onUBorder == true && targetPosition.y > transform.position.y)
        {
            targetPosition.y = transform.position.y;
        }
        else if (onUBorder == true && targetPosition.y < transform.position.y)
        {
            onUBorder = false;
        }

        if (onRBorder == true && targetPosition.x > transform.position.x)
        {
            targetPosition.x = transform.position.x;
        }
        else if (onRBorder == true && targetPosition.x < transform.position.x)
        {
            onRBorder = false;
        }

        if (onDBorder == true && targetPosition.y < transform.position.y)
        {
            targetPosition.y = transform.position.y;
        }
        else if (onDBorder == true && targetPosition.y > transform.position.y)
        {
            onDBorder = false;
        }

        if (onLBorder == true && targetPosition.x < transform.position.x)
        {
            targetPosition.x = transform.position.x;
        }
        else if (onLBorder == true && targetPosition.x > transform.position.x)
        {
            onLBorder = false;
        }

    }

    private IEnumerator Movement()
    {
        float checkWait = Random.Range(2.5f, 5f);
        yield return new WaitForSeconds(checkWait);
        float setPositionX = Random.Range(minX, maxX);
        float setPositionY = Random.Range(minY, maxY);
        targetPosition = new Vector3(setPositionX, setPositionY, 0);
        speed = Random.Range(1f, 3f);
        canMove = true;
        StartCoroutine("Movement");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject collided = col.gameObject;
        if (collided.name == "BorderU")
        {
            onUBorder = true;
            canMove = false;
        }
        if (collided.name == "BorderR")
        {
            onRBorder = true;
            canMove = false;
        }

        if (collided.name == "BorderD")
        {
            onDBorder = true;
            canMove = false;
        }

        if (collided.name == "BorderL")
        {
            onLBorder = true;
            canMove = false;
        }
    }
}
