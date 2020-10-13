using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{

  [SerializeField] private Image background = null;
  [SerializeField] private Image image = null;
  [SerializeField] private Color pressedColor = default;

  public KeyCode keyCode;
  public String keyCodeName;
  public TextMeshProUGUI keybindText;
  //   public event Action<ItemSlot> OnPointerEnterEvent;
  //   public event Action<ItemSlot> OnPointerExitEvent;
  public event Action<ItemSlot> OnRightClickEvent;
  public event Action<ItemSlot> OnBeginDragEvent;
  public event Action<ItemSlot> OnEndDragEvent;
  public event Action<ItemSlot> OnDragEvent;
  public event Action<ItemSlot> OnDropEvent;

  private Color normalColor = Color.white;
  private Color disabledColor = new Color(1, 1, 1, 0);
  private Color normalBackgroundColor;

  public Item _item;

  public Item Item
  {
    get { return _item; }
    set
    {
      _item = value;
      if (_item == null)
      {
        image.color = disabledColor;
      }
      else
      {
        image.sprite = _item.icon;
        image.color = normalColor;
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
      background.color = pressedColor;
    }
    else if (Input.GetKeyUp(keyCode))
    {
      background.color = normalBackgroundColor;
    }
  }

  protected virtual void OnValidate()
  {
    if (image == null)
    {
      image = GetComponentInChildren<Image>();
    }
  }

  // True always for standard items, but overridable for custom item types.
  public virtual bool CanReceiveItem(Item item)
  {
    return true;
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
    {
      if (OnRightClickEvent != null)
      {
        OnRightClickEvent(this);
      }
    }
  }

  Vector2 originalPosition;

  public void OnBeginDrag(PointerEventData eventData)
  {
    if (OnBeginDragEvent != null) OnBeginDragEvent(this);
  }

  public void OnDrag(PointerEventData eventData)
  {
    if (OnDragEvent != null) OnDragEvent(this);
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    if (OnEndDragEvent != null) OnEndDragEvent(this);
  }

  public void OnDrop(PointerEventData eventData)
  {
    if (OnDropEvent != null) OnDropEvent(this);
  }
}