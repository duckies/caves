using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Backpack : MonoBehaviour
{
  [SerializeField] protected List<Item> startingItems = null;
  [SerializeField] protected List<Tool> startingTools = null;
  [SerializeField] protected Transform itemsParent = null;
  [SerializeField] protected Transform toolsParent = null;
  [SerializeField] private ItemSlot[] itemSlots = null;
  [SerializeField] private ToolSlot[] toolSlots = null;
  [SerializeField] private Image draggingIcon = null;
  [SerializeField] private GameObject itemPickupPrefab = null;
  [SerializeField] private Transform character = null;

  private ItemSlot draggingSlot;

  private void Awake()
  {
    EventManager.instance.KeyDownEvent += OnSlotKeyDown;
    EventManager.instance.KeyUpEvent += OnSlotKeyUp;
    EventManager.instance.LeftClickDownEvent += OnSlotClickDown;
    EventManager.instance.RightClickDownEvent += OnSlotRightClick;
    EventManager.instance.LeftClickUpEvent += OnSlotClickUp;
    EventManager.instance.RightClickUpEvent += OnSlotClickUp;
    EventManager.instance.BeginDragEvent += OnSlotBeginDrag;
    EventManager.instance.EndDragEvent += OnSlotEndDrag;
    EventManager.instance.DragEvent += OnSlotDrag;
    EventManager.instance.DropEvent += OnSlotDrop;
    EventManager.instance.ConsumeItem += OnConsumeItem;
    EventManager.instance.CreateItem += OnCreateItem;

    SetStartingItems();
    SetStartingTools();
  }

  private void OnValidate()
  {
    if (itemsParent != null)
    {
      itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
    }

    if (toolsParent != null)
    {
      toolSlots = toolsParent.GetComponentsInChildren<ToolSlot>();
    }

    if (!Application.isPlaying)
    {
      SetStartingItems();
      SetStartingTools();
    }
  }

  private void SetStartingTools()
  {
    int i = 0;
    for (; i < startingTools.Count && i < toolSlots.Length; i++)
    {
      toolSlots[i].tool = startingTools[i];
    }

    for (; i < toolSlots.Length; i++)
    {
      toolSlots[i].tool = null;
    }
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

  private void OnSlotKeyDown(Slot slot)
  {
    slot.pressed = true;
  }

  private void OnSlotKeyUp(Slot slot)
  {
    slot.pressed = false;
    if (slot is ToolSlot toolSlot)
    {
      EventManager.instance.OnToolUse(toolSlot);
    }
    else if (slot is ItemSlot itemSlot && itemSlot.item is SeedItem seed)
    {
      EventManager.instance.OnSeedUse(seed);
    }
  }

  private void OnSlotClickDown(Slot slot)
  {
    slot.pressed = true;
  }

  private void OnSlotRightClick(Slot slot)
  {
    if (slot is ItemSlot itemSlot)
    {
      GameObject itemGO = (GameObject)Instantiate(itemPickupPrefab, character.position, character.rotation);
      ItemPickup item = itemGO.GetComponent<ItemPickup>();
      item.item = itemSlot.item;
      RemoveItem(itemSlot.item);
    }
  }

  private void OnSlotClickUp(Slot slot)
  {
    slot.pressed = false;
  }

  private void OnSlotBeginDrag(Slot slot)
  {
    if (slot is ItemSlot itemSlot)
    {
      draggingSlot = itemSlot;
      draggingIcon.sprite = itemSlot.item.sprite;
      draggingIcon.transform.position = Input.mousePosition;
      draggingIcon.enabled = true;
    }
  }

  private void OnSlotEndDrag(Slot slot)
  {
    draggingSlot = null;
    draggingIcon.enabled = false;
  }

  private void OnSlotDrag(Slot slot)
  {
    if (draggingIcon.enabled)
    {
      draggingIcon.transform.position = Input.mousePosition;
    }
  }

  private void OnSlotDrop(Slot slot)
  {
    // TODO: Fix issue where dropping a tool here causes error!
    if (slot is ItemSlot itemSlot)
    {
      Item draggedItem = draggingSlot.item;
      draggingSlot.item = itemSlot.item;
      itemSlot.item = draggedItem;
    }
  }

  private void OnCreateItem(Item item)
  {
    AddItem(item);
  }

  private void OnConsumeItem(Item item)
  {
    RemoveItem(item);
  }

  public bool AddItem(Item item)
  {
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