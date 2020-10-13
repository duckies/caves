using UnityEngine;

[CreateAssetMenu(menuName = "Farming/Tool")]
public class Tool : ScriptableObject
{
  public string toolName;
  public Sprite sprite;

  public virtual void Use() { }
}