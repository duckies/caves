using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System.Collections;

public class Plot
{
  public Vector3 worldLocation;
  public Vector3Int localLocation;
  public Plant plant;
  public GameObject progressBar = null;
  public Slider slider = null;
  public float growth;
  public bool isWatered = false;

  private Tilemap Tilemap;
  private TileBase TileData;

  public int stagesDrawn = 0;

  public Plot(Tilemap Tilemap, TileBase Tile, Vector3 worldLocation, Vector3Int localLocation)
  {
    this.Tilemap = Tilemap;
    this.TileData = Tile;
    this.worldLocation = worldLocation;
    this.localLocation = localLocation;
  }

  public int CurrentStage()
  {
    return Mathf.Max(Mathf.FloorToInt(plant.numStages * Progress()), 1);
  }

  public void GrowPlant()
  {
    growth = Mathf.Clamp(growth + Time.deltaTime, 0, plant.growthTime);
  }

  public bool IsGrown()
  {
    return growth >= plant.growthTime;
  }

  public void Update()
  {
    if (!plant) return;

    if (IsGrown()) return;

    GrowPlant();
  }

  public float Progress()
  {
    return Mathf.Clamp(growth / plant.growthTime, 0, plant.growthTime);
  }

  public IEnumerator SetProgressBar()
  {
    float progress = Progress();

    slider.value = progress;

    if (progress == 1.0)
    {
      yield return new WaitForSeconds(2f);

      HideProgressBar();
    }
  }

  public void HideProgressBar()
  {
    progressBar.SetActive(false);
  }
}