using UnityEngine;

public class Ladder : MonoBehaviour
{
  [SerializeField] private CharacterMovement movement = null;

  private PlatformEffector2D effector2D = null;

  private void Start()
  {
    effector2D = GetComponent<PlatformEffector2D>();
  }

  private void Update()
  {
    if (movement.isClimbing) return;

    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
    {
      effector2D.rotationalOffset = 180f;
    }

    if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
    {
      effector2D.rotationalOffset = 0f;

    }
  }
}
