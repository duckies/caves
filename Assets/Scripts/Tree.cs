using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
  [SerializeField] private Sprite[] sprites = null;
  [SerializeField] private int maxGrowth = 5;
  [SerializeField] private Slider slider = null;

  private SpriteRenderer sprite;
  public int curGrowth = 1;

  private void Awake()
  {
    sprite = GetComponent<SpriteRenderer>();
    sprite.sprite = sprites[0];

    EventManager.instance.HarvestPlant += OnHarvestPlant;
    slider.maxValue = maxGrowth;
  }

  public void AdvanceState(int amount)
  {
    curGrowth += amount;
    slider.value = maxGrowth;

    if (curGrowth >= 6)
    {
      Debug.Log("You won!");
      return;
    }

    ChangeSprite(curGrowth - 2);
  }

  public void ChangeSprite(int index)
  {
    sprite.sprite = sprites[index + 1];
  }

  public float GetState()
  {
    return curGrowth;
  }

  private void OnHarvestPlant(int amount)
  {
    AdvanceState(amount);
  }
}
