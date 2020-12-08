using UnityEngine;

public class Lizard : Patroller
{
  public float attackCooldown = 5f;
  public int meleeDamage = 5;

  [SerializeField] private GameObject projectile;
  [SerializeField] private Transform projectileSpawn;

  private float attackCooldownValue = 0f;
  private bool isAttacking = false;

  protected override void Attack()
  {
    base.Attack();

    if (!IsPlayerInRange())
    {
      isAttacking = false;
      animator.SetBool("IsAttacking", false);
      return;
    }

    if (attackCooldownValue <= 0)
    {
      animator.SetBool("IsAttacking", true);
      isAttacking = true;

      if (IsFacingCharacter())
      {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
      }

      Vector3 targetPosition = player.transform.position;
      Vector3 direction = targetPosition - projectileSpawn.position;

      GameObject projectileInstance = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation, null);
      Bullet projectileObject = projectileInstance.GetComponent<Bullet>();

      projectileInstance.GetComponent<Rigidbody2D>().velocity = direction * projectileObject.speed;

      attackCooldownValue = attackCooldown;
    }
    else
    {
      animator.SetBool("IsAttacking", false);
      attackCooldownValue -= Time.deltaTime;
    }
  }

  private bool IsFacingCharacter()
  {
    float abs = transform.position.x - player.position.x;

    return abs > 0 && facingRight || abs < 0 && !facingRight;
  }

  protected override void FaceCharacter()
  {
    // The lizard turns around to attack.
    if (isAttacking) return;

    float abs = transform.position.x - player.position.x;

    if (abs > 0 && !base.facingRight || abs < 0 && facingRight)
    {
      facingRight = !facingRight;
      transform.Rotate(0f, 180f, 0f);
    }
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      other.gameObject.GetComponent<Character>().TakeDamage(meleeDamage);
    }
  }
}