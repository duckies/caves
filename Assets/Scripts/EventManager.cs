using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
  public static EventManager instance;

  private void Awake()
  {
    if (instance == null) instance = this;
    else Destroy(this);
  }

  // Backpack SLots

  public event Action<Slot> LeftClickDownEvent;
  public event Action<Slot> LeftClickUpEvent;
  public event Action<Slot> RightClickDownEvent;
  public event Action<Slot> RightClickUpEvent;
  public event Action<Slot> DragEvent;
  public event Action<Slot> BeginDragEvent;
  public event Action<Slot> EndDragEvent;
  public event Action<Slot> DropEvent;

  public void OnLeftClickDownEvent(Slot slot)
  {
    LeftClickDownEvent?.Invoke(slot);
  }

  public void OnLeftClickUpEvent(Slot slot)
  {
    LeftClickUpEvent?.Invoke(slot);
  }

  public void OnRightClickDownEvent(Slot slot)
  {
    RightClickDownEvent?.Invoke(slot);
  }

  public void OnRightClickUpEvent(Slot slot)
  {
    RightClickUpEvent?.Invoke(slot);
  }

  public void OnDragEvent(Slot slot)
  {
    DragEvent?.Invoke(slot);
  }

  public void OnBeginDragEvent(Slot slot)
  {
    BeginDragEvent?.Invoke(slot);
  }

  public void OnEndDragEvent(Slot slot)
  {
    EndDragEvent?.Invoke(slot);
  }

  public void OnDropEvent(Slot slot)
  {
    DropEvent?.Invoke(slot);
  }

  // Tool Events

  public event Action<ToolSlot> ToolUse;

  public void OnToolUse(ToolSlot toolSlot)
  {
    ToolUse?.Invoke(toolSlot);
  }

  // Farming Events

  public event Action<float> HarvestPlant;

  public void OnHarvestPlant(float growthAmount)
  {
    HarvestPlant?.Invoke(growthAmount);
  }
}
