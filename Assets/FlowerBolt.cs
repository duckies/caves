using UnityEngine;

public class FlowerBolt : MonoBehaviour
{
  [SerializeField] private float speed;
  [SerializeField] private float maxLifeTime = 2.0f;
  [SerializeField] private GameObject destroyEffect = null;
  [SerializeField] private int damage = 3;

  private float curLifeTime = 0f;
  private Character target;
  private Rigidbody2D rigidbody2D;

  private void Awake()
  {
    target = Character.instance;
    rigidbody2D = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    rigidbody2D.velocity = new Vector2(0f, 5f);

    transform.Rotate(new Vector3(0, 0, 90 * Time.deltaTime));

    curLifeTime += Time.deltaTime;

    if (curLifeTime >= maxLifeTime)
    {
      Death();
    }

  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag != "Player") return;

    target.TakeDamage(damage);

    Death();
  }

  private void Death()
  {
    if (destroyEffect)
    {
      Instantiate(destroyEffect, transform.position, Quaternion.identity);
    }

    Destroy(gameObject);
  }
}

