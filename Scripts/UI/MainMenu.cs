using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator anim;

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SettingsIn(bool state)
    {
        anim.SetBool("Settings", state);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
