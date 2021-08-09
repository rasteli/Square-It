using UnityEngine;
using System.Collections;

public class SpikeSpawner : MonoBehaviour
{
    private Camera cam;
    private float time;

    public GameObject spikePrefab;

    private IEnumerator Start()
    {
        if (!GameManager.Instance.cam)
            yield return null;
        
        cam = GameManager.Instance.cam;
    }
    
    private void Update()
    {
        time += Time.deltaTime;
        float randomTime = Random.Range(2f, 10f);

        if (time >= randomTime)
        {
            Spawn();

            time = 0;
        }
    }

    private void Spawn()
    {
        Vector2 position = RandomPosition();

        Instantiate(spikePrefab, position, transform.rotation);
    }

    private Vector2 RandomPosition()
    {
        float randomX = Random.Range(50, cam.pixelWidth - 50);
        float randomY = transform.position.y + Random.Range(-5, 0);

        Vector2 position = cam.ScreenToWorldPoint(new Vector2(randomX, 0));

        return new Vector2(position.x, randomY);
    }
}
