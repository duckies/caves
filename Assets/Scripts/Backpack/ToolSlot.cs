public class ToolSlot : Slot
{
  private Tool _tool;

  public Tool tool
  {
    get
    {
      return _tool;
    }
    set
    {
      _tool = value;
      if (_tool == null)
      {
        base.icon.color = base.disabledColor;
      }
      else
      {
        icon.sprite = _tool.sprite;
        icon.color = base.normalColor;
      }
    }
  }
}