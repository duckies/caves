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
