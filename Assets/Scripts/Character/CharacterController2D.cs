using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
  [SerializeField] private float jumpForce = 400f;
  [Range(0, 1)] [SerializeField] private float crouchSpeed = 0.36f;
  [Range(0, 0.3f)] [SerializeField] private float movementSmoothing = 0.05f;
  [SerializeField] private bool airControl = false;

  [Header("Unity Fields")]
  public LayerMask whatIsGround;
  public Transform groundCheck;
  public Transform ceilingCheck;
  public Collider2D crouchDisabledCollider;

  const float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
  private bool grounded;            // Whether or not the player is grounded.
  const float ceilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
  private Rigidbody2D _rigidbody2D;
  private bool facingRight = true;  // For determining which way the player is currently facing.
  private Vector3 velocity = Vector3.zero;

  [Header("Events")]
  [Space]

  public UnityEvent OnLandEvent;

  [System.Serializable]
  public class BoolEvent : UnityEvent<bool> { }

  public BoolEvent OnCrouchEvent;
  private bool wasCrouching = false;

  private void Awake()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();

    if (OnLandEvent == null) OnLandEvent = new UnityEvent();
    if (OnCrouchEvent == null) OnCrouchEvent = new BoolEvent();
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

  public void Move(float move, bool crouch, bool jump)
  {
    // If crouching, check if the character can stand up.
    if (!crouch)
    {
      if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
      {
        crouch = true;
      }
    }

    // Only control the player if grounded or airControl is turned on.
    if (grounded || airControl)
    {
      if (crouch)
      {
        if (!wasCrouching)
        {
          wasCrouching = true;
          OnCrouchEvent.Invoke(true);
        }

        // Reduce speed by the crouchSpeed multiplier
        move *= crouchSpeed;

        // Disable one of the colliders when crouching.
        if (crouchDisabledCollider != null)
        {
          crouchDisabledCollider.enabled = false;
        }
      }
      else
      {
        // Enable the collider when not crouching
        if (crouchDisabledCollider != null)
        {
          crouchDisabledCollider.enabled = true;
        }

        if (wasCrouching)
        {
          wasCrouching = false;
          OnCrouchEvent.Invoke(false);
        }
      }

      // Move the character by finding the target velocity
      Vector3 targetVelocity = new Vector2(move * 10f, _rigidbody2D.velocity.y);
      // And then smoothing it out and applying it to the character
      _rigidbody2D.velocity = Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

      // If the input is moving the player right and the player is facing left...
      if (move > 0 && !facingRight)
      {
        // ... flip the player.
        Flip();
      }
      // Otherwise if the input is moving the player left and the player is facing right...
      else if (move < 0 && facingRight)
      {
        // ... flip the player.
        Flip();
      }
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
