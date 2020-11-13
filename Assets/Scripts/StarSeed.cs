using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSeed : MonoBehaviour
{
  [SerializeField] private GameObject tree = null;
  [SerializeField] private Dialog dialog = null;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      DialogManager.instance.StartDialog(dialog);
    }
  }
}
