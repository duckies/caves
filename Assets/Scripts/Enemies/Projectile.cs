using UnityEngine;

public class Projectile : MonoBehaviour
{
  private Transform target;
  [SerializeField] private float speed;
  [SerializeField] private float rotationSpeed = 2.0f;
  [SerializeField] private float maxLifeTime = 2.0f;
  [SerializeField] private GameObject destroyEffect = null;

  private float curLifeTime = 0f;

  private void Awake()
  {
    target = GameObject.FindGameObjectWithTag("Player").transform;
  }

  private void Update()
  {
    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    transform.Rotate(new Vector3(0, 0, 90 * Time.deltaTime));

    curLifeTime += Time.deltaTime;

    if (curLifeTime >= maxLifeTime)
    {
      Death();
    }

  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    Debug.Log("Acorn touched " + other.tag);
    if (other.tag != "Player") return;

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
