using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
  [Header("Serialize Fields")]
  [SerializeField] private LayerMask enemyLayers = default;
  [SerializeField] private Transform attackPoint = null;

  [Header("Configurables")]
  public float attackRange;
  public int attackDamage;

  private Animator animator;

  private void Start()
  {
    animator = GetComponent<Animator>();
  }

  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Attack();
    }
    else
    {
      animator.SetBool("IsAttacking", false);
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
