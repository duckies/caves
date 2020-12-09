using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
  [SerializeField] private Sprite[] sprites = null;
  [SerializeField] private int maxGrowth = 5;
  [SerializeField] private Slider slider = null;
  [SerializeField] private GameObject seed = null;
  [SerializeField] private GameObject[] tutorialWalls = null;
  [SerializeField] private Dialog tutorialDialog = null;
  [SerializeField] private Dialog firstSeedDialog = null;
  [SerializeField] private Dialog firstGrowth = null;

  [SerializeField] private GameObject winScreen = null;

  private SpriteRenderer sprite;
  private int seedsSeen = 0;

  public int curGrowth = 1;

  private void Awake()
  {
    EventManager.instance.DialogCompleteEvent += OnDialogCompleteEvent;
    EventManager.instance.ItemDrop += OnItemDrop;

    sprite = GetComponent<SpriteRenderer>();
    sprite.sprite = null;

    EventManager.instance.PlantGrown += OnPlantGrown;
    slider.maxValue = maxGrowth;
  }

  public void AdvanceState(int amount)
  {
    curGrowth += amount;
    slider.value = maxGrowth;

    if (curGrowth >= 6)
    {
      Debug.Log("You won!");
      winScreen.SetActive(true);
      return;
    }

    // if (curGrowth == 2)
    // {
    DialogManager.instance.StartDialog(firstGrowth);
    // }

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

  private void OnPlantGrown(int amount)
  {
    AdvanceState(amount);
  }

  private void OnDialogCompleteEvent(Dialog dialog)
  {
    if (dialog.name == "StarSeed")
    {
      sprite.sprite = sprites[0];
      Destroy(seed);

      Destroy(tutorialWalls[0]);

      DialogManager.instance.StartDialog(tutorialDialog);
    }
  }

  private void OnItemDrop(Item item)
  {
    if (item is SeedItem seed)
    {
      seedsSeen++;

      if (seedsSeen == 1)
      {
        DialogManager.instance.StartDialog(firstSeedDialog);
        foreach (GameObject wall in tutorialWalls)
        {
          Destroy(wall);
        }
      }
    }
  }
}
