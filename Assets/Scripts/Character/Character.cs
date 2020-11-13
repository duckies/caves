using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
  [Header("Serialize Fields")]
  [SerializeField] private Transform respawnPoint = null;
  [SerializeField] private GridLayoutGroup healthBar = null;
  [SerializeField] private GameObject heart = null;
  [SerializeField] private GameObject heartHalf = null;

  [Header("Stats")]
  public int health = 100;

  [Header("Configurables")]
  public float relocateFallHeight = -20f;
  public int numHearts = 5;

  public int curHealth = 0;
  public static Character instance;

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
      gameObject.transform.position = respawnPoint.position;
    }
  }

  private void DrawHearts()
  {
    bool hasHalfHeart = curHealth > 0 && curHealth % (numHearts * 4) != 0;
    int fullHearts = curHealth / (numHearts * 4);

    foreach (Transform child in healthBar.transform)
    {
      Destroy(child.gameObject);
    }

    for (int i = 0; i < fullHearts; i++)
    {
      Instantiate(heart, Vector3.zero, Quaternion.identity, healthBar.transform);
    }

    if (hasHalfHeart)
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
    // Do sad noises.
  }
}