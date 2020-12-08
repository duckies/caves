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
    Debug.Log("Projectile Collision" + other.gameObject.name);

    if (other.gameObject.tag == "Player")
    {
      other.gameObject.GetComponent<Character>().TakeDamage(damage);
      Destroy(gameObject);
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      other.GetComponent<Character>().TakeDamage(damage);
      Destroy(gameObject);
    }
  }

  private void DestroyBullet()
  {
    Destroy(gameObject);
  }
}