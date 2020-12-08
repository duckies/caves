using UnityEngine;

public class Bullet : MonoBehaviour
{
  public float speed;
  public float lifetime;
  public int damage = 5;

  private void Start()
  {
    Invoke("DestroyBullet", lifetime);
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      other.gameObject.GetComponent<Character>().TakeDamage(damage);
      Destroy(gameObject);
    }
  }

  private void DestroyBullet()
  {
    Destroy(gameObject);
  }
}