using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
  [SerializeField] private TriggerType type = TriggerType.Range;

  private Transform character = null;
  public float range = 3f;
  public bool triggersOnce = true;
  public bool shouldDestroy = true;
  public Dialog dialog;

  private enum TriggerType { Range, Manual }

  private bool triggered = false;

  private void Awake()
  {
    character = GameObject.FindGameObjectWithTag("Player").transform;
  }

  public void TriggerDialog()
  {
    DialogManager.instance.StartDialog(dialog);
  }

  public void Update()
  {
    if (triggered) return;

    if (type == TriggerType.Range)
    {
      if (Vector2.Distance(transform.position, character.position) <= range)
      {
        Debug.Log("Range Dialog Trigger " + dialog.name);
        TriggerDialog();

        if (triggersOnce)
        {
          if (shouldDestroy)
          {
            Destroy(gameObject);
            return;

          }
          else
          {
            triggered = true;
          }
        }
      }
    }
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(transform.position, range);
  }
}
