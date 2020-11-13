﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFly : Monster
{
    public GameObject dragonFly;//refrence of dragonfly gameobject
    public float speed;
    public int attack;
    public int health;
    public float followDis;
    public float attackDis;
    public float range;
    private int direction;
    private bool facingRight = false;

    Vector3 original;

    Vector2 newPosition;

    MAction currentAction;
    // Start is called before the first frame update
    void Start()
    {
        // disable gravity for our dragonfly
        dragonFly.GetComponent<Rigidbody2D>().gravityScale = 0f; //.gravity = 0f;
        setMoveSpeed(speed);
        setAttackDamage(attack);
        setLifePoints(health);
        setFollowRadius(followDis);
        setAttackRadius(attackDis);
        direction = -1;
        original = transform.position;

        StartCoroutine(FlyFSM());
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
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    IEnumerator FlyFSM()
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
}