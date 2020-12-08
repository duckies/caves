using UnityEngine;

public class Snowflake : Patroller
{
  public int damage = 5;
  public float chargeCooldown = 5f;
  public float chargeRange = 7f;
  public float chargeSpeed = 10f;

  private float chargeCooldownValue;
  private Rigidbody2D rb;

  protected override void Start()
  {
    base.Start();

    rb = GetComponent<Rigidbody2D>();
  }

  protected override void Attack()
  {
    base.Attack();

    if (chargeCooldownValue <= 0)
    {

      // Only charge if the player is close enough.
      if (Vector2.Distance(transform.position, player.transform.position) > chargeRange) return;

      Vector3 targetPosition = player.transform.position;
      Vector3 direction = targetPosition - transform.position;
      rb.velocity = direction * chargeSpeed;
      chargeCooldownValue = chargeCooldown;
    }
    else
    {
      chargeCooldownValue -= Time.deltaTime;
    }
  }
  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      other.gameObject.GetComponent<Character>().TakeDamage(damage);
    }
  }
}