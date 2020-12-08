using UnityEngine;

public class Snowman : Fly
{
  [SerializeField] private GameObject[] parts;

  protected override void Death()
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

    for (int i = 0; i < parts.Length; i++)
    {
      Instantiate(parts[i], transform.position, transform.rotation, null);
    }

    Destroy(gameObject);
  }
}