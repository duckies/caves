using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : Monster
{
    public float speed;
    public int attack;
    public int health;
    public float followDis;
    public float attackDis;
    public GameObject player;
    private bool facingRight = false;
    public int range;
    private int direction;
    // public float distance;
    Vector3 orgPos;

    private MAction currentAction;

    Transform playerTransform;

    void GetPlayerTransform()
    {
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.Log("Player not specified in Inspector");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        orgPos = transform.position;
        setMoveSpeed(speed);
        setAttackDamage(attack);
        setLifePoints(health);
        setFollowRadius(followDis);
        setAttackRadius(attackDis);
        direction = -1;
        currentAction = MAction.Patrol;
        setAction(currentAction);

        StartCoroutine(BlobFSM());
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    IEnumerator BlobFSM()
    {
        while (true)
        {
            yield return StartCoroutine(currentAction.ToString());
        }
    }

    IEnumerator Patrol()
    {
        Debug.Log("Entered Patrol State");
        while (currentAction == MAction.Patrol)
        {
            // move back and forth between range
            switch (direction)
            {
                case -1:
                    // Moving Left
                    if (transform.position.x > (orgPos.x - range))
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
                    }
                    else
                    {
                        direction = 1;
                        Flip();
                    }
                    break;
                case 1:
                    //Moving Right
                    if (transform.position.x < (orgPos.x + range))
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
                    }
                    else
                    {
                        direction = -1;
                        Flip();
                    }
                    break;
            }
            yield return null;
        }
        Debug.Log("Exited Patrol State");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentAction == MAction.Patrol)
        {
            setAction(MAction.Attack);
            Debug.Log("Transform to ATTACK state");
            currentAction = MAction.Attack;
        }
    }
    /* void Patrol()
     {
         while(!checkFollowRadius(playerTransform.position.x, transform.position.x) && !checkAttackRadius(playerTransform.position.x, transform.position.x))
         {
             switch (direction)
             {
                 case -1:
                     // Moving Left
                     if (transform.position.x > minDist)
                     {
                         GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
                     }
                     else
                     {
                         direction = 1;
                     }
                     break;
                 case 1:
                     //Moving Right
                     if (transform.position.x < maxDist)
                     {
                         GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
                     }
                     else
                     {
                         direction = -1;
                     }
                     break;
             }
         }

         if (checkFollowRadius(playerTransform.position.x, transform.position.x))
         {
             setAction(MAction.Follow);
         }
         else if(checkAttackRadius(playerTransform.position.x, transform.position.x))
         {
             setAction(MAction.Attack);
         }

     }*/
}
