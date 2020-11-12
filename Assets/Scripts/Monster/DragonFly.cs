using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFly : Monster
{
    //public GameObject dragonFly;//refrence of dragonfly gameobject
    public float speed;
    public int attack;
    public int health;
    public float followDis;
    public float attackDis;
    public float range;
    private int direction;
    private bool facingRight = false;

    Vector2 original;

    Vector2 newPosition;
    // Start is called before the first frame update
    void Start()
    {
        //dragonFly.GetComponent<Rigidbody2D>().useGravity = false; //.gravity = 0f;
        setMoveSpeed(speed);
        setAttackDamage(attack);
        setLifePoints(health);
        setFollowRadius(followDis);
        setAttackRadius(attackDis);
        direction = -1;
        original = transform.position;

        PositionChange();
    }

    void PositionChange()
    {
        newPosition = new Vector2(Random.Range(-range, range), Random.Range(-range, range));
        Debug.Log("new range: " + newPosition);
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Vector2.Distance(transform.position, newPosition) < 1)
             PositionChange();
         Vector2 position = new Vector2(transform.position.x, transform.position.y);
         Debug.Log("new position: " + position);
         Vector2 destination = newPosition + position;
         Debug.Log("new destination: " + destination);
         transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);*/

        switch (direction)
        {
            case -1:
                // Moving Left
                if (transform.position.x > (original.x - range))
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
                if (transform.position.x < (original.x + range))
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
}
