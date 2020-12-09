using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{

  public void Restart()
  {
    SceneManager.LoadScene("MainScene");
  }

  public void QuitGame()
  {
    SceneManager.LoadScene("MainMenu");
  }
}