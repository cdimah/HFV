using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BystanderController : MonoBehaviour
{

    bool refPosonUBorder = false;        //Used to detect if in Upper boarder.
    bool refPosonRBorder = false;        //Used to detect if in Right boarder.
    bool refPosonDBorder = false;        //Used to detect if in Lower boarder.
    bool refPosonLBorder = false;        //Used to detect if in Upper boarder.
    bool refPosattacking = false;        //Used to know if the zombie is attacking.
    bool refPosdestinationSet;           //Destination point is set.
    float refPosspeed;                   //Speed at which the Bystander will run.
    float refPosdamageWait;              //Seconds the player will wait  to make damage.
    float refPosstrength;                //Variable used to define how much damage will deal.
    float refPossetPositionX;            //Variable to calculate X direction of Euphoric.
    float refPossetPositionY;            //Variable to calculate Y direction of Euphoric.
    float refPoscheckWait;               //Seconds the bystander will wait before cecking if in area.
    float refPosdistToZombie;            //Used to calculate distance to closest Zombie.
    float refPoszRef;                    //Used to assign y value to z.
    float refPosmaxDist = 3;             //Distance at which will run from zombie.
    Vector2 refPos;
    Vector3 refPostargetPosition;
    Vector3 refPoseuphoricDirection;     //Vector to determinate direction at which the Euphoric will run.
    GameObject refPosclosestZombie;
    Collider2D coll;           //Colider for interactions.
    SpriteRenderer refPosmySR;
    GameObject enemyChar;              //Bystander that is being attacked.


    public float Healthpoints;      //Remaining life of the character.
    public float MaxHealthpoints = 10; //Maximum healtpoints of the character.
    public HealthBarController Healthbar;    //Declaration of the Healthbar of this character.
    public enum BystanderType       //List of types of Bystanders.
    {
        Shocked,
        Frightened,
        Smart,
        Euphoric,
        Brave
    }

    BystanderType thisBystander;             //Declare the enum of the bystander's type.

    void Start()
    {
        refPosmySR = GetComponent<SpriteRenderer>();
        int i = Random.Range(0, 5);
        if(i == 0)
        {
            thisBystander = BystanderType.Shocked;
        } else if (i == 1)
        {
            thisBystander = BystanderType.Frightened;
        } else if (i == 2)
        {
            thisBystander = BystanderType.Smart;
        } else if (i == 3)
        {
            thisBystander = BystanderType.Euphoric;
        } else if (i == 4)
        {
            thisBystander = BystanderType.Brave;
        }
        Healthpoints = MaxHealthpoints;
        Healthbar.SetHealth(Healthpoints, MaxHealthpoints);
        coll = GetComponent<Collider2D>();     //Create collider for all interactions.
        refPostargetPosition = transform.position;
        refPoszRef = refPostargetPosition.y;
        refPostargetPosition.z = refPoszRef;
        transform.position = refPostargetPosition;
        switch (thisBystander)
        {
            case BystanderType.Shocked:
                break;
            case BystanderType.Frightened:
                refPosspeed = 2;
                refPosmySR.color = Color.blue;
                break;
            case BystanderType.Smart:
                refPosspeed = 4;
                refPosstrength = 1;
                refPosmySR.color = Color.green;
                break;
            case BystanderType.Euphoric:
                refPosspeed = 5;
                refPosstrength = 1;
                refPosmySR.color = Color.red;
                int refPosselect = Random.Range(1, 5);
                if (refPosselect == 1)
                {
                    refPossetPositionX = 1f;
                    refPossetPositionY = 1f;
                }
                else if (refPosselect == 2)
                {
                    refPossetPositionX = 1f;
                    refPossetPositionY = -1f;
                }
                else if (refPosselect == 3)
                {
                    refPossetPositionX = -1f;
                    refPossetPositionY = 1f;
                }
                else
                {
                    refPossetPositionX = -1f;
                    refPossetPositionY = -1f;
                }
                refPoseuphoricDirection = new Vector3(refPossetPositionX, refPossetPositionY, 0f);
                break;
            case BystanderType.Brave:
                refPosspeed = 3;
                refPosstrength = 2;
                refPosmySR.color = Color.yellow;
                break;
        }
        refPosclosestZombie = FindClosestZombie();
        StartCoroutine("ZombieCheck");
    }

    // Update is called once per frame
    void Update()
    {
        switch(thisBystander)
        {
            case BystanderType.Shocked:
                //Only include shocked animation
                break;
            case BystanderType.Frightened:
                if (refPosdestinationSet == true)
                {
                    Turn();
                    Moving();
                }
                break;
            case BystanderType.Smart:
                if (refPosdestinationSet == true)
                {
                    Turn();
                    Moving();
                }
                break;
            case BystanderType.Euphoric:
                if (refPosdestinationSet == true)
                {
                    EuphoricMoving();
                }
                break;
            case BystanderType.Brave:
                if (refPosdestinationSet == true)
                {
                    Turn();
                    Moving();
                }
                break;
        }

        
    }

    void CalcDest()
    {
        refPos = transform.position;

        switch (thisBystander)
        {
            case BystanderType.Shocked:
                break;
            case BystanderType.Frightened:
                if (refPos.x > refPosclosestZombie.transform.position.x)
                {
                    refPostargetPosition.x = refPos.x + (Random.Range(1.5f, 2.5f));
                }
                else if (refPos.x < refPosclosestZombie.transform.position.x)
                {
                    refPostargetPosition.x = refPos.x + (Random.Range(1.5f, 2.5f) * -1);
                }

                if (refPos.y > refPosclosestZombie.transform.position.y)
                {
                    refPostargetPosition.y = refPos.y + (Random.Range(1.5f, 2.5f));
                }
                else if (refPos.y < refPosclosestZombie.transform.position.y)
                {
                    refPostargetPosition.y = refPos.y + (Random.Range(1.5f, 2.5f) * -1);
                }
                refPosdestinationSet = true;

                break;
            case BystanderType.Smart:
                if (refPos.x > refPosclosestZombie.transform.position.x)
                {
                    refPostargetPosition.x = refPos.x + (Random.Range(1f, 2.5f));
                }
                else if (refPos.x < refPosclosestZombie.transform.position.x)
                {
                    refPostargetPosition.x = refPos.x + (Random.Range(1f, 2.5f) * -1);
                }

                if (refPos.y > refPosclosestZombie.transform.position.y)
                {
                    refPostargetPosition.y = refPos.y + (Random.Range(1f, 2.5f));
                }
                else if (refPos.y < refPosclosestZombie.transform.position.y)
                {
                    refPostargetPosition.y = refPos.y + (Random.Range(1f, 2.5f) * -1);
                }
                refPosdestinationSet = true;

                break;
            case BystanderType.Euphoric:
                refPosdestinationSet = true;
                break;
            case BystanderType.Brave:
                refPostargetPosition = refPosclosestZombie.transform.position;
                refPosdestinationSet = true;

                break;
        }
        
    }

    void Moving()
    {
        CheckBorders();
        refPoszRef = refPostargetPosition.y;
        refPostargetPosition.z = refPoszRef;
        transform.position = Vector3.MoveTowards(transform.position, refPostargetPosition, refPosspeed * Time.deltaTime);
        if (transform.position == refPostargetPosition)
        {
            refPosdestinationSet = false;
        }
    }

    void EuphoricMoving()
    {
        refPoseuphoricDirection.z = refPoseuphoricDirection.y;
        if(refPoseuphoricDirection.x == 1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.Translate(refPoseuphoricDirection * refPosspeed * Time.deltaTime);
    }

    void Turn()
    {
        if (transform.position.x != refPostargetPosition.x)               //To avoid turning at begining or moving only on 'y' axis
        {

            if (transform.position.x < refPostargetPosition.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    void CheckBorders()
    {
        if (refPosonUBorder == true && refPostargetPosition.y > transform.position.y)
        {
            refPostargetPosition.y = transform.position.y;
        }
        else if (refPosonUBorder == true && refPostargetPosition.y < transform.position.y)
        {
            refPosonUBorder = false;
        }

        if (refPosonRBorder == true && refPostargetPosition.x > transform.position.x)
        {
            refPostargetPosition.x = transform.position.x;
        }
        else if (refPosonRBorder == true && refPostargetPosition.x < transform.position.x)
        {
            refPosonRBorder = false;
        }

        if (refPosonDBorder == true && refPostargetPosition.y < transform.position.y)
        {
            refPostargetPosition.y = transform.position.y;
        }
        else if (refPosonDBorder == true && refPostargetPosition.y > transform.position.y)
        {
            refPosonDBorder = false;
        }

        if (refPosonLBorder == true && refPostargetPosition.x < transform.position.x)
        {
            refPostargetPosition.x = transform.position.x;
        }
        else if (refPosonLBorder == true && refPostargetPosition.x > transform.position.x)
        {
            refPosonLBorder = false;
        }

    }

    GameObject FindClosestZombie()
    {
        GameObject[] refPosgos;
        refPosgos = GameObject.FindGameObjectsWithTag("Zombie");
        GameObject refPosclosest = null;
        float refPosdistance = Mathf.Infinity;
        Vector3 refPosposition = transform.position;
        foreach (GameObject refPosgo in refPosgos)
        {
            Vector3 refPosdiff = refPosgo.transform.position - refPosposition;
            float refPoscurDistance = refPosdiff.sqrMagnitude;
            if (refPoscurDistance < refPosdistance)
            {
                refPosclosest = refPosgo;
                refPosdistance = refPoscurDistance;
            }
        }
        return refPosclosest;
    }

    public void DamageDone(float damage)
    {
        Healthpoints -= damage;
        Healthbar.SetHealth(Healthpoints, MaxHealthpoints);
        if (Healthpoints <= 0)
        {
            GameObject refPoscontroller = GameObject.Find("SceneController");
            var controller = refPoscontroller.GetComponent<SceneController>();
            controller.CreateZombie(transform.position);
            Destroy(gameObject);
        }
    }
    private IEnumerator Attacking()
    {
        if (refPosattacking == true)
        {
            if (enemyChar.name == "ZombieLeader")
            {
                var enemy = enemyChar.GetComponent<ZombieLeaderController>();
                enemy.DamageDone(refPosstrength);
            }

            if (enemyChar.name == "Zombie")
            {
                var enemy = enemyChar.GetComponent<ZombieController>();
                enemy.DamageDone(refPosstrength);
            }
        }
        refPosdamageWait = Random.Range(1.5f, 2.5f);
        yield return new WaitForSeconds(refPosdamageWait);
        StartCoroutine("Attacking");
    }

    private IEnumerator ZombieCheck()                                 //Coroutine to Keep bystander checking for Zombies nearby
    {
        refPoscheckWait = Random.Range(1f, 2.5f);
        yield return new WaitForSeconds(refPoscheckWait);

        if (refPosattacking == false)
        {
            if (refPosdestinationSet == false)
            {
                refPosclosestZombie = FindClosestZombie();
                refPosdistToZombie = Vector2.Distance(this.transform.position, refPosclosestZombie.transform.position);
                if (refPosdistToZombie < refPosmaxDist)
                {
                    CalcDest();
                }
            }
        }
       
        StartCoroutine("ZombieCheck");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject refPoscollided = col.gameObject;
        
        if (thisBystander == BystanderType.Euphoric)
        {
            if (refPoscollided.name == "BorderU")
            {
                refPoseuphoricDirection.y = refPoseuphoricDirection.y * - 1;
            }

            if (refPoscollided.name == "BorderR")
            {
                refPoseuphoricDirection.x = refPoseuphoricDirection.x * -1;
            }

            if (refPoscollided.name == "BorderD")
            {
                refPoseuphoricDirection.y = refPoseuphoricDirection.y * -1;
            }

            if (refPoscollided.name == "BorderL")
            {
                refPoseuphoricDirection.x = refPoseuphoricDirection.x * -1;
            }
            if (refPoscollided.tag == "Zombie")
            {
                
                if (refPoscollided.name == "ZombieLeader")
                {
                    var enemy = refPoscollided.GetComponent<ZombieLeaderController>();
                    enemy.DamageDone(refPosstrength);
                }

                if (refPoscollided.name == "Zombie")
                {
                    var enemy = refPoscollided.GetComponent<ZombieController>();
                    enemy.DamageDone(refPosstrength);
                }
            }
        }
        else
        {
            if (refPoscollided.name == "BorderU")
            {
                refPosonUBorder = true;
                refPosdestinationSet = false;
            }

            if (refPoscollided.name == "BorderR")
            {
                refPosonRBorder = true;
                refPosdestinationSet = false;
                if (thisBystander == BystanderType.Smart)
                {
                    refPosdestinationSet = false;
                    enemyChar = refPoscollided;
                    StartCoroutine("Attacking");
                }
            }

            if (refPoscollided.name == "BorderD")
            {
                refPosonDBorder = true;
                refPosdestinationSet = false;
            }

            if (refPoscollided.name == "BorderL")
            {
                refPosonLBorder = true;
                refPosdestinationSet = false;
                if (thisBystander == BystanderType.Smart)
                {
                    refPosdestinationSet = false;
                    enemyChar = refPoscollided;
                    StartCoroutine("Attacking");
                }
            }

            if (refPoscollided.tag == "Zombie")
            {
                if (thisBystander == BystanderType.Brave)
                {
                    refPosdestinationSet = false;
                    enemyChar = refPoscollided;
                    StartCoroutine("Attacking");
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        switch (thisBystander)
        {
            case BystanderType.Shocked:
                break;
            case BystanderType.Frightened:
                break;
            case BystanderType.Smart:
                if (refPosonLBorder == true)
                {
                    if (col.gameObject.tag == "Zombie")
                    {
                        enemyChar = col.gameObject;
                        refPosattacking = true;
                    }
                }
                else if (refPosonRBorder == true)
                {
                    if (col.gameObject.tag == "Zombie")
                    {
                        enemyChar = col.gameObject;
                        refPosattacking = true;
                    }
                }
                break;
            case BystanderType.Euphoric:
                break;
            case BystanderType.Brave:
                if (col.gameObject.tag == "Zombie")
                {
                    enemyChar = col.gameObject;
                    refPosattacking = true;
                }
                break;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        switch (thisBystander)
        {
            case BystanderType.Shocked:
                break;
            case BystanderType.Frightened:
                break;
            case BystanderType.Smart:
                if (col.gameObject.tag == "Zombie")
                {
                    enemyChar = col.gameObject;
                    refPosattacking = false;
                    StopCoroutine("Attacking");
                }
                break;
            case BystanderType.Euphoric:
                break;
            case BystanderType.Brave:
                if (col.gameObject.tag == "Zombie")
                {
                    enemyChar = col.gameObject;
                    refPosattacking = false;
                    StopCoroutine("Attacking");
                }
                break;
        }
    }
}
