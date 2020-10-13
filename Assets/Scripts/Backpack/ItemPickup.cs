using UnityEngine;

public class ItemPickup : MonoBehaviour
{
  public Item item;
  public Backpack Backpack;
  public Color emptyColor;
  public KeyCode keyCode = KeyCode.E;

  private bool inRange;

  private void OnValidate()
  {
    if (Backpack == null) Backpack = FindObjectOfType<Backpack>();
  }

  private void Update()
  {
    if (inRange && Input.GetKeyDown(keyCode))
    {
      Backpack.AddItem(item);
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
}
