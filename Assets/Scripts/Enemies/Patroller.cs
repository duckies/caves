using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : Enemy
{
  [SerializeField] private int attackDamage;
  [SerializeField] private Transform[] waypoints;

  private int wayIndex = 0;

  protected override void Move()
  {
    // Move to the player if they're close enough.
    base.Move();

    Patrol();
  }

  protected override void Attack()
  {
    if (!animator || !ContainsParam("IsAttacking")) return;

    if (IsPlayerInRange())
    {
      animator.SetBool("IsAttacking", true);
    }
    else
    {
      animator.SetBool("IsAttacking", false);
    }
  }

  private bool ContainsParam(string name)
  {
    foreach (AnimatorControllerParameter param in animator.parameters)
    {
      if (param.name == name) return true;
    }

    return false;
  }

  protected void Patrol()
  {
    // No waypoints means no patrolling.
    if (waypoints.Length == 0) return;

    if (!IsPlayerInRange())
    {
      // Change waypoints by cycling through the array length if we are close enough to a waypoint.
      if (Vector2.Distance(transform.position, waypoints[wayIndex].position) < 0.1f)
      {
        wayIndex = (wayIndex + 1) % waypoints.Length;
      }

      // Move to the current waypoint
      transform.position = Vector2.MoveTowards(transform.position, waypoints[wayIndex].position, speed * Time.deltaTime);
    }
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      other.gameObject.GetComponent<Character>().TakeDamage(attackDamage);
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      other.GetComponent<Character>().TakeDamage(attackDamage);
    }
  }

}
