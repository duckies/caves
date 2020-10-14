using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
  [SerializeField] protected Image background = null;
  [SerializeField] protected Color pressedColor = default;
  [SerializeField] private KeyCode keyCode = default;
  [SerializeField] private String keyCodeName = null;
  [SerializeField] private TextMeshProUGUI keybindText = null;

  private Color normalBackgroundColor;

  public Color normalColor = Color.white;
  public Color disabledColor = new Color(1, 1, 1, 0);
  public Image icon = null;
  public bool _pressed;
  public bool pressed
  {
    get
    {
      return _pressed;
    }
    set
    {
      _pressed = value;

      if (_pressed == false)
      {
        background.color = normalBackgroundColor;
      }
      else
      {
        background.color = pressedColor;
      }
    }
  }

  protected virtual void Awake()
  {
    keybindText.SetText("" + keyCodeName);
    normalBackgroundColor = background.color;
  }

  protected virtual void Update()
  {
    if (Input.GetKeyDown(keyCode))
    {
      EventManager.instance.OnKeyDownEvent(this);
    }
    else if (Input.GetKeyUp(keyCode))
    {
      EventManager.instance.OnKeyUpEvent(this);
    }
  }

  protected virtual void OnValidate()
  {
    if (icon == null)
    {
      icon = GetComponentInChildren<Image>();
    }
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    if (eventData?.button == PointerEventData.InputButton.Left)
    {
      EventManager.instance.OnLeftClickDownEvent(this);
    }
    else if (eventData?.button == PointerEventData.InputButton.Right)
    {
      EventManager.instance.OnRightClickDownEvent(this);
    }
  }

  public void OnPointerUp(PointerEventData eventData)
  {
    if (eventData?.button == PointerEventData.InputButton.Left)
    {
      EventManager.instance.OnLeftClickUpEvent(this);
    }
    else if (eventData?.button == PointerEventData.InputButton.Right)
    {
      EventManager.instance.OnRightClickUpEvent(this);
    }
  }

  Vector2 originalPosition;

  public void OnBeginDrag(PointerEventData eventData)
  {
    EventManager.instance.OnBeginDragEvent(this);
  }

  public void OnDrag(PointerEventData eventData)
  {
    EventManager.instance.OnDragEvent(this);
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    EventManager.instance.OnEndDragEvent(this);
  }

  public void OnDrop(PointerEventData eventData)
  {
    EventManager.instance.OnDropEvent(this);
  }
}