using UnityEngine;
using EZCameraShake;

public class FlyingEnemy : MonoBehaviour
{
    public EnemyHealthController ehc;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Vector2 direction = (other.transform.position - transform.position).normalized;

            AudioManager.Instance.Play("Explosion1");
            CameraShaker.Instance.ShakeOnce(5f, 1f, .1f, .1f);

            PlayerMovement.Instance.hc.TakeDamage(20);
            PlayerMovement.Instance.rb.AddForce(direction * 500f * Time.fixedDeltaTime * 100f);

            ehc.TakeDamage(ehc.maxHealth, false, false);
        }
    }
}
