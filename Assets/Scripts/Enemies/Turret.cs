using UnityEngine;

public class Turret : Enemy
{
  [SerializeField] private float shootingRate;
  [SerializeField] private GameObject projectile;
  [SerializeField] private Transform spawnLocation;

  private float shootingTimer;

  protected override void Attack()
  {
    base.Attack();

    shootingTimer += Time.deltaTime;
    if (shootingTimer > shootingRate)
    {
      animator.SetTrigger("Attack");
      Instantiate(projectile, spawnLocation.position, Quaternion.identity);
      shootingTimer = 0;
    }
  }
}
