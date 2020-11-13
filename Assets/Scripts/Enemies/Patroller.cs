using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : Enemy
{
  [SerializeField] private Transform[] waypoints;

  private int wayIndex = 0;

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
  }
}
