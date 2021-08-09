using TMPro;
using UnityEngine;
using System.Collections;

public class DeathScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    private void Awake()
    {
        scoreText.text = GameManager.Instance.GetScore().ToString();
        bestScoreText.text = SavePrefs.LoadState("BestScore").ToString();
    }
}
