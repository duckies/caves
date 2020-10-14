using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
  //   [SerializeField] private Sprite[] sprites = null;
  [SerializeField] private float winGrowthAmount = 10;
  [SerializeField] private Slider slider;

  private float treeState = 0f;

  private void Awake()
  {
    EventManager.instance.HarvestPlant += OnHarvestPlant;
    slider.maxValue = winGrowthAmount;
  }

  public void AdvanceState(float amount)
  {
    treeState += amount;
    slider.value = treeState;
  }

  public float GetState()
  {
    return treeState;
  }

  private void OnHarvestPlant(float growthAmount)
  {
    AdvanceState(growthAmount);
  }
}
