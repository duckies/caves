using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
  [SerializeField] protected private float speed;
  [SerializeField] protected private float range;
  [SerializeField] private float maxHealth = default;
  [SerializeField] protected private Slider healthSlider;
  [SerializeField] protected GameObject itemPickupPrefab;
  [SerializeField] protected Item item;
  [SerializeField] protected float itemSpawnYOffset;
  [SerializeField, Range(0, 1)] protected float dropChance;
  [SerializeField] protected bool shouldFacePlayer = true;

  private SpriteRenderer sprite;
  protected private Animator animator;
  protected private Transform player;
  private float curHealth;
  private bool facingRight = true;

  protected virtual void Start()
  {
    curHealth = maxHealth;
    healthSlider.value = 1f;
    player = GameObject.FindGameObjectWithTag("Player").transform;
    animator = GetComponent<Animator>();
    sprite = GetComponent<SpriteRenderer>();
  }

  protected virtual void Update()
  {
    if (shouldFacePlayer)
    {
      FaceCharacter();
    }

    if (curHealth <= 0)
    {
      Death();
    }

    Move();

    Attack();
    UpdateHealthBar();
  }

  protected virtual void Move()
  {
    if (IsPlayerInRange())
    {
      transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
  }

  protected virtual bool IsPlayerInRange()
  {
    return Vector2.Distance(transform.position, player.position) < range;
  }

  protected virtual void FaceCharacter()
  {
    float abs = transform.position.x - player.position.x;

    if (abs > 0 && !facingRight || abs < 0 && facingRight)
    {
      facingRight = !facingRight;
      transform.Rotate(0f, 180f, 0f);
    }
  }

  protected virtual void Attack()
  {
  }

  public void TakeDamage(int amount)
  {
    curHealth -= amount;
  }

  protected virtual void Death()
  {
    if (itemPickupPrefab && item && Random.value <= dropChance)
    {
      Vector3 position = new Vector3(
        transform.position.x,
        transform.position.y + itemSpawnYOffset,
        transform.position.z
      );
      GameObject pickupGO = Instantiate(itemPickupPrefab, position, Quaternion.identity);
      ItemPickup pickup = pickupGO.GetComponent<ItemPickup>();
      pickup.item = item;

      EventManager.instance.OnItemDrop(item);
    }

    Destroy(gameObject);
  }

  protected virtual void UpdateHealthBar()
  {
    healthSlider.value = Mathf.Clamp01(curHealth / maxHealth);
  }
}
