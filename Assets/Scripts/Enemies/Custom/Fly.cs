using UnityEngine;

public class Fly : Enemy
{

  public float shootCooldown = 2f;
  public float tetherRange = 5f;

  [SerializeField] private int attackDamage;
  [SerializeField] private Transform[] waypoints;
  [SerializeField] private GameObject bullet;

  [SerializeField] private Transform firingPoint;

  private float shootCooldownValue = 0f;

  private int wayIndex = 0;

  protected override void Start()
  {
    base.Start();
  }

  protected override void Move()
  {
    // Stop chasing the player if we're too far.
    if (!IsTooFarFromPatrol())
    {
      // Move to the player if they're close enough.
      base.Move();
    }

    Patrol();
  }
  private void Patrol()
  {
    // No waypoints means no patrolling.
    if (waypoints.Length == 0) return;

    // If the player is in range attack, stop moving.
    if (IsPlayerInRange()) return;

    // Change waypoints by cycling through the array length if we are close enough to a waypoint.
    if (Vector2.Distance(transform.position, waypoints[wayIndex].position) < 0.01f)
    {
      wayIndex = (wayIndex + 1) % waypoints.Length;
    }

    // Move to the current waypoint
    transform.position = Vector2.MoveTowards(transform.position, waypoints[wayIndex].position, speed * Time.deltaTime);
  }

  private bool IsTooFarFromPatrol()
  {
    if (waypoints.Length == 0) return false;

    return Vector2.Distance(waypoints[wayIndex].position, transform.position) >= tetherRange;
  }

  protected override void Attack()
  {
    base.Attack();

    // We can't attack if the player is too far.
    if (!IsPlayerInRange()) return;

    if (shootCooldownValue <= 0)
    {
      Vector3 targetPosition = player.transform.position;

      if (firingPoint)
      {
        Vector3 direction = targetPosition - firingPoint.position;
        GameObject bulletInstance = Instantiate(bullet, firingPoint.position, firingPoint.rotation, null);
        Bullet bulletObject = bulletInstance.GetComponent<Bullet>();
        bulletInstance.GetComponent<Rigidbody2D>().velocity = direction * bulletObject.speed;
      }
      else
      {
        Vector3 direction = targetPosition - transform.position;
        GameObject bulletInstance = Instantiate(bullet, transform.position, transform.rotation, null);
        Bullet bulletObject = bulletInstance.GetComponent<Bullet>();
        bulletInstance.GetComponent<Rigidbody2D>().velocity = direction * bulletObject.speed;
      }

      shootCooldownValue = shootCooldown;
    }
    else
    {
      shootCooldownValue -= Time.deltaTime;
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
