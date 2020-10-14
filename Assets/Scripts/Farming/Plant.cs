using UnityEngine;

[CreateAssetMenu(menuName = "Farming/Plant")]
public class Plant : ScriptableObject
{
  public Sprite[] stages;
  public float growthTime;
  public float treeGrowthAmount = 0.1f;
}