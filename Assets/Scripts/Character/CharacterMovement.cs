using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
  [SerializeField] private CharacterController2D controller2D = null;
  [SerializeField] private Animator animator = null;
  [SerializeField] private LayerMask whatIsLadder = default;
  public float runSpeed = 40f;
  public float climbSpeed = 20f;
  public bool isClimbing = false;

  private float horizontalMove = 0f;
  private float verticalMove = 0f;
  private bool isJumping = false;
  private RaycastHit2D hit;

  private void Update()
  {
    hit = Physics2D.Raycast(transform.position, Vector2.up, 3f, whatIsLadder);

    horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
    verticalMove = Input.GetAxisRaw("Vertical") * climbSpeed;

    animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

    if (Input.GetButtonDown("Jump"))
    {
      isJumping = true;
      animator.SetBool("IsJumping", true);
    }

    if (hit.collider)
    {
      if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
      {
        isClimbing = true;
      }
    }
    else
    {
      isClimbing = false;
    }
  }

  public void OnLanding()
  {
    animator.SetBool("IsJumping", false);
  }

  private void FixedUpdate()
  {

    controller2D.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime, isJumping, isClimbing);
    isJumping = false;
  }
}
