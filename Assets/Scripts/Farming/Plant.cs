using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Farming/Plant")]
public class Plant : ScriptableObject
{
  // Unity doesn't serialize multidimensional arrays.
  public TileBase[] firstStage;
  public TileBase[] secondStage;
  public TileBase[] thirdStage;
  public TileBase[] fourthStage;

  public float growthTime;
  public float treeGrowthAmount = 0.1f;
  public int numStages = -1;

  private void Awake()
  {
    GetNumStages();
  }

  public int GetNumStages()
  {
    if (numStages != -1) return numStages;

    if (fourthStage.Length > 0) numStages = 4;
    else if (thirdStage.Length > 0) numStages = 3;
    else if (secondStage.Length > 0) numStages = 2;
    else if (firstStage.Length > 0) numStages = 1;
    else numStages = 0;

    return numStages;
  }

  public TileBase[] GetStage(int i)
  {
    if (i == 4) return fourthStage;
    else if (i == 3) return thirdStage;
    else if (i == 2) return secondStage;
    else return firstStage;
  }
}