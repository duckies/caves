﻿using UnityEngine;

public class Fly : Enemy
{
 
    [SerializeField] private int attackDamage;
    [SerializeField] private Transform[] waypoints;

    private int wayIndex = 0;
    public float scaleSize;
    Vector2 originalPos;
    // 
    protected override void Start()
    {
        base.Start();
        // get the original position to get back once done with attack
        originalPos = new Vector2(transform.position.x, transform.position.y);
        // disable gravity on fly
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        // rescale the object
        if (scaleSize < 1)
            transform.localScale -= new Vector3(1 - scaleSize, 1 - scaleSize, 0);
        else if (scaleSize > 1)
            transform.localScale += new Vector3(1 - scaleSize, 1 - scaleSize, 0);
    }
    protected override void Move() 
    {
        // Move to the player if they're close enough.
        base.Move();
       
        Patrol();
    }
    private void Patrol()
    {
        // No waypoints means no patrolling.
        if (waypoints.Length == 0) return;
       
        if (!IsPlayerInRange())
        {
            // Change waypoints by cycling through the array length if we are close enough to a waypoint.
            if (Vector2.Distance(transform.position, waypoints[wayIndex].position) < 0.01f)
            {
           
                wayIndex = (wayIndex + 1) % waypoints.Length;
            }
            
            // Move to the current waypoint
            transform.position = Vector2.MoveTowards(transform.position, waypoints[wayIndex].position, speed * Time.deltaTime);
        }

        if(Vector2.Distance(transform.position, waypoints[wayIndex].position) > 10f)
        {
            transform.position = Vector2.MoveTowards(transform.position, originalPos, speed * Time.deltaTime);
        }
    }

    protected override void Attack()
    {
        base.Attack();

        if (!IsPlayerInRange()) return;

        var target = player.transform.position;

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * 30f * Time.deltaTime);
        //if (transform.position == target)
            transform.position = Vector2.MoveTowards(transform.position, originalPos, speed * Time.deltaTime);
        Patrol();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            Debug.Log("Fly hit player");
            other.collider.GetComponent<Character>().TakeDamage(attackDamage);
        }
    }
}
