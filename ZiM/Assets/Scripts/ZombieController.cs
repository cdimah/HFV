using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{

    bool onUBorder = false;        //Used to detect if in Upper boarder.
    bool onRBorder = false;        //Used to detect if in Right boarder.
    bool onDBorder = false;        //Used to detect if in Lower boarder.
    bool onLBorder = false;        //Used to detect if in Upper boarder.
    bool moveByHand = false;       //Bool to separate automatic move of player move.
    bool canMove = false;          //Indicator of player clicked.
    bool destinationSet = false;   //Destination point is set.
    bool attacking = false;        //Used to know if the zombie is attacking.
    float speed = 4f;              //Initial speed of the player.
    float checkWait;               //Seconds the player will wait before cecking if in area.
    float damageWait;              //Seconds the player will wait  to make damage.
    float strength = 2;            //Variable used to define how much damage will deal.
    float setPositionX;            //Variable to calculate position X inside the circle.
    float setPositionY;            //Variable to calculate position Y inside the circle.
    float zRef;                    //Variable used to assign y axis value to z axis.
    float distToLeader;            //Distance to calculate how far away from leader and calculate speed.
    float distToBystander;         //Distance to calculate how far away from bystander.
    float maxLDist = 8;            //Limit of how far from the leader the zombie will go.
    float maxBDist = 4;            //Distace at which the zombie will atemp to attack a bystander.
    float slowingDist = 6;         //Distance at which the zombie will start to slow down.
    Vector3 targetPosition;        //Destination of the movement.
    Vector2 leadRef;               //Reference to go to Leader or Bystander.
    Collider2D coll;           //Colider for interactions.
    GameObject Leader;              //GameObject to calculate distance between the zombie and the leader.
    GameObject closestBystander = null;    //GameObject to calculate distance between the zombie and closest bystander.
    GameObject enemy;              //Bystander that is being attacked.

    public float Healthpoints;      //Remaining life of the character.
    public float MaxHealthpoints = 10;      //Maximum healtpoints of the character.
    public HealthBarController Healthbar;    //Declaration of the Healthbar of this character.

    void Start()
    {
        Healthpoints = MaxHealthpoints;
        Healthbar.SetHealth(Healthpoints, MaxHealthpoints);
        coll = GetComponent<Collider2D>();     //Create collider for all interactions
        Leader = GameObject.Find("ZombieLeader");
        StartCoroutine("AreaCheck");
        targetPosition = transform.position;
        zRef = targetPosition.y;
        targetPosition.z = zRef;
        transform.position = targetPosition;

    }


    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    //Register click on player
        if (Input.GetMouseButtonDown(0))
        {
            if (coll == Physics2D.OverlapPoint(mousePos))
            {
                canMove = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && canMove == true)
        {
            Vector2 refPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);     //Register the destination point by using a Vector2 variable to avoid changinf z value
            targetPosition = refPos;
            moveByHand = true;
            destinationSet = true;
            canMove = false;

        }

        if (destinationSet == true)                                                 //Begin movement if everything is ready
        {
            Turn();
            Moving();

        }

    }

    void Moving()
    {
        speed = 4;
        if (Leader != null)
        {
            if (moveByHand == true)
            {
                distToLeader = Vector2.Distance(this.transform.position, Leader.transform.position);
                if (distToLeader >= maxLDist)
                {
                    destinationSet = false;
                }

                if (distToLeader > slowingDist)
                {
                    speed = speed * ((maxLDist - distToLeader) / (maxLDist - slowingDist));
                    if (speed < 1)
                    {
                        speed = 1;
                    }
                }
            }
        }

        CheckBorders();
        zRef = targetPosition.y;
        targetPosition.z = zRef;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            canMove = false;
            destinationSet = false;
        }
    }

    void Turn()
    {
        if (transform.position.x != targetPosition.x)               //To avoid turning at begining or moving only on 'y' axis
        {

            if (transform.position.x < targetPosition.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    void CoordinatesCalc()                                          //Function needed to create a randome coordinate inside the leader area 
    {
        int select = Random.Range(1, 5);
        if (select == 1)
        {
            setPositionX = 1f;
            setPositionY = 1f;
        }
        else if (select == 2)
        {
            setPositionX = 1f;
            setPositionY = -1f;
        }
        else if (select == 3)
        {
            setPositionX = -1f;
            setPositionY = 1f;
        }
        else
        {
            setPositionX = -1f;
            setPositionY = -1f;
        }

        leadRef.x = leadRef.x + (Random.Range(0f, 4.5f) * setPositionX);
        leadRef.y = leadRef.y + (Random.Range(0f, 3f) * setPositionY);
        targetPosition = leadRef;
        moveByHand = false;
        destinationSet = true;
        canMove = false;

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


    void FindLeaderArea()
    {
        {
            
            if (Leader != null)
            {
                leadRef = GameObject.Find("ZombieLeader").transform.position;
                if (coll.IsTouching(GameObject.FindWithTag("LArea").GetComponent<Collider2D>()))
                {
                    distToLeader = Vector2.Distance(this.transform.position, leadRef);
                    if (distToLeader < 1)
                    {
                        CoordinatesCalc();
                    }
                }
                else
                {
                    CoordinatesCalc();
                }
            }
        }
    }

    GameObject FindClosestBystander()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Bystander");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public void DamageDone (float damage)
    {
        Healthpoints -= damage;
        Healthbar.SetHealth(Healthpoints, MaxHealthpoints);
        if (Healthpoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Attacking()
    {
        if (attacking == true)
        {
            var enemyChar = enemy.GetComponent<BystanderController>();
            enemyChar.DamageDone(strength);
        }
        damageWait = Random.Range(1.5f, 2.5f);
        yield return new WaitForSeconds(damageWait);
        StartCoroutine("Attacking");
    }

    private IEnumerator AreaCheck()                                 //Coroutine to Keep zombies checking if they are inside the Leader area
    {
        checkWait = Random.Range(2.5f, 4f);
        yield return new WaitForSeconds(checkWait);
        if (attacking == false)
        {
            if (destinationSet == false)
            {
                closestBystander = FindClosestBystander();
                if (closestBystander != null)
                {
                    distToBystander = Vector2.Distance(this.transform.position, closestBystander.transform.position);
                    if (distToBystander < maxBDist)
                    {
                        targetPosition = closestBystander.transform.position;
                        moveByHand = false;
                        destinationSet = true;
                    }
                    else if (Leader != null)
                    {
                        FindLeaderArea();
                    }
                }
                else if (Leader != null)
                {
                    FindLeaderArea();
                }
            }
        }
        StartCoroutine("AreaCheck");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject collided = col.gameObject;
        if (collided.name == "BorderU")
        {
            onUBorder = true;
            destinationSet = false;
        }

        if (collided.name == "BorderR")
        {
            onRBorder = true;
            destinationSet = false;
        }

        if (collided.name == "BorderD")
        {
            onDBorder = true;
            destinationSet = false;
        }

        if (collided.name == "BorderL")
        {
            onLBorder = true;
            destinationSet = false;
        }

        if (collided.tag == "Bystander")
        {
            destinationSet = false;
            enemy = collided;
            attacking = true;
            StartCoroutine("Attacking");
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "Bystander")
        {
            enemy = col.gameObject;
            attacking = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bystander")
        {
            enemy = col.gameObject;
            attacking = false;
            StopCoroutine("Attacking");
        }
    }
}