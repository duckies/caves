using UnityEngine;
using System;
using System.Collections.Generic;

public class Backpack : MonoBehaviour
{
  [SerializeField] protected List<Item> startingItems;
  [SerializeField] protected Transform itemsParent;
  [SerializeField] ItemSlot[] itemSlots;
  [SerializeField] ToolSlot[] toolSlots;

  // public event Action<ItemSlot> OnPointerEnterEvent;
  // public event Action<ItemSlot> OnPointerExitEvent;
  public event Action<ItemSlot> OnRightClickEvent;
  public event Action<ItemSlot> OnBeginDragEvent;
  public event Action<ItemSlot> OnEndDragEvent;
  public event Action<ItemSlot> OnDragEvent;
  public event Action<ItemSlot> OnDropEvent;

  private void Awake()
  {
    for (int i = 0; i < itemSlots.Length; i++)
    {
      // itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
      // itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
      itemSlots[i].OnRightClickEvent += OnRightClickEvent;
      itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
      itemSlots[i].OnEndDragEvent += OnEndDragEvent;
      itemSlots[i].OnDragEvent += OnDragEvent;
      itemSlots[i].OnDropEvent += OnDropEvent;
    }

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
      itemSlots[i].Item = startingItems[i];
    }

    for (; i < itemSlots.Length; i++)
    {
      itemSlots[i].Item = null;
    }
  }

  public bool AddItem(Item item)

  {
    Debug.Log("Add Item: " + item);
    for (int i = 0; i < itemSlots.Length; i++)
    {
      if (itemSlots[i].Item == null)
      {
        itemSlots[i].Item = item;
        return true;
      }
    }

    return false;
  }

  public bool RemoveItem(Item item)
  {
    for (int i = 0; i < itemSlots.Length; i++)
    {
      if (itemSlots[i].Item == item)
      {
        itemSlots[i].Item = null;
        return true;
      }
    }

    return false;
  }

  public bool IsFull()
  {
    for (int i = 0; i < itemSlots.Length; i++)
    {
      if (itemSlots[i].Item == null)
      {
        return false;
      }
    }

    return false;
  }
}