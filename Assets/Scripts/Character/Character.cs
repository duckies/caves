using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
  [Header("Stats")]
  public int Health = 100;

  [Header("Configurables")]
  public float relocateFallHeight = -20f;

  [Header("Serialize Fields")]
  [SerializeField] private Backpack backpack = null;
  [SerializeField] private Image draggableItem = null;
  [SerializeField] private Transform respawnPoint = null;
  public GameObject itemPickupPrefab;

  private ItemSlot draggedSlot;

  private void Awake()
  {
    // Setup Events
    backpack.OnRightClickEvent += DropItem;
    backpack.OnBeginDragEvent += BeginDrag;
    backpack.OnEndDragEvent += EndDrag;
    backpack.OnDragEvent += Drag;
    backpack.OnDropEvent += Drop;
  }

  private void Update()
  {
    // Move the player back to the start if they fall off.
    if (gameObject.transform.position.y < relocateFallHeight)
    {
      gameObject.transform.position = respawnPoint.position;
    }
  }

  private void DropItem(ItemSlot itemSlot)
  {
    GameObject itemGo = (GameObject)Instantiate(itemPickupPrefab, transform.position, transform.rotation);
    ItemPickup item = itemGo.GetComponent<ItemPickup>();
    item.item = itemSlot.Item;
    backpack.RemoveItem(itemSlot.Item);
  }

  private void BeginDrag(ItemSlot itemSlot)
  {
    if (itemSlot.Item != null)
    {
      draggedSlot = itemSlot;
      draggableItem.sprite = itemSlot.Item.icon;
      draggableItem.transform.position = Input.mousePosition;
      draggableItem.enabled = true;
    }
  }

  private void EndDrag(ItemSlot itemSlot)
  {
    draggedSlot = null;
    draggableItem.enabled = false;
  }

  private void Drop(ItemSlot dropItemSlot)
  {
    if (dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item))
    {
      Item draggedItem = draggedSlot.Item;
      draggedSlot.Item = dropItemSlot.Item;
      dropItemSlot.Item = draggedItem;
    }
  }

  private void Drag(ItemSlot itemSlot)
  {
    if (draggableItem.enabled)
    {
      draggableItem.transform.position = Input.mousePosition;
    };
  }
}