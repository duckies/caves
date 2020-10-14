using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
  [Header("Stats")]
  public int Health = 100;

  [Header("Configurables")]
  public float relocateFallHeight = -20f;

  [Header("Serialize Fields")]
  [SerializeField] private Transform respawnPoint = null;

  private void Update()
  {
    // Move the player back to the start if they fall off.
    if (gameObject.transform.position.y < relocateFallHeight)
    {
      gameObject.transform.position = respawnPoint.position;
    }
  }
}