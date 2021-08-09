using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed;
    
    public LayerMask whatIsEnemy;
    public LayerMask whatIsGround;
    public Transform[] enemyChecks;
    public Transform[] groundChecks;
    public EnemyHealthController ehc;

    private Transform player;
    private Transform enemyCheck;
    private Transform groundCheck;

    private float distance;
    private float radius = .2f;

    private bool canMove;

    private Camera cam;
    private Vector2 movement;
    private Vector2 screenPos;

    private void Awake()
    {
        cam = GameManager.Instance.cam;
        player = PlayerMovement.Instance.gameObject.transform;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(.5f);
        canMove = true;
    }

    private void Update()
    {
        screenPos = cam.WorldToScreenPoint(transform.position);
        distance = Mathf.Abs(player.position.x - transform.position.x);

        if (screenPos.y < 0 || screenPos.x < -50 || screenPos.x > cam.pixelWidth + 50)
            ehc.Die(true, false);
        
        if (player.position.x < transform.position.x)
            SetDirection(0, -1);
        else
            SetDirection(1, 1);

        if (transform.eulerAngles.z != 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Collider2D isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, whatIsGround);
            Collider2D hasEnemy = Physics2D.OverlapCircle(enemyCheck.position, radius, whatIsEnemy);

            if (distance > 3 && isGrounded && !hasEnemy)
            {
                transform.Translate(movement * movementSpeed * Time.fixedDeltaTime);
            }  //TODO Retreating distance -> maybe flipping the player? Most likely...
        }
    }

    private void SetDirection(int index, int multiplier)
    {
        groundCheck = groundChecks[index];
        enemyCheck = enemyChecks[index];
        movement = Vector2.right * multiplier;
    }
}
