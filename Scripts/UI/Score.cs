using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.text = GameManager.Instance.GetScore().ToString();
    }
}
