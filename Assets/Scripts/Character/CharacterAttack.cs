using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
  [Header("Serialize Fields")]
  [SerializeField] private Bow bow = null;
  [SerializeField] private LayerMask enemyLayers = default;
  [SerializeField] private Transform attackPoint = null;
  [SerializeField] private KeyCode attackKeyCode = default;
  [SerializeField] private KeyCode shootKeyCode = default;

  [SerializeField] private GameObject arrow = null;
  [SerializeField] private Transform shootPoint = null;

  [Header("Configurables")]
  public float arrowForce;
  public float attackRange;
  public int attackDamage;
  public float attackCooldown;

  private float attackCooldownValue;

  private Animator animator;

  private void Start()
  {
    animator = GetComponent<Animator>();
  }

  private void Update()
  {
    if (Input.GetKeyDown(attackKeyCode))
    {
      Attack();
    }
    else
    {
      animator.SetBool("IsAttacking", false);
    }

    // Bow and Arrow Ranged Attack
    if (attackCooldownValue <= 0 && Input.GetKeyDown(shootKeyCode))
    {
      animator.SetBool("IsShooting", true);
      bow.Shoot();
      attackCooldownValue = attackCooldown;
    }
    else
    {
      animator.SetBool("IsShooting", false);
      attackCooldownValue -= Time.deltaTime;
    }
  }

  private void Attack()
  {
    animator.SetBool("IsAttacking", true);

    // We may want to change this to a rectangle, since, you know, spear.
    Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

    foreach (Collider2D enemy in enemies)
    {
      Debug.Log("Hit " + enemy.name);
      enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
    }
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
  }
}
