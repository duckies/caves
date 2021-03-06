﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSlime : Enemy
{
    // combine both Turret script and patroller script since our ice boi can shoot out ice
    [SerializeField] private int attackDamage;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float shootingRate = default;
    [SerializeField] private GameObject projectile = null;
    [SerializeField] private Transform spawnLocation = null;

    private float shootingTimer;
    private int wayIndex = 0;

    protected override void Move()
    {
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
    }

    protected override void Attack()
    {
        base.Attack();

        if (!IsPlayerInRange()) return;

        shootingTimer += Time.deltaTime;
        if (shootingTimer > shootingRate)
        {
            animator.SetTrigger("Attack");
            Instantiate(projectile, spawnLocation.position, Quaternion.identity);
            shootingTimer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            Debug.Log("Patroller hit player");
            other.collider.GetComponent<Character>().TakeDamage(attackDamage);
        }
    }
}
