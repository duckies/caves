using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
  [SerializeField] private GameObject dialog = null;
  [SerializeField] private GameObject button = null;
  [SerializeField] private TextMeshProUGUI text = null;

  private Coroutine routine = null;

  public float autoAdvanceTimer = 5.0f;
  public Animator animator;
  public static DialogManager instance;
  public float typingSpeed;
  public Dialog curDialog;

  private Queue<string> sentences;

  private void Awake()
  {
    if (instance == null) instance = this;
    else Destroy(this);
  }

  private void Start()
  {
    sentences = new Queue<string>();
  }

  private IEnumerator Type(string sentence)
  {
    animator.SetTrigger("Change");

    foreach (char character in sentence)
    {
      text.text += character;
      yield return new WaitForSeconds(typingSpeed);
    }

    button.SetActive(true);
    // StartCoroutine(ContinueTimer());
  }

  private IEnumerator ContinueTimer()
  {
    yield return new WaitForSeconds(autoAdvanceTimer);
    DisplayNextSentence();
  }

  public void StartDialog(Dialog dialogue)
  {
    dialog.SetActive(true);

    curDialog = dialogue;

    sentences.Clear();

    foreach (string sentence in dialogue.sentences)
    {
      sentences.Enqueue(sentence);
    }

    DisplayNextSentence();
  }

  public void DisplayNextSentence()
  {
    if (routine != null)
    {
      StopCoroutine(routine);
    }

    button.SetActive(false);

    if (sentences.Count == 0)
    {
      EndDialogue();
      return;
    }

    string sentence = sentences.Dequeue();
    text.text = "";
    routine = StartCoroutine(Type(sentence));
  }

  public void EndDialogue()
  {
    dialog.SetActive(false);
    EventManager.instance.OnDialogCompleteEvent(curDialog);
  }
}
