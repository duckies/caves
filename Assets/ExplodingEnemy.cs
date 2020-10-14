using UnityEngine;

public class ExplodingEnemy : MonoBehaviour
{
  [SerializeField] private Item item = null;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      EventManager.instance.OnCreateItem(item);
      Destroy(gameObject);
      return;
    }
  }
}
