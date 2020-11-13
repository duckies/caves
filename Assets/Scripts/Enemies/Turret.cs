using UnityEngine;

public class Turret : Enemy
{
  [SerializeField] private float shootingRate = default;
  [SerializeField] private GameObject projectile = null;
  [SerializeField] private Transform spawnLocation = null;

  private float shootingTimer;

  // Turret enemies do not move.
  protected override void Move() { }

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
}
