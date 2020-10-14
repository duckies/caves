public class ItemSlot : Slot
{
  private Item _item;

  public Item item
  {
    get
    {
      return _item;
    }
    set
    {
      _item = value;
      if (_item == null)
      {
        base.icon.color = base.disabledColor;
      }
      else
      {
        icon.sprite = _item.sprite;
        icon.color = base.normalColor;
      }
    }
  }
}