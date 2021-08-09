using UnityEngine;

public class Spike : MonoBehaviour
{
    private Camera cam;
    private Vector2 screenPos;

    public GameObject spikeFX;

    private void Start()
    {
        cam = GameManager.Instance.cam;
    }

    private void Update()
    {
        screenPos = cam.WorldToScreenPoint(transform.position);

        if (screenPos.y < -50)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        HealthController hc = other.transform.GetComponent<HealthController>();
        EnemyHealthController ehc = other.transform.GetComponent<EnemyHealthController>();
        
        if (hc || ehc)
        {
            if (other.transform.CompareTag("Player"))
            {
                hc.TakeDamage(20);
            }
            else if (other.transform.CompareTag("Enemy"))
            {
                ehc.TakeDamage(20, true, false);
            }
        }

        gameObject.SetActive(false);
        AudioManager.Instance.Play("Spike Drop");

        GameObject fx = Instantiate(spikeFX, transform.position, Quaternion.identity);

        Destroy(fx, 2.5f);
        Destroy(gameObject, 1f);
    }
}
