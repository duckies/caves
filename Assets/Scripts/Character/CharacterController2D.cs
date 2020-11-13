using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
  [SerializeField] private float jumpForce = 400f;
  [Range(0, 0.3f)] [SerializeField] private float movementSmoothing = 0.05f;

  [Header("Unity Fields")]
  public LayerMask whatIsGround;
  public Transform groundCheck;

  const float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
  private bool grounded;            // Whether or not the player is grounded.
  private bool facingRight = true;  // For determining which way the player is currently facing.
  private Rigidbody2D _rigidbody2D;
  private Vector3 velocity = Vector3.zero;
  private Vector3 targetVelocity;

  [Header("Events")]
  [Space]

  public UnityEvent OnLandEvent;

  [System.Serializable]
  public class BoolEvent : UnityEvent<bool> { }

  private void Awake()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();

    if (OnLandEvent == null) OnLandEvent = new UnityEvent();
  }

  private void FixedUpdate()
  {
    bool wasGrounded = grounded;
    grounded = false;

    // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
    // This can be done using layers instead but Sample Assets will not overwrite your project settings.

    Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);

    for (int i = 0; i < colliders.Length; i++)
    {
      grounded = true;
      if (!wasGrounded) OnLandEvent.Invoke();
    }
  }

  public void Move(float moveX, float moveY, bool jump, bool climb)
  {
    if (climb)
    {
      // targetVelocity = new Vector2(_rigidbody2D.velocity.x, moveY * 10f);
      targetVelocity = new Vector2(moveX * 3f, moveY * 10f);
      _rigidbody2D.gravityScale = 0;
    }
    else
    {
      targetVelocity = new Vector2(moveX * 10f, _rigidbody2D.velocity.y);
      _rigidbody2D.gravityScale = 3;
    }

    _rigidbody2D.velocity = Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

    if ((moveX > 0 && !facingRight) || (moveX < 0 && facingRight))
    {
      Flip();
    }

    // If the player should jump...
    if (grounded && jump)
    {
      // Add a vertical force to the player.
      grounded = false;
      _rigidbody2D.AddForce(new Vector2(0f, jumpForce));
    }
  }

  private void Flip()
  {
    // Switch the way the player is labelled as facing.
    facingRight = !facingRight;

    Vector3 scale = transform.localScale;
    scale.x *= -1;
    transform.localScale = scale;
  }
}
