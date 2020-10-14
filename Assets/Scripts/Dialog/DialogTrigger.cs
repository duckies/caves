using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
  [SerializeField] private Transform character = null;
  public bool isDestroyed = true;
  public Dialog dialogue;


  public void TriggerDialogue()
  {
    FindObjectOfType<DialogManager>().StartDialogue(dialogue);
  }

  public void Update()
  {
    // If the player walks over (to the left of) the trigger. 
    if (character.position.x <= gameObject.transform.position.x)
    {
      TriggerDialogue();
      if (isDestroyed)
      {
        Destroy(gameObject);
        return;
      }
    }
  }
}
