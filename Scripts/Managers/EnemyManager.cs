using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public GameObject spawnFX;
    public GameObject[] enemyPrefabs;

    private Camera cam;

    private bool canSpawn;
    private int maxSize = 5;
    private float timeToSpawn;

    private IEnumerator Start()
    {
        if (!GameManager.Instance.cam)
            yield return null;

        cam = GameManager.Instance.cam;

        yield return new WaitForSeconds(1.5f);
        canSpawn = true;
    }

    private void Update()
    {
        timeToSpawn += Time.deltaTime;

        if (timeToSpawn >= 1.1f && canSpawn)
        {
            if (EnemyContainer.enemies.Count < maxSize)
                StartCoroutine(SpawnEnemy());

            timeToSpawn = 0;
        }
    }

    private IEnumerator SpawnEnemy()
    {
        int playerBestScore = (int)SavePrefs.LoadState("BestScore");
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        Vector2 enemyPosition = RandomPosition();

        // Only spawn tank if player's best score is at least equal to this threshold (5000)
        if (randomIndex == 2 && playerBestScore < 5000) randomIndex = Random.Range(0, 2);
        else if (playerBestScore >= 5000) maxSize = 7;
            
        GameObject fx = Instantiate(spawnFX, enemyPosition, Quaternion.identity);
        GameObject enemy = Instantiate(enemyPrefabs[randomIndex], enemyPosition, Quaternion.identity);
        ParticleSystem.MainModule main = fx.GetComponent<ParticleSystem>().main;

        main.startColor = enemy.GetComponent<SpriteRenderer>().color;
        EnemyContainer.enemies.Add(enemy);
        
        enemy.SetActive(false);
        yield return new WaitForSeconds(1.8f);

        enemy.SetActive(true);
        Destroy(fx);
    }

    private Vector2 RandomPosition()
    {
        float randomX = Random.Range(100, cam.pixelWidth - 100);
        float randomY = Random.Range(100, cam.pixelHeight - 100);

        Vector2 position = cam.ScreenToWorldPoint(new Vector2(randomX, randomY));

        return position;
    }
}
