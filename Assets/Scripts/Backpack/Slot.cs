using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
  [SerializeField] private Image background = null;
  [SerializeField] private Image icon = null;
  [SerializeField] private Color pressedColor = default;
  [SerializeField] private KeyCode keyCode = default;
  [SerializeField] private String keyCodeName = null;
  [SerializeField] private TextMeshProUGUI keybindText = null;

  private Color normalColor = Color.white;
  private Color disabledColor = new Color(1, 1, 1, 0);
  private Color normalBackgroundColor;

  private Item _item;

  public Item item
  {
    get { return _item; }
    set
    {
      _item = value;
      if (_item == null)
      {
        icon.color = disabledColor;
      }
      else
      {
        icon.sprite = _item.icon;
        icon.color = normalColor;
      }
    }
  }

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
      pressed = true;
    }
    else if (Input.GetKeyUp(keyCode))
    {
      pressed = false;
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
    Debug.Log("Dragging!");
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