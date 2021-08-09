using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Camera cam;  // used in other scrpits ▬ do not delete ▬
    public GameObject settingsUI;
    public GameObject pauseMenuUI;
    public GameObject deathScreenUI;
    public TextMeshProUGUI coinText;
    public Texture2D[] cursorTextures;
    public AudioMixerSnapshot[] snapshots;

    public static GameManager Instance;

    private int score;
    private bool isPaused;
    private bool gameHasEnded;

    private void Awake()
    {
        MakeInstance();

        if (Time.timeScale != 1) Time.timeScale = 1f;
        if (gameHasEnded) gameHasEnded = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameHasEnded)
        {
            if (!isPaused)
                PauseOrResume(true, true, false, 0); // pause
            else
                PauseOrResume(false, false, true, 1); // resume
        }

        coinText.text = SavePrefs.LoadState("Coins").ToString();
    }

    private void LateUpdate()
    {
        ChangeCursor();
    }

    private void MakeInstance()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void EndGame()
    {
        StartCoroutine(EndGameCoroutine());
    }

    private IEnumerator EndGameCoroutine()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            TransitionSnapshots(0, 1);
            PlayerMovement.Instance.gameObject.SetActive(false);
            
            yield return new WaitForSeconds(1f);
            Time.timeScale = 0f;
            deathScreenUI.SetActive(true);
        }
    }

    public void TransitionSnapshots(int snap1, int snap2)
    {
        FilterControl.Instance.TransitionSnapshots(snapshots[snap1], snapshots[snap2], .15f);
    }

    public void Restart()
    {
        TransitionSnapshots(1, 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseOrResume(bool paused, bool pauseUI, bool shooting, int scale)
    {
        isPaused = paused;
        Time.timeScale = scale;

        pauseMenuUI.SetActive(pauseUI);
        Shooting.Instance.enabled = shooting;

        if (settingsUI.activeInHierarchy)
            settingsUI.SetActive(false);
    }

    public void SetScore(int _score)
    {
        score = _score;

        if (SavePrefs.LoadState("BestScore") < score)
            SavePrefs.SaveState("BestScore", score);
    }

    public int GetScore()
    {
        return score;
    }

    public void ChangeCursor()
    {
        if (settingsUI.activeInHierarchy || deathScreenUI.activeInHierarchy || pauseMenuUI.activeInHierarchy)
            Cursor.SetCursor(cursorTextures[0], Vector2.zero, CursorMode.Auto);
        else
            Cursor.SetCursor(cursorTextures[1], Vector2.zero, CursorMode.Auto);
    }
}
