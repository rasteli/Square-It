using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Gun gun;
    private Camera cam;
    private Vector2 screenPos;

    private int width;
    private int height;

    private void Awake()
    {
        cam = GameManager.Instance.cam;

        width = cam.pixelWidth;
        height = cam.pixelHeight;
    }

    private void Update()
    {
        screenPos = cam.WorldToScreenPoint(transform.position);

        if (screenPos.x > width + 50 || screenPos.x < -50 || screenPos.y > height + 50 || screenPos.y < -50)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        SpriteRenderer sr = other.gameObject.GetComponent<SpriteRenderer>();
        HealthController hc = other.gameObject.GetComponent<HealthController>();
        EnemyHealthController ehc = other.gameObject.GetComponent<EnemyHealthController>();
        
        if (hc || ehc)
        {
            if (other.CompareTag("Player") && gameObject.CompareTag("BOE"))
            {
                hc.TakeDamage(20);
            }
            else if (other.CompareTag("Enemy") && gameObject.CompareTag("BOP"))
            {
                gun = Shooting.gun;
                ehc.TakeDamage(gun.GetDamage(), true, true);
            }
        }

        if (!gameObject.CompareTag(other.tag))
            Destroy(gameObject);
    }
}
