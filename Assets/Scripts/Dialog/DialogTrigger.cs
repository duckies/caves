using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
  [SerializeField] private TriggerType type = TriggerType.Range;

  private Transform character = null;
  public float range = 3f;
  public bool triggersOnce = true;
  public Dialog dialog;

  private enum TriggerType { Range, Manual }

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
    if (type == TriggerType.Range)
    {
      if (Vector2.Distance(transform.position, character.position) <= range)
      {
        Debug.Log("Range Dialog Trigger " + dialog.name);
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
