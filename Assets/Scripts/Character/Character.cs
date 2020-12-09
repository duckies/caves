using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Character : MonoBehaviour
{
  [Header("Serialize Fields")]
  public Transform respawnPoint = null;
  [SerializeField] private GridLayoutGroup healthBar = null;
  [SerializeField] private GameObject heart = null;
  [SerializeField] private GameObject heartHalf = null;
  [SerializeField] private TextMeshProUGUI text = null;

  [SerializeField] private Rigidbody2D rb = null;

  [Header("Stats")]
  public int health = 100;
  public float regenRate = 1.0f;

  [Header("Configurables")]
  public float relocateFallHeight = -20f;
  public int numHearts = 5;

  public int curHealth = 0;
  public static Character instance;

  private float regenCountdown = 0.0f;

  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  private void Awake()
  {
    if (instance == null) instance = this;
    else Destroy(this);

    curHealth = health;

    DrawHearts();
  }

  private void Update()
  {
    // Move the player back to the start if they fall off.
    // May want this to hurt or kill the player.
    if (gameObject.transform.position.y < relocateFallHeight)
    {
      rb.velocity = Vector2.zero;
      gameObject.transform.position = respawnPoint.position;
    }

    if (curHealth < health)
    {
      regenCountdown += Time.deltaTime;

      if (regenCountdown >= regenRate)
      {
        curHealth++;
        regenCountdown = 0.0f;
      }

      DrawHearts();
    }
  }

  public bool HalfHeart()
  {
    return curHealth > 0 && curHealth % (numHearts * 4) != 0;
  }

  public int FullHearts()
  {
    return curHealth / (numHearts * 4);
  }

  public float HealthPercent()
  {
    return Mathf.Clamp((float)curHealth / (float)health, 0f, 1f);
  }

  public void DrawHearts()
  {
    text.text = string.Format("Health: {0:P0}", HealthPercent());

    foreach (Transform child in healthBar.transform)
    {
      Destroy(child.gameObject);
    }

    for (int i = 0; i < FullHearts(); i++)
    {
      Instantiate(heart, Vector3.zero, Quaternion.identity, healthBar.transform);
    }

    if (HalfHeart())
    {
      Instantiate(heartHalf, Vector3.zero, Quaternion.identity, healthBar.transform);
    }
  }

  public void TakeDamage(int amount)
  {
    curHealth -= amount;

    DrawHearts();

    if (curHealth <= 0)
    {
      Death();
    }
  }

  public void Death()
  {
    EventManager.instance.OnDeathEvent(this);
  }
}