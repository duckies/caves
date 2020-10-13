using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

//  Medium tutorial for storing unique data in a tile.
//  https://medium.com/@allencoded/unity-tilemaps-and-storing-individual-tile-data-8b95d87e9f32

public class FarmController : MonoBehaviour
{
  [SerializeField] private Tilemap FarmingTilemap = null;
  [SerializeField] private Color highlightColor = default;
  [SerializeField] private Plant tempPlant = null;
  [SerializeField] private GameObject ProgressBar = null;

  private Dictionary<Vector3, Plot> Farm = null;
  private Plot selected = null;
  private Quaternion UIRotation = new Quaternion(0, 0, 0, 0);

  private void Awake()
  {
    EventManager.instance.ToolUse += UseTool;

    Farm = new Dictionary<Vector3, Plot>();

    foreach (Vector3Int position in FarmingTilemap.cellBounds.allPositionsWithin)
    {
      if (!FarmingTilemap.HasTile(position)) continue;

      TileBase tile = FarmingTilemap.GetTile(position);
      Vector3 worldPosition = FarmingTilemap.CellToWorld(position);

      Plot Plot = new Plot(FarmingTilemap, tile, worldPosition, position);

      Farm.Add(position, Plot);
    }
  }

  private void Update()
  {
    // On Left Click.
    if (Input.GetMouseButtonDown(0))
    {
      (Vector3Int coordinates, TileBase tile) = GetTileFromCoords(Input.mousePosition);

      // If we didn't click on a tile, abort.
      if (!tile) return;

      // Clear the current color modification on the selected tile.
      if (selected != null)
      {
        FarmingTilemap.SetColor(selected.localLocation, Color.white);
      }

      // Select the new tile.
      if (Farm.TryGetValue(coordinates, out selected))
      {
        FarmingTilemap.SetTileFlags(selected.localLocation, TileFlags.None);
        FarmingTilemap.SetColor(selected.localLocation, highlightColor);
      };

      // If there is no plant in the plot, put one there.
      if (!selected.plant)
      {
        // TODO: REMOVE TEMPORARY
        selected.plant = tempPlant;
        Debug.Log("Added Plant: " + coordinates);
      }

      DrawProgressBar(selected);
    }

    GrowPlants();
  }

  private void UseTool(ToolSlot toolSlot)
  {
    switch (toolSlot.tool.name)
    {
      case "Watering Pail":
        UseWateringPail();
        return;
      case "Plow":
        UsePlow();
        return;
      default:
        return;
    }
  }

  private void UseWateringPail()
  {
    if (selected == null || selected.isWatered) return;


  }

  private void UsePlow()
  {
    // Can we harvest?
    if (selected == null || !selected.IsGrown()) return;

    EventManager.instance.OnHarvestPlant(selected.plant.treeGrowthAmount);

    Vector3Int position = new Vector3Int(selected.localLocation.x, selected.localLocation.y, 0);

    for (int i = 0; i < selected.CurrentStage(); i++)
    {
      position.y += 1;
      FarmingTilemap.SetTile(position, null);
    }

    selected.HideProgressBar();
  }


  public void GrowPlants()
  {
    foreach (var plot in Farm)
    {
      if (plot.Value.plant != null)
      {
        plot.Value.Update();
        SetPlantTiles(plot.Value);
        StartCoroutine(plot.Value.SetProgressBar());
      }
    }
  }

  public void DrawProgressBar(Plot Plot)
  {
    if (Plot.progressBar == null)
    {
      Vector3 position = new Vector3(
        Plot.worldLocation.x + 0.5f,
        Plot.worldLocation.y + 2f,
        Plot.worldLocation.z
      );

      Plot.progressBar = (GameObject)Instantiate(ProgressBar, position, UIRotation);
      Plot.slider = Plot.progressBar.GetComponent<Slider>();
    }
  }

  public void SetPlantTiles(Plot plot)
  {
    Debug.Log("Stages Drawn: " + plot.stagesDrawn);
    Debug.Log("Current Stage: " + plot.CurrentStage());
    while (plot.stagesDrawn < plot.CurrentStage())
    {
      Debug.Log("Adding Plant Stage: " + plot.stagesDrawn + 1);
      DrawPlotStage(plot, plot.stagesDrawn + 1);
      plot.stagesDrawn += 1;
    }
  }

  public void DrawPlotStage(Plot plot, int stage)
  {
    TileBase[] tilesToDraw = plot.plant.GetStage(stage);

    for (var i = 0; i < tilesToDraw.Length; i++)
    {
      PlaceTile(plot.localLocation, i + 1, tilesToDraw[i]);
    }
  }

  public void PlaceTile(Vector3Int position, int offset, TileBase tile)
  {
    position.y += offset;

    FarmingTilemap.SetTile(position, tile);
  }

  private void UpdatePlant(Plot Plot)
  {
    Plot.Update();
  }

  public (Vector3Int, TileBase) GetTileFromCoords(Vector3 worldPosition)
  {
    Vector2 position = Camera.main.ScreenToWorldPoint(worldPosition);
    Vector3Int coordinates = FarmingTilemap.WorldToCell(position);

    return (coordinates, FarmingTilemap.GetTile<TileBase>(coordinates));
  }
}
