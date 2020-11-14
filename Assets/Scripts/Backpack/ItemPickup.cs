using UnityEngine;

public class ItemPickup : MonoBehaviour
{
  public Item item;
  public Color emptyColor;
  public KeyCode keyCode = KeyCode.E;

  private bool inRange;
  private SpriteRenderer sprite;

  private void Start()
  {
    sprite = GetComponent<SpriteRenderer>();
    sprite.sprite = item.sprite;
  }

  private void Update()
  {
    if (inRange && Input.GetKeyDown(keyCode))
    {
      Backpack.instance.AddItem(item);
      Destroy(this.gameObject);
      return;
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      inRange = true;
    }

    // Alternative collection of just walking over the item.
    // if (other.gameObject.CompareTag("Player"))
    // {
    //   Backpack.AddItem(item);
    //   Destroy(this.gameObject);
    //   return;
    // }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      inRange = false;
    }
  }
}
