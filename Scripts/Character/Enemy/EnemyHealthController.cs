using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject bloodFX;
    public SpriteRenderer sr;

    public float maxHealth;

    private float health;
    private int index;

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damageAMT, bool drop, bool shot)
    {
        health -= damageAMT;

        GameObject fx = Instantiate(bloodFX, transform.position, Quaternion.identity);
        ParticleSystem.MainModule main = fx.GetComponent<ParticleSystem>().main;

        main.startColor = sr.color;
        Destroy(fx, 1.2f);

        if (health <= 0)
            Die(drop, shot);
    }

    public void Die(bool drop, bool shot)
    {
        int number = 2;

        if (shot)
            GameManager.Instance.SetScore(GameManager.Instance.GetScore() + 100);

        if (ProbabilityToSpawnCoin(number) && drop)
            Instantiate(coinPrefab, transform.position, Quaternion.identity);

        EnemyContainer.enemies.Remove(gameObject);

        Destroy(gameObject);
    }

    private bool ProbabilityToSpawnCoin(int number)
    {
        int[] cases = new int[4]{0, 2, 2, 2};
        int chosenNumber = cases[Random.Range(0, cases.Length)];

        return number == chosenNumber;
    }
}
