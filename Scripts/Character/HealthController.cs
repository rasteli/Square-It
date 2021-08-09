using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour
{
    public float maxHealth;
    public SpriteRenderer sr;
    public GameObject shields;
    public GameObject bloodFX;
    public GameObject[] hearts;
    public GameObject[] shield;

    private float health;
    private int healthIndex;
    private int shieldIndex;
    private Color defaultColor;

    private bool shielded;
    private bool hurtEnded;

    private void Awake()
    {
        defaultColor = sr.color;
        health = maxHealth;
        shieldIndex = shield.Length - 1;
        healthIndex = hearts.Length - 1;

        shielded = SavePrefs.LoadState("Shielded") == 1;

        if (shielded)
            shields.SetActive(true);
    }

    public void TakeDamage(float damageAMT)
    {
        if (shielded)
        {
            UpdateHearts(shield, shieldIndex);

            if (shieldIndex < 0)
                shielded = false;
        } else {
            health -= damageAMT;
            UpdateHearts(hearts, healthIndex);
        }

        hurtEnded = false;
        StartCoroutine(Hurt());
        
        if (health <= 0)
            StartCoroutine(Die());
    }

    private IEnumerator Hurt()
    {
        if (!hurtEnded)
        {
            AudioManager.Instance.Play("Hurt");

            sr.color = Color.red;
            yield return new WaitForSeconds(.05f);

            sr.color = defaultColor;
            hurtEnded = true;
        }
    }

    private IEnumerator Die()
    {
        GameObject fx = Instantiate(bloodFX, transform.position, Quaternion.identity);
        ParticleSystem.MainModule main = fx.GetComponent<ParticleSystem>().main;

        main.startColor = defaultColor;
        Destroy(fx, .9f);
        
        if (!hurtEnded)
            yield return null;

        GameManager.Instance.EndGame();
    }

    private void UpdateHearts(GameObject[] array, int index)
    {
        if (index < 0) return;
        
        array[index].SetActive(false);
        index--;
        
        if (array == shield) shieldIndex = index;
        else if (array == hearts) healthIndex = index;
    }
}
