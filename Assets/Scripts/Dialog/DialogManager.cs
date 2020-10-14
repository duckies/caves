using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
  [SerializeField] private GameObject dialogueBubble = null;
  [SerializeField] private Transform character = null;

  public Animator animator;
  private Queue<string> sentences;
  private TextMeshProUGUI text;

  private void Start()
  {
    sentences = new Queue<string>();
    // dialogueBubble.SetActive(false);
    text = dialogueBubble.GetComponentInChildren<TextMeshProUGUI>();
  }

  public void StartDialogue(Dialog dialogue)
  {
    Debug.Log("Starting conversation");
    animator.SetBool("IsOpen", true);
    // dialogueBubble.SetActive(true);

    sentences.Clear();

    foreach (string sentence in dialogue.sentences)
    {
      sentences.Enqueue(sentence);
    }

    DisplayNextSentence();
  }

  private void Update()
  {
    if (animator.GetBool("IsOpen") == true)
    {
      dialogueBubble.transform.localPosition = new Vector3(character.position.x, character.position.y - 3, 0);
    }
  }

  public void DisplayNextSentence()
  {
    if (sentences.Count == 0)
    {
      EndDialogue();
      return;
    }

    string sentence = sentences.Dequeue();
    text.SetText(sentence);
    Debug.Log(sentence);
  }

  public void EndDialogue()
  {
    // dialogueBubble.SetActive(false);
    animator.SetBool("IsOpen", false);
    Debug.Log("End Dialogue");
  }
}
