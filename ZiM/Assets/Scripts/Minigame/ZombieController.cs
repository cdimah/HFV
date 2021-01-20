using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{

    bool onUBorder = false;         //Used to detect if in Upper boarder.
    bool onRBorder = false;         //Used to detect if in Right boarder.
    bool onDBorder = false;         //Used to detect if in Lower boarder.
    bool onLBorder = false;         //Used to detect if in Upper boarder.
    bool moveByHand = false;        //Bool to separate automatic move of player move.
    bool canMove = false;           //Indicator of player clicked.
    bool destinationSet = false;    //Destination point is set.
    bool attacking = false;         //Used to know if the zombie is attacking.
    float speed = 4f;               //Initial speed of the player.
    float checkWait;                //Seconds the player will wait before cecking if in area.
    float damageWait;               //Seconds the player will wait  to make damage.
    float strength = 2;             //Variable used to define how much damage will deal.
    float setPositionX;             //Variable to calculate position X inside the circle.
    float setPositionY;             //Variable to calculate position Y inside the circle.
    float zRef;                     //Variable used to assign y axis value to z axis.
    float distToLeader;             //Distance to calculate how far away from leader and calculate speed.
    float distToBystander;          //Distance to calculate how far away from bystander.
    float maxLDist = 8;             //Limit of how far from the leader the zombie will go.
    float maxBDist = 4;             //Distace at which the zombie will atemp to attack a bystander.
    float slowingDist = 6;          //Distance at which the zombie will start to slow down.
    Vector3 targetPosition;         //Destination of the movement.
    Vector2 leadRef;                //Reference to go to Leader or Bystander.
    Collider2D coll;                //Colider for interactions.
    GameObject Leader;              //GameObject to calculate distance between the zombie and the leader.
    GameObject closestBystander = null;     //GameObject to calculate distance between the zombie and closest bystander.
    GameObject enemy;               //Bystander that is being attacked.
    GameObject sceneController;     //Scene controller object to count the number of lost zombies.
    SceneController sceneControllerScript;  //SceneController script declaration to be able to modify the numer of lost zombies.
    public float Healthpoints;      //Remaining life of the character.
    public float MaxHealthpoints = 10;      //Maximum healtpoints of the character.
    public HealthBarController Healthbar;   //Declaration of the Healthbar of this character.

    void Start()
    {
        Healthpoints = MaxHealthpoints;
        Healthbar.SetHealth(Healthpoints, MaxHealthpoints);
        coll = GetComponent<Collider2D>();
        Leader = GameObject.Find("ZombieLeader");
        sceneController = GameObject.Find("SceneController");
        sceneControllerScript = sceneController.GetComponent<SceneController>();
        StartCoroutine("AreaCheck");
        targetPosition = transform.position;
        zRef = targetPosition.y;
        targetPosition.z = zRef;
        transform.position = targetPosition;
    }
    /*
        In the 2 first lines of code is defined the Healt of the Zombie.
        3rd line just calls the collider of the Game Object.
        4th line creates the object for the Zombie Leader to be able to find him.
        5th line initiates the funtion “AreaCheck” that will maintain the Zombie Leader looking for near Bystanders.
        Last 3 lines set the correct position of the Zombie in the ”z” axis, setting it equal to the “y” axis. 
    */


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
    /*
        In the first line and first “if” is checked if the there is a click and if it landed on the Zombie.
        The second “if” checks if when the button is released and if it was over he Zombie to then save the location of the
        mouse as the destination of the Zombie. The CheckBoarders() function is called to prevent the Zombie from going outside of
        th stage. The last “if” just execute the Turn() and Moving() function to move the Zombie.
    */

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
    /*
        In the Moving() function, first is validated if there is still a leader in the scene. If it does it gives a
        priority to the movement done by the hand. If it does it measures the distance to see if it needs to move closest,
        further or stand still. if moved by hand and far away from the leader, the Zombie will begin to reduce its speed.
        Finally, the function CheckBoarders() is called, position in "z" is arranged and then the movement is done until
        distination is reached.
    */

    void Turn()
    {
        if (transform.position.x != targetPosition.x)               
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
    /*
        Turn() function just check where the Zombie is walking to and turn the sprite to dace each direction.
    */

    void CoordinatesCalc()                                           
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
    /*
        In this function, a coordinate around the Leader is calculated to assign the destination in case the Leader is far away.
        This is done by getting a dandom number to see if the new location will be on the up/right, down/right, down/left or up/left
        side of the leader. 
    */

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
    /*
        CheckBoarders() funcion will check of the bool of any boarder (Up, down, left and right) is set to ”true” and then
        lock the “x” (for the upper and lower boearder) or “y” (For the sides boarders) axis movement.
        If the bools are not set to true then is confirmed as false.
    */

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
    /*
        In this function, first is checked if the Leader is still around and then it proceeds to obtain the area of the leader.
        If the zombie is within, it checs that he is at least at some distance to the Leader. If not, it calculates a new location.
    */

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
    /*
        FincClosestBystander() start by creating an array of every object named "Bystander" and then going trough all of
        them and whenever it find a closer one, ot assign it to the "closes" variable.
        When the whole array os checked, it returns the "closest" Bystander.
    */

    public void DamageDone (float damage)
    {
        Healthpoints -= damage;
        Healthbar.SetHealth(Healthpoints, MaxHealthpoints);
        if (Healthpoints <= 0)
        {
            sceneControllerScript.lostZombies += 1;
            Destroy(gameObject);
        }
    }
    /*
        DamageDone() function is intended to be called from an enemy GameObject to execute the damage on this Zombie.
        This funtions receives the strength of the attacking enemy and the substract t to the current health.
    */

    private IEnumerator Attacking()
    {
        if(enemy != null)
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
    }
    /*
        Attacking() checksif the bool "attacking" is set to true and then calls the funtion DamageDone() of the enemy
        with the own strength as a parameter.
        then it waits for a 1.5 to 2.5 seconds to execute the funtion again.
    */

    private IEnumerator AreaCheck()                                 
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
    /*
        AreaCheck() Coroutine will always look for the area of the leader or a nearby Bystander. First it will check if the zombie
        is attacking, then if he already has a destination set. Then, the coroutine check for a bystander and he is in the minimum
        distance to go and get it and finally check the leader area and goinside in case the Zombie is outside.
    */ 

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

    /*
        The Trigger activated on collision will mainly check if a border of the scene is hit to prevent advancing further on
        said axis. The funtion also check of the collision is with a Bystander and then stop and proceed to attack.
    */

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "Bystander")
        {
            enemy = col.gameObject;
            attacking = true;
        }
    }
    /*
        The OnStay trigger will just check if there is still a Bystander to keep attacking
    */

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bystander")
        {
            enemy = col.gameObject;
            attacking = false;
            StopCoroutine("Attacking");
        }
    }
    /*
        And finally, the OnExit Trigger os just coded to stop the Attacking() coroutine when there is no Bystader collided.
    */
}