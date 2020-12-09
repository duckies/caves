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

  [SerializeField] private Plant[] triggersFirstGrowth = null;
  [SerializeField] private Plant[] triggersSecondGrowth = null;
  [SerializeField] private Plant[] triggersThirdGrowth = null;
  [SerializeField] private Plant[] triggersFourthGrowth = null;

  [SerializeField] private GameObject forestBarrier = null;
  [SerializeField] private GameObject lavaBarrier = null;
  [SerializeField] private GameObject snowBarrier = null;


  [SerializeField] private int[] growths = null;

  private SpriteRenderer sprite;
  private int seedsSeen = 0;

  public int curGrowth = 1;

  private void Start()
  {
    growths = new int[] { 0, 0, 0, 0 };
  }

  private void Awake()
  {
    EventManager.instance.DialogCompleteEvent += OnDialogCompleteEvent;
    EventManager.instance.ItemDrop += OnItemDrop;

    sprite = GetComponent<SpriteRenderer>();
    sprite.sprite = null;

    EventManager.instance.PlantGrown += OnPlantGrown;
    slider.maxValue = maxGrowth;
  }

  public void AdvanceState(Plant plant)
  {
    switch (curGrowth)
    {
      case 1:
        foreach (Plant triggerPlant in triggersFirstGrowth)
        {
          if (triggerPlant.name == plant.name)
          {
            growths[0]++;
            break;
          }
        }

        if (growths[0] == triggersFirstGrowth.Length)
        {
          curGrowth++;
          ChangeSprite(0);
          forestBarrier.SetActive(false);
        }
        break;

      case 2:
        foreach (Plant triggerPlant in triggersSecondGrowth)
        {
          if (triggerPlant.name == plant.name)
          {
            growths[1]++;
            break;
          }
        }

        if (growths[1] == triggersSecondGrowth.Length)
        {
          curGrowth++;
          DialogManager.instance.StartDialog(firstGrowth);
          ChangeSprite(1);
          lavaBarrier.SetActive(false);
        }
        break;

      case 3:
        foreach (Plant triggerPlant in triggersThirdGrowth)
        {
          if (triggerPlant.name == plant.name)
          {
            growths[2]++;
            break;
          }
        }

        if (growths[2] == triggersThirdGrowth.Length)
        {
          curGrowth++;
          ChangeSprite(2);
          snowBarrier.SetActive(false);
        }
        break;

      case 4:
        foreach (Plant triggerPlant in triggersFourthGrowth)
        {
          if (triggerPlant.name == plant.name)
          {
            growths[3]++;
            break;
          }
        }

        if (growths[3] == triggersFourthGrowth.Length)
        {
          winScreen.SetActive(true);
          ChangeSprite(3);
          return;
        }
        break;
    }
    // curGrowth += amount;
    // slider.value = maxGrowth;

    // if (curGrowth >= 6)
    // {
    //   Debug.Log("You won!");
    //   winScreen.SetActive(true);
    //   return;
    // }

    // // if (curGrowth == 2)
    // // {
    // DialogManager.instance.StartDialog(firstGrowth);
    // // }

    // ChangeSprite(curGrowth - 2);
  }

  public void ChangeSprite(int index)
  {
    sprite.sprite = sprites[index + 1];
  }

  public float GetState()
  {
    return curGrowth;
  }

  private void OnPlantGrown(Plant plant)
  {
    AdvanceState(plant);
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
