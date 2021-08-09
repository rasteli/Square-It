using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Resume()
    {
        GameManager.Instance.PauseOrResume(false, false, true, 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        GameManager.Instance.TransitionSnapshots(1, 0);
    }
}
