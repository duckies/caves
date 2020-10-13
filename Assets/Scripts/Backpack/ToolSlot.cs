using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ToolSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
  [SerializeField] private Color pressedColor = default;
  [SerializeField] private Image icon = null;

  public Tool tool;
  public KeyCode keyCode;
  public string keyCodeName;

  private TextMeshProUGUI text;
  private Image background;
  private Color normalBackgroundColor;

  private void Awake()
  {
    background = GetComponent<Image>();
    text = GetComponentInChildren<TextMeshProUGUI>();

    normalBackgroundColor = background.color;
    icon.sprite = tool.sprite;
    text.SetText("" + keyCodeName);
  }

  private void Update()
  {
    if (Input.GetKeyDown(keyCode)) Select();

    else if (Input.GetKeyUp(keyCode)) Deselect();
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    if (eventData?.button == PointerEventData.InputButton.Left)
      Select();
  }

  public void OnPointerUp(PointerEventData eventData)
  {
    if (eventData?.button == PointerEventData.InputButton.Left)
      Deselect();
  }

  private void Select()
  {
    EventManager.instance.OnToolUse(this);
    background.color = pressedColor;
  }

  private void Deselect()
  {
    background.color = normalBackgroundColor;
  }
}