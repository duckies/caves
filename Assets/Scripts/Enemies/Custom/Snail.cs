using UnityEngine;

public class Snail : Patroller
{
  [SerializeField] private float fallRange;
  [SerializeField] private int fallDamage = 20;

  private Rigidbody2D rb;

  private bool fell = false;

  protected override void Start()
  {
    base.Start();

    rb = GetComponent<Rigidbody2D>();
  }
  protected override void Update()
  {
    base.Update();

    // If the player is too far, don't fall if they walk under.
    if (Vector2.Distance(transform.position, player.position) >= fallRange) return;

    if (transform.position.x - player.position.x <= 0.01f)
    {
      rb.gravityScale = 3;
      fell = true;
      Invoke("SelfDestruct", 5);
    }
  }

  protected override void Move()
  {
    // Snail doesn't patrol after it falls.
    if (fell) return;

    // We aren't moving towards the player, just patrolling.
    base.Patrol();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      other.GetComponent<Character>().TakeDamage(fallDamage);
      SelfDestruct();
    }

    if (other.tag == "Ground")
    {
      SelfDestruct();
    }
  }

  private void SelfDestruct()
  {
    Destroy(gameObject);
  }
}