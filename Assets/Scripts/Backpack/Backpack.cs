using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Backpack : MonoBehaviour
{
  [SerializeField] protected List<Item> startingItems = null;
  [SerializeField] protected Transform itemsParent = null;
  [SerializeField] private ItemSlot[] itemSlots = null;
  [SerializeField] private ToolSlot[] toolSlots = null;
  [SerializeField] private Image draggingIcon = null;
  [SerializeField] private GameObject itemPickupPrefab = null;
  [SerializeField] private Transform character;

  private Slot draggingSlot;

  private void Awake()
  {
    EventManager.instance.LeftClickDownEvent += OnSlotClickDown;
    EventManager.instance.RightClickDownEvent += OnSlotRightClick;
    EventManager.instance.LeftClickUpEvent += OnSlotClickUp;
    EventManager.instance.RightClickUpEvent += OnSlotClickUp;
    EventManager.instance.BeginDragEvent += OnSlotBeginDrag;
    EventManager.instance.EndDragEvent += OnSlotEndDrag;

    SetStartingItems();
  }

  private void OnValidate()
  {
    if (itemsParent != null)
    {
      itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
    }

    if (!Application.isPlaying) SetStartingItems();
  }

  private void SetStartingItems()
  {
    int i = 0;
    for (; i < startingItems.Count && i < itemSlots.Length; i++)
    {
      itemSlots[i].item = startingItems[i];
    }

    for (; i < itemSlots.Length; i++)
    {
      itemSlots[i].item = null;
    }
  }

  private void OnSlotClickDown(Slot slot)
  {
    slot.pressed = true;
  }

  private void OnSlotRightClick(Slot slot)
  {
    if (slot is ItemSlot)
    {
      GameObject itemGO = (GameObject)Instantiate(itemPickupPrefab, character.position, character.rotation);
      ItemPickup item = itemGO.GetComponent<ItemPickup>();
      item.item = slot.item;
      RemoveItem(slot.item);
    }
  }

  private void OnSlotClickUp(Slot slot)
  {
    slot.pressed = false;
  }

  private void OnSlotBeginDrag(Slot slot)
  {
    Debug.Log("Begin Drag");
    if (slot.item == null) return;

    draggingSlot = slot;
    draggingIcon.sprite = slot.item.icon;
    draggingIcon.transform.position = Input.mousePosition;
    draggingIcon.enabled = true;
  }

  private void OnSlotEndDrag(Slot slot)
  {
    Debug.Log("End Drag");
    draggingSlot = null;
    draggingIcon.enabled = false;
  }

  public bool AddItem(Item item)
  {
    Debug.Log("Add Item: " + item);
    for (int i = 0; i < itemSlots.Length; i++)
    {
      if (itemSlots[i].item == null)
      {
        itemSlots[i].item = item;
        return true;
      }
    }

    return false;
  }

  public bool RemoveItem(Item item)
  {
    for (int i = 0; i < itemSlots.Length; i++)
    {
      if (itemSlots[i].item == item)
      {
        itemSlots[i].item = null;
        return true;
      }
    }

    return false;
  }

  public bool IsFull()
  {
    for (int i = 0; i < itemSlots.Length; i++)
    {
      if (itemSlots[i].item == null)
      {
        return false;
      }
    }

    return false;
  }
}