using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Plot
{
  public Vector3 worldLocation;
  public Vector3Int localLocation;
  public Plant plant;
  public GameObject progressBar = null;
  public Slider slider = null;
  public float growth;
  public GameObject plantPrefab = null;
  public bool isWatered = false;
  public bool isTilled = false;

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
    return Mathf.FloorToInt(plant.stages.Length * Progress());
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

  public void SetProgress()
  {
    float progress = Progress();

    slider.value = progress;

    if (progress == 1.0)
    {
      // Figure out a decent way to do this later, coroutines don't work
      // outside of MonoBehaviorus.
      // yield return new WaitForSeconds(2f);

      progressBar.SetActive(false);
    }
    else if (progressBar.activeSelf == false)
    {
      progressBar.SetActive(true);
    }
  }

  public void ClearPlot()
  {
    plant = null;
    growth = 0;
    isWatered = false;
    stagesDrawn = 0;
    progressBar.SetActive(false);
  }
}