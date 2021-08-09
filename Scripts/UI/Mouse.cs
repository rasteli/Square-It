using UnityEngine;

public class Mouse : MonoBehaviour
{
    public void PointerEnter()
    {
        AudioManager.Instance.Play("Hover");
    }

    public void PointerClick()
    {
        AudioManager.Instance.Play("Click");
    }
}
