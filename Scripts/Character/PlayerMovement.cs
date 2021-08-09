using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject jumpFX;
    public HealthController hc; // used in FlyingEnemy.cs
    public Transform groundCheck;
    public Animator jumpAnimator;
    public LayerMask whatIsGround;

    public float jumpVel = 5f;
    public float movementVel = 5f;
    public int availableJumps = 2;

    private Camera cam;
    private Vector2 movement;
    private Vector2 screenPos;

    private bool toJump;
    private bool landed;
    private bool jumped;
    private bool goat;

    private int jumpAmount;
    private float radius = .2f;

    public static PlayerMovement Instance;

    private void Awake()
    {
        MakeInstance();
        
        cam = GameManager.Instance.cam;
        movementVel = SavePrefs.LoadState("Speed", 10);
        availableJumps = (int) SavePrefs.LoadState("Jumps", 3);
    }

    private void MakeInstance()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        screenPos = cam.WorldToScreenPoint(transform.position);

        if (screenPos.y < 0)
             GameManager.Instance.EndGame();

        if (Input.GetButtonDown("Jump"))
            toJump = true;

        if (transform.eulerAngles.z != 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        MovePlayer(toJump);
        toJump = false;
    }

    private void MovePlayer(bool jump)
    {
        transform.Translate(movement * movementVel * Time.fixedDeltaTime);
        Collider2D isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, whatIsGround);

        if (isGrounded != null && !landed && !goat)
        {
            goat = true;
            landed = true;
        }
        else if (isGrounded == null)
        {
            goat = false;
            landed = false;
        }

        if (jump)
        {
            if (goat)
                jumpAmount = availableJumps;
            else
                jumpAmount--;

            if (jumpAmount > 0)
            {
                jumped = true;
                rb.AddForce(transform.up * jumpVel * Time.fixedDeltaTime * 100f, ForceMode2D.Impulse);

                AudioManager.Instance.Play("Jump");
            }
        }

        if (jumped || landed)
        {
            jumped = false;
            landed = false;

            jumpAnimator.SetTrigger("Jump");
            
            GameObject fx = Instantiate(jumpFX, groundCheck.position, Quaternion.identity);
            Destroy(fx, .8f);
        }

    }

    public Vector2 PredictPosition(float timeInSeconds)
    {
        Vector2 position = (Vector2) transform.position;
        position += rb.velocity * timeInSeconds;
        return position;
    }
}
