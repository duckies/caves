using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    public float minDist;
    public float maxDist;
    private int direction;
   // public float distance;

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
        setMoveSpeed(speed);
        setAttackDamage(attack);
        setLifePoints(health);
        setFollowRadius(followDis);
        setAttackRadius(attackDis);
        direction = -1;
        currentAction = MAction.Patrol;
        setAction(currentAction);
    }

    // Update is called once per frame
    void Update()
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
                    Flip();
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
                    Flip();
                    }
                    break;
            }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
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
