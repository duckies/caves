using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
  [SerializeField] private Transform character = null;
  [SerializeField] private TriggerType type = TriggerType.Range;

  public float range = 3f;
  public bool triggersOnce = true;
  public Dialog dialog;

  private enum TriggerType { Range, Manual }

  public void TriggerDialog()
  {
    Debug.Log("Starting Dialog " + dialog.name);
    DialogManager.instance.StartDialog(dialog);
  }

  public void Update()
  {
    if (type == TriggerType.Range)
    {
      if (Vector2.Distance(transform.position, character.position) <= range)
      {
        Debug.Log("Dialog in Range");
        TriggerDialog();

        if (triggersOnce)
        {
          Destroy(gameObject);
          return;
        }
      }
    }
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(transform.position, range);
  }
}
