using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
  [SerializeField] private GameObject deathMenuUI = null;
  private static bool GameIsPaused = false;

  private Character character = null;

  private void Awake()
  {
    EventManager.instance.DeathEvent += OnDeathEvent;
  }

  private void OnDeathEvent(Character character)
  {
    this.character = character;
    Pause();
  }

  public void Revive()
  {
    character.curHealth = character.health;
    character.transform.position = character.respawnPoint.position;
    character.DrawHearts();


    deathMenuUI.SetActive(false);
    Time.timeScale = 1f;
    GameIsPaused = false;
  }

  public void Pause()
  {
    deathMenuUI.SetActive(true);
    Time.timeScale = 0f;
    GameIsPaused = true;
  }

  public void QuitGame()
  {
    Time.timeScale = 1f;
    SceneManager.LoadScene("MainMenu");
  }
}
