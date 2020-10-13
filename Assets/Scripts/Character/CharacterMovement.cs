using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
  [SerializeField] private CharacterController2D controller2D = null;
  [SerializeField] private Animator animator = null;
  public float runSpeed = 40f;

  private float horizontalMove = 0f;
  private bool isJumping = false;
  private bool isCrouching = false;

  private void Update()
  {
    horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

    animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

    if (Input.GetButtonDown("Jump"))
    {
      isJumping = true;
      animator.SetBool("IsJumping", true);
    }

    if (Input.GetButtonDown("Crouch"))
    {
      isCrouching = true;
    }
    else if (Input.GetButtonUp("Crouch"))
    {
      isCrouching = false;
    }
  }

  public void OnLanding()
  {
    animator.SetBool("IsJumping", false);
  }

  public void OnCrouching(bool isCrouching)
  {
    animator.SetBool("IsCrouching", isCrouching);
  }

  private void FixedUpdate()
  {
    controller2D.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
    isJumping = false;
  }
}
