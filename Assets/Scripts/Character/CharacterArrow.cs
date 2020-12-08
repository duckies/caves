using UnityEngine;

public class CharacterArrow : MonoBehaviour
{
  [Header("Configurables")]
  public float lifeTime = 5;
  public int damage = 5;

  private Vector3 velocity;
  private bool hasHit = false;
  private Rigidbody2D rb;
  private Collider2D col;

  private void Start()
  {
    Invoke("DestroyProjectile", lifeTime);
    rb = GetComponent<Rigidbody2D>();
    col = GetComponent<Collider2D>();
  }

  private void Update()
  {
    if (hasHit == false)
    {
      float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Enemy")
    {
      other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
      Destroy(gameObject);
    }
    else
    {
      hasHit = true;
      rb.velocity = Vector2.zero;
      col.isTrigger = true;
      rb.isKinematic = true;
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Enemy")
    {
      other.GetComponent<Enemy>().TakeDamage(damage);
      Destroy(gameObject);
    }
    else
    {
      hasHit = true;
      rb.velocity = Vector2.zero;
      col.isTrigger = true;
      rb.isKinematic = true;
    }
  }

  private void DestroyProjectile()
  {
    Destroy(gameObject);
  }
}
