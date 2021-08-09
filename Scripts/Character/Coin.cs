using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private PlayerMovement player;
    private float speed = 25f;

    private void Awake()
    {
        player = PlayerMovement.Instance;
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.Play("PickUp");
            SavePrefs.SaveState("Coins", SavePrefs.LoadState("Coins") + 1);

            Destroy(gameObject);
        }
    }
}
